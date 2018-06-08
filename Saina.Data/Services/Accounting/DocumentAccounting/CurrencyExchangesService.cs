using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using System.Threading;

namespace Saina.Data.Services.Accounting.DocumentAccounting
{
    /// <summary>
    /// سرویس تسعیر ارز
    /// </summary>
    public class CurrencyExchangesService : ICurrencyExchangesService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public CurrencyExchangesService(SainaDbContext uow, IAppContextService appContextService)
        {
            _uow = uow;
            _appContextService = appContextService;
        }
        #endregion
        #region Fields
        SainaDbContext _uow;
        readonly IAppContextService _appContextService;
        #endregion
        #region Properties
        public bool ContextHasChanges => _uow.ContextHasChanges;
        #endregion
        #region Methode

        public async Task<AccDocumentHeader> AddAccDocumentHeaderAsync(AccDocumentHeader accDocumentHeader)
        {
            _uow.AccDocumentHeaders.Add(accDocumentHeader);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return accDocumentHeader;
        }
        public long GetLastAccDocumentHeaderCode()
        {
            if (_uow.AccDocumentHeaders.Any())
                return _uow.AccDocumentHeaders.Select(x => x.DocumentNumber).Max();
            return 0;
        }
        public async Task<DocumentNumbering> GetDocumentNumberingAsync()
        {
            return await _uow.Set<DocumentNumbering>().AsNoTracking().FirstOrDefaultAsync(x => x.AccountDocument.AccountDocumentId == 1).ConfigureAwait(false);
        }
        public async Task<DateTime?> GetLastDateDocAsync(int date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            var years = (await (_uow.AccDocumentHeaders.Where(x => x.TypeDocument.TypeDocumentId == 1 ).ToListAsync().ConfigureAwait(false))).Select(x => new { DocumentDateYear = persianCalendar.GetYear(x.DocumentDate), x.DocumentDate });
            var docYear = years.Where(x => x.DocumentDateYear == date);
            //var years = (await (_uow.AccDocumentHeaders.Where(x => x.TypeDocument.TypeDocumentId == 1).ToListAsync().ConfigureAwait(false))).Select(x => new { DocumentDateYear = persianCalendar.GetYear(x.DocumentDate), x.DocumentDate });
            //var docYear = years.Where(x => x.DocumentDateYear == date);
            if (docYear.Any())
            {
                return (docYear.Max(y => y.DocumentDate));
            }
            return null;
        }
        //دریافت آیتم هایی که از آخرین تاریخ تا تاریخ کنونی ما باشند و ارز داشته باشد.
        public async Task<List<AccDocumentItem>> GetLastAccDocumentItemsAsync(DateTime fromDate, DateTime toDate)
        {
           // PersianCalendar persianCalendar = new PersianCalendar();

            return await _uow.AccDocumentItems.Where(x => x.CurrencyAmount != 0).ToListAsync().ConfigureAwait(false);
            //  return await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate>= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.CurrencyAmount != 0).ToListAsync().ConfigureAwait(false);

        }
        //گروهبندی براساس کد معینو تفصیل و... برای نمایش اقلام.

        public async Task<double?> GetRemainExchangesAsync(DateTime fromDate, DateTime toDate)
        {
            var originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var q = $@"SELECT sum(t.CurrencyAmount * q.ParityRate) - sum(diff) x
from 
    (  select di.CurrencyId, diff = (SUM(Debit) - sum(Credit)),  CurrencyAmount = (SUM(CurrencyAmount * case when Debit = 0 then -1 else 1 end))/( case when c.ExchangeUnit  = 0 then 1 end)
	
        from acc.AccDocumentItems di
		LEFT JOIN  Info.Currencies c ON c.CurrencyId = di.CurrencyId
		INNER JOIN Acc.AccDocumentHeaders h ON h.AccDocumentHeaderId = di.AccDocumentHeaderId
       where di.CurrencyId in (SELECT CurrencyId FROM Info.Currencies) AND (h.DocumentDate BetWeen '{fromDate.ToShortDateString()}' And '{toDate.ToShortDateString()}')
        group by di.CurrencyId,c.ExchangeUnit
    ) t
    join (
        select top 1 with ties * from info.ExchangeRates
        order by row_number() over (partition by CurrencyId order by EffectiveDate desc)
    ) q on t.CurrencyId = q.CurrencyId";
            Thread.CurrentThread.CurrentCulture = originalCulture;
            var result = (await _uow.Database.SqlQuery<double?>(q).FirstOrDefaultAsync().ConfigureAwait(false));
            return result;
        }
        public double? GetRemainExchanges(DateTime fromDate, DateTime toDate)
        {
            var originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var q = $@"SELECT sum(t.CurrencyAmount * q.ParityRate) - sum(diff) x
from 
    (  select di.CurrencyId, diff = (SUM(Debit) - sum(Credit)),  CurrencyAmount = (SUM(CurrencyAmount * case when Debit = 0 then -1 else 1 end))/( case when c.ExchangeUnit  = 0 then 1 end)
	
        from acc.AccDocumentItems di
		LEFT JOIN  Info.Currencies c ON c.CurrencyId = di.CurrencyId
		INNER JOIN Acc.AccDocumentHeaders h ON h.AccDocumentHeaderId = di.AccDocumentHeaderId
       where di.CurrencyId in (SELECT CurrencyId FROM Info.Currencies) AND (h.DocumentDate BetWeen '{fromDate.ToShortDateString()}' And '{toDate.ToShortDateString()}')
        group by di.CurrencyId,c.ExchangeUnit
    ) t
    join (
        select top 1 with ties * from info.ExchangeRates
        order by row_number() over (partition by CurrencyId order by EffectiveDate desc)
    ) q on t.CurrencyId = q.CurrencyId";
            Thread.CurrentThread.CurrentCulture = originalCulture;
            var result = ( _uow.Database.SqlQuery<double?>(q).FirstOrDefault());
            return result;
        }
        public async Task<List<AccDocumentItemGroupedDTO>> GetGroupedAccDocumentItemsAsync(DateTime fromDate, DateTime toDate)
        {
            // 
            var originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            var query = $@" WITH CTETable(SLId , SLCode, SLTitle, DL1Id, DL2Id, DL1Code, DL2Code, DLTitle1, DLTitle2, CurrencyTitle,CurrencyId, ParityRate, TrackingNumber, SumDebit, SumCredit, SumCurrencyAmount, TypeDocumentId, DocumentDate)
              As
            (SELECT sl.SLId, sl.SLCode, sl.Title SLTitle, dl1.DLId DL1Id, dl2.DLId DL2Id, dl1.DLCode DL1Code, dl2.DLCode DL2Code, dl1.Title DLTitle1, dl2.Title DLTitle2, c.CurrencyTitle,c.CurrencyId,
            (SELECT TOP 1 e.ParityRate FROM Info.ExchangeRates e WHERE e.CurrencyId = c.CurrencyId ORDER BY e.ExchangeRateId DESC) ParityRate,di.TrackingNumber,
			ISNULL(SUM(di.Debit), 0) SumDebit,ISNULL(SUM(di.Credit), 0) ,ISNULL(SUM(di.CurrencyAmount), 0) ,h.TypeDocumentId,h.DocumentDate
            FROM Acc.AccDocumentItems di
             LEFT JOIN Acc.AccDocumentHeaders h ON h.AccDocumentHeaderId = di.AccDocumentHeaderId
             left JOIN Info.SLs sl ON sl.SLId = di.SLId
             left JOIN Info.DLs dl1 ON dl1.DLId = di.DL1Id
             left JOIN Info.DLs dl2 ON dl2.DLId = di.DL2Id
             left JOIN Info.Currencies c  ON c.CurrencyId = di.CurrencyId
               GROUP BY sl.SLId,sl.SLCode,sl.Title,dl1.DLId,dl2.DLId,dl1.DLCode  ,dl2.DLCode,dl1.Title,dl2.Title,c.CurrencyTitle,c.CurrencyId,di.TrackingNumber,h.TypeDocumentId,h.DocumentDate)
               SELECT SLId, SLCode, SLTitle, DL1Id, DL2Id, DL1Code, DL2Code, DLTitle1, DLTitle2, CurrencyTitle,CurrencyId, ParityRate, SumDebit, SumCredit, SumCurrencyAmount, TrackingNumber, TypeDocumentId, DocumentDate
               FROM CTETable
            WHERE TypeDocumentId=2 AND
                 CTETable.DocumentDate BETWEEN '{fromDate.ToShortDateString()}' AND '{toDate.ToShortDateString()}' 
                AND  SumCurrencyAmount<>0  ";
             
            Thread.CurrentThread.CurrentCulture = originalCulture;
            var result = await _uow.Database.SqlQuery<AccDocumentItemGroupedDTO>(query).ToListAsync().ConfigureAwait(false);
            return result;
            //این کوئری باید با بالا جابه جا شود.
            //            WITH table1(slid, SLCode, SLTitle, DL1Id, DL2Id, DL1Code, DL2Code, DLTitle1, DLTitle2, CurrencyTitle, ParityRate, TrackingNumber, SumDebit, SumCredit, SumCurrencyAmount, TypeDocumentId, DocumentDate)
            //As
            //     (SELECT sl.SLId, sl.SLCode, sl.Title SLTitle, dl1.DLId DL1Id, dl2.DLId DL2Id, dl1.DLCode DL1Code, dl2.DLCode DL2Code, dl1.Title DLTitle1, dl2.Title DLTitle2, c.CurrencyTitle,
            //       (SELECT TOP 1 e.ParityRate FROM Info.ExchangeRates e WHERE e.CurrencyId = c.CurrencyId ORDER BY e.ExchangeRateId DESC) ParityRate,di.TrackingNumber,
            //    ISNULL(SUM(di.Debit), 0) SumDebit,ISNULL(SUM(di.Credit), 0) ,ISNULL(SUM(di.CurrencyAmount), 0)  ,h.TypeDocumentId,h.DocumentDate
            //  FROM Acc.AccDocumentItems di
            //    LEFT JOIN Acc.AccDocumentHeaders h ON h.AccDocumentHeaderId = di.AccDocumentHeaderId
            //    left JOIN Info.SLs sl ON sl.SLId = di.SLId
            //    left JOIN Info.DLs dl1 ON dl1.DLId = di.DL1Id
            //    left JOIN Info.DLs dl2 ON dl2.DLId = di.DL2Id
            //    left JOIN Info.Currencies c  ON c.CurrencyId = di.CurrencyId
            //GROUP BY sl.SLId,sl.SLCode,sl.Title,dl1.DLId,dl2.DLId,dl1.DLCode  ,dl2.DLCode,dl1.Title,dl2.Title,c.CurrencyTitle,c.CurrencyId,di.TrackingNumber,h.TypeDocumentId,h.DocumentDate
            //)
            //SELECT slid,
            //SLCode,
            //SLTitle,
            //DL1Id,
            //DL2Id,
            //DL1Code,
            //DL2Code,
            //DLTitle1,
            //DLTitle2,
            //     CurrencyTitle,
            //      ParityRate,
            //       SumDebit,

            //       SumCredit,
            //SumCurrencyAmount,
            //TrackingNumber,
            //       TypeDocumentId,
            //       DocumentDate
            //FROM table1
            //WHERE NOT EXISTS
            //(
            //    SELECT 1
            //    FROM table1 t2
            //    WHERE t2.SLCode = table1.SLCode
            //          AND t2.TypeDocumentId = 6
            //)
            //AND(table1.DocumentDate BetWeen '{fromDate.ToShortDateString()}' And '{toDate.ToShortDateString()}')
        }
        public List<AccDocumentItemGroupedDTO> GetGroupedAccDocumentItems(DateTime fromDate, DateTime toDate)
        {
            // 
            var originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            var query = $@" WITH CTETable(AccDocumentItemId,SLId , SLCode, SLTitle, DL1Id, DL2Id, DL1Code, DL2Code, DLTitle1, DLTitle2, CurrencyTitle,CurrencyId, ParityRate, TrackingNumber, SumDebit, SumCredit, SumCurrencyAmount, TypeDocumentId, DocumentDate,HasExchange)
              As
            (SELECT di.AccDocumentItemId, sl.SLId, sl.SLCode, sl.Title SLTitle, dl1.DLId DL1Id, dl2.DLId DL2Id, dl1.DLCode DL1Code, dl2.DLCode DL2Code, dl1.Title DLTitle1, dl2.Title DLTitle2, c.CurrencyTitle,c.CurrencyId,
            (SELECT TOP 1 e.ParityRate FROM Info.ExchangeRates e WHERE e.CurrencyId = c.CurrencyId ORDER BY e.ExchangeRateId DESC) ParityRate,di.TrackingNumber,
			ISNULL(SUM(di.Debit), 0) SumDebit,ISNULL(SUM(di.Credit), 0) ,ISNULL(SUM(di.CurrencyAmount), 0) ,h.TypeDocumentId,h.DocumentDate,di.HasExchange
            FROM Acc.AccDocumentItems di
             LEFT JOIN Acc.AccDocumentHeaders h ON h.AccDocumentHeaderId = di.AccDocumentHeaderId
             left JOIN Info.SLs sl ON sl.SLId = di.SLId
             left JOIN Info.DLs dl1 ON dl1.DLId = di.DL1Id
             left JOIN Info.DLs dl2 ON dl2.DLId = di.DL2Id
             left JOIN Info.Currencies c  ON c.CurrencyId = di.CurrencyId
               GROUP BY di.AccDocumentItemId, sl.SLId,sl.SLCode,sl.Title,dl1.DLId,dl2.DLId,dl1.DLCode  ,dl2.DLCode,dl1.Title,dl2.Title,c.CurrencyTitle,c.CurrencyId,di.TrackingNumber,h.TypeDocumentId,h.DocumentDate,di.HasExchange)
               SELECT AccDocumentItemId,SLId, SLCode, SLTitle, DL1Id, DL2Id, DL1Code, DL2Code, DLTitle1, DLTitle2, CurrencyTitle,CurrencyId, ParityRate, SumDebit, SumCredit, SumCurrencyAmount, TrackingNumber, TypeDocumentId, DocumentDate
               FROM CTETable
            WHERE TypeDocumentId=2   
                AND HasExchange=0 and
                 CTETable.DocumentDate BETWEEN '{fromDate.ToShortDateString()}' AND '{toDate.ToShortDateString()}' 
                AND  SumCurrencyAmount<>0  ";

            Thread.CurrentThread.CurrentCulture = originalCulture;
            var result =  _uow.Database.SqlQuery<AccDocumentItemGroupedDTO>(query).ToList();
            return result;
            //این کوئری باید با بالا جابه جا شود.
            //            WITH table1(slid, SLCode, SLTitle, DL1Id, DL2Id, DL1Code, DL2Code, DLTitle1, DLTitle2, CurrencyTitle, ParityRate, TrackingNumber, SumDebit, SumCredit, SumCurrencyAmount, TypeDocumentId, DocumentDate)
            //As
            //     (SELECT sl.SLId, sl.SLCode, sl.Title SLTitle, dl1.DLId DL1Id, dl2.DLId DL2Id, dl1.DLCode DL1Code, dl2.DLCode DL2Code, dl1.Title DLTitle1, dl2.Title DLTitle2, c.CurrencyTitle,
            //       (SELECT TOP 1 e.ParityRate FROM Info.ExchangeRates e WHERE e.CurrencyId = c.CurrencyId ORDER BY e.ExchangeRateId DESC) ParityRate,di.TrackingNumber,
            //    ISNULL(SUM(di.Debit), 0) SumDebit,ISNULL(SUM(di.Credit), 0) ,ISNULL(SUM(di.CurrencyAmount), 0)  ,h.TypeDocumentId,h.DocumentDate
            //  FROM Acc.AccDocumentItems di
            //    LEFT JOIN Acc.AccDocumentHeaders h ON h.AccDocumentHeaderId = di.AccDocumentHeaderId
            //    left JOIN Info.SLs sl ON sl.SLId = di.SLId
            //    left JOIN Info.DLs dl1 ON dl1.DLId = di.DL1Id
            //    left JOIN Info.DLs dl2 ON dl2.DLId = di.DL2Id
            //    left JOIN Info.Currencies c  ON c.CurrencyId = di.CurrencyId
            //GROUP BY sl.SLId,sl.SLCode,sl.Title,dl1.DLId,dl2.DLId,dl1.DLCode  ,dl2.DLCode,dl1.Title,dl2.Title,c.CurrencyTitle,c.CurrencyId,di.TrackingNumber,h.TypeDocumentId,h.DocumentDate
            //)
            //SELECT slid,
            //SLCode,
            //SLTitle,
            //DL1Id,
            //DL2Id,
            //DL1Code,
            //DL2Code,
            //DLTitle1,
            //DLTitle2,
            //     CurrencyTitle,
            //      ParityRate,
            //       SumDebit,

            //       SumCredit,
            //SumCurrencyAmount,
            //TrackingNumber,
            //       TypeDocumentId,
            //       DocumentDate
            //FROM table1
            //WHERE NOT EXISTS
            //(
            //    SELECT 1
            //    FROM table1 t2
            //    WHERE t2.SLCode = table1.SLCode
            //          AND t2.TypeDocumentId = 6
            //)
            //AND(table1.DocumentDate BetWeen '{fromDate.ToShortDateString()}' And '{toDate.ToShortDateString()}')
        }

        public DateTime? GetEndExchangeDoc(int date)
        {
            var year = ( _uow.AccDocumentHeaders.Select(x => new { x.DocumentDate, x.TypeDocumentId })
                .Where(x => x.TypeDocumentId == 6 && _uow.ShamsiToMiladi(x.DocumentDate, "Saal") == date.ToString()));
            if (year != null && year.Any())
            {
              return   year.Max(x => x.DocumentDate);
            }
            //var year =  _uow.AccDocumentItems.Select(x => new { x.AccDocumentHeader.DocumentDate, x.CurrencyId }).Where(x => x.CurrencyId !=0  && _uow.ShamsiToMiladi(x.DocumentDate, "Saal") == date.ToString());
            //if (year!=null)
            //{
            //var m= year.Max(x => x.DocumentDate);
            //return m;

            //}
            else
            {
                return null;
            }
        }
        public async Task<DateTime> GetStartFinancialYear(int date)
        {
            var year = (await _uow.FinancialYears.Select(x => new { x.YearName, x.StartDate }).FirstOrDefaultAsync(x => x.YearName == date)).StartDate;
            return year;
        }
        object o = new object();
        public DateTime GetStartFinancialYear1(int date)
        {
          
            var year = ( _uow.FinancialYears.Select(x => new { x.YearName, x.StartDate }).FirstOrDefault(x => x.YearName == date)).StartDate;

            return year;
            
        }
        public async Task<DateTime> GetEndFinancialYear(int date)
        {
            var year = (await _uow.FinancialYears.Select(x => new { x.YearName, x.EndDate }).FirstOrDefaultAsync(x => x.YearName == date)).EndDate;
            return year;
        }
        public DateTime GetEndFinancialYear1(int date)
        {
            var year = ( _uow.FinancialYears.Select(x => new { x.YearName, x.EndDate }).FirstOrDefault(x => x.YearName == date)).EndDate;
            return year;
        }
        public async Task<List<AccDocumentItemGroupedDTO>> GetCurrencyDocAsync()
        {
            return await _uow.AccDocumentItems.Where(x => x.CurrencyAmount != 0)
                   .GroupBy(x => new { x.SLId, DL1 = x.DL1 == null ? 0 : x.DL1.DLId, DL2 = x.DL2 == null ? 0 : x.DL2.DLId, x.TrackingNumber, x.CurrencyId, x.Currency, x.Debit, x.Credit })
                   .Select(x => new AccDocumentItemGroupedDTO
                   {
                       //  SLId = x.Key.SLId,
                       DL1Code = x.Key.DL1,
                       DL2Code = x.Key.DL2,
                       TrackingNumber = x.Key.TrackingNumber,
                       SumDebit = x.Sum(y => y.Debit),
                       SumCredit = x.Sum(y => y.Credit),
                       SumCurrencyAmount = x.Sum(y => y.CurrencyAmount),
                       //LastCurrency = _uow.ExchangeRates.OrderByDescending(y => y.EffectiveDate).Select(y => y.ParityRate).FirstOrDefault()
                   }).ToListAsync().ConfigureAwait(false);
            ;
        }

       




    public double GetLastCurrencyDocAsync()
    {
      return   _uow.ExchangeRates.OrderByDescending(y => y.EffectiveDate).Select(y => y.ParityRate).FirstOrDefault();
}
        public async Task<Dictionary<int?,double>> GetLastCurrencyDocDictionayAsync()
        {
           return await _uow.ExchangeRates.GroupBy(x=>x.CurrencyId).ToDictionaryAsync(x => x.Key, y => y.OrderBy(z=>z.EffectiveDate).Select(p=>p.ParityRate).First());
        }
        #endregion
    }
    public class AccDocumentHeader0
    {
        public string DocumentDate { get; set; }
    }
}
