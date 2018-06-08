using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
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
using System.Threading;
using System.Threading.Tasks;

namespace Saina.Data.Services.Accounting.DocumentAccounting
{
    /// <summary>
    /// تبدیل اسناد موقت به دائم
    /// </summary>
    public class ConvertDocumentsService : IConvertDocumentsService
    {
        #region Constructors

        public ConvertDocumentsService(SainaDbContext uow, IAppContextService appContextService)
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

        public async Task<long> GetLastNumberPermanentAsync(int date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            var years = (await (_uow.AccDocumentHeaders.Where(x => x.Status == StatusEnum.Permanent).ToListAsync().ConfigureAwait(false))).Select(x => new { DocumentDate = persianCalendar.GetYear(x.DocumentDate), SystemFixNumber = x.SystemFixNumber });
            //return years.Any(x => x == date);

            //var number= (await _uow.AccDocumentHeaders.Where(x => x.Status == StatusEnum.Permanent).Select(x => x.SystemFixNumber).ToListAsync().ConfigureAwait(false));
            var docYear = years.Where(x => x.DocumentDate == date);
            if (docYear.Any())
            {
                return (docYear.Max(y => y.SystemFixNumber));

            }
            return 0;

        }


        //public async Task<List<AccDocumentHeaderDTO>> GetAccDocumentHeadersAsync(int date)
        //{
        //    PersianCalendar persianCalendar = new PersianCalendar();
        //   // return (await _uow.AccDocumentHeaders.Include(x => x.TypeDocument).AsNoTracking().ToListAsync().ConfigureAwait(false)).Where(x => persianCalendar.GetYear(x.DocumentDate) == date).ToList();
        //  return( await _uow.AccDocumentHeaders.Select(x => new AccDocumentHeaderDTO
        //    {
        //        DailyNumber = x.DailyNumber,
        //        SystemFixNumber = x.SystemFixNumber,
        //        DocumentNumber = x.DocumentNumber,
        //        Exporter = x.Exporter,
        //        Seconder = x.Seconder,
        //        Status = x.Status,
        //        SystemName = x.SystemName,
        //        ManualDocumentNumber = x.ManualDocumentNumber,
        //        HeaderDescription = x.HeaderDescription,
        //        DocumentDate = x.DocumentDate,
        //        AmountDocument = x.AccDocumentItems.Sum(y => y.Debit),
        //        TypeDocumentTitle = x.TypeDocument.TypeDocumentTitle
        //    }).AsNoTracking().ToListAsync().ConfigureAwait(false)).Where(x => persianCalendar.GetYear(x.DocumentDate) == date).ToList();


        //}
        public DateTime GetEndFinancialYear1(int date)
        {
            var year = (_uow.FinancialYears.Select(x => new { x.YearName, x.EndDate }).FirstOrDefault(x => x.YearName == date)).EndDate;
            return year;
        }
        public async Task<DateTime?> GetLastDatePermanentAsync(int date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            var years = (await (_uow.AccDocumentHeaders.Where(x => x.Status == StatusEnum.Permanent).ToListAsync().ConfigureAwait(false))).Select(x => new { DocumentDateYear = persianCalendar.GetYear(x.DocumentDate), x.DocumentDate });
            var docYear = years.Where(x => x.DocumentDateYear == date);
            if (docYear.Any())
            {
                return (docYear.Max(y => y.DocumentDate));

            }

            return null;
            //var q = $"SELECT MAX(DocumentDate) FROM Acc.AccDocumentHeaders  WHERE  dbo.ShamsiToMiladi(TLDocumentHeaderDate,'Saal')='{date}'";
            //var result = _uow.Database.SqlQuery<DateTime?>(q).FirstOrDefault();
            //if (result == null) return DateTime.Now;
            //return result.Value;

        }

        public async Task<int> UpdateAccToNumberAsync(int date,StatusEnum status, int toNumber, StatusEnum convert)
        {
            var originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var q = $"SELECT Min(TLDocumentHeaderDate) FROM Acc.TLDocumentHeaders  WHERE dbo.ShamsiToMiladi(TLDocumentHeaderDate,'Saal')='{date}'";
            var fromdate = await _uow.Database.SqlQuery<DateTime?>(q).FirstOrDefaultAsync();
            var cmd = $@"UPDATE Acc.AccDocumentHeaders SET Status={(int)status} WHERE DocumentNumber<={toNumber}  and Status={(int)convert}";
            Thread.CurrentThread.CurrentCulture = originalCulture;

            return await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);



        }
        public async Task<bool> CanUpdate(int date, DateTime toDate)
        {
            bool hasUpdate = false;
                var originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                var q = $"SELECT Min(DocumentDate) FROM Acc.AccDocumentHeaders  WHERE dbo.ShamsiToMiladi(DocumentDate,'Saal')='{date}'";
                var Permanentdate = await _uow.Database.SqlQuery<DateTime?>(q).FirstOrDefaultAsync();
                if (Permanentdate != null)
                {
                    var cmd = $@"SELECT CASE WHEN EXISTS (
                          select DocumentDate , Status  from Acc.AccDocumentHeaders                           
											 WHERE DocumentDate between '{Permanentdate.Value.ToShortDateString()}'  
                                              and      '{toDate.ToShortDateString()}'                  
											 and( Status<>4  and Status<>2)
                                            )
                                            THEN CAST(1 AS BIT)
                                            ELSE CAST(0 AS BIT) END ";
                    Thread.CurrentThread.CurrentCulture = originalCulture;
                hasUpdate = await  _uow.Database.SqlQuery<bool>(cmd).FirstOrDefaultAsync();
                           

                }
            return hasUpdate;

              
            
        }
            public async Task<int> UpdateAccToDateAsync(int date,StatusEnum status, DateTime toDate, StatusEnum convert,string user)
        {


          
            if (convert==StatusEnum.Permanent)
            {
                var originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                var q = $"SELECT Min(DocumentDate) FROM Acc.AccDocumentHeaders  WHERE dbo.ShamsiToMiladi(DocumentDate,'Saal')='{date}'";
                var Permanentdate = await _uow.Database.SqlQuery<DateTime?>(q).FirstOrDefaultAsync();
                if (Permanentdate!=null)
                {
                var cmd = $@"UPDATE Acc.AccDocumentHeaders SET Status={(int)convert}   WHERE CONVERT(date, DocumentDate) <='{toDate.ToShortDateString()}' and   CONVERT(date, DocumentDate) >='{Permanentdate.Value.ToShortDateString()}' and Status={(int)status} ";
                Thread.CurrentThread.CurrentCulture = originalCulture;
               return 
                       await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);

                }
                else
                {
                    return 0;
                }
            }
            else 
            {
                var originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                var q2 = $"SELECT Max(DocumentDate) FROM Acc.AccDocumentHeaders  WHERE dbo.ShamsiToMiladi(DocumentDate,'Saal')='{date}' and   Status={(int)StatusEnum.Permanent}";
                var enddate = await _uow.Database.SqlQuery<DateTime?>(q2).FirstOrDefaultAsync();
                if (enddate!=null)
                {
                var cmd = $@"UPDATE Acc.AccDocumentHeaders SET Status={(int)convert} WHERE CONVERT(date, DocumentDate) >=   '{toDate.ToShortDateString()}' and  CONVERT(date, DocumentDate)<='{enddate.Value.ToShortDateString()}' and Status={(int)status} ";

            Thread.CurrentThread.CurrentCulture = originalCulture;
                return await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
                }
                else
                {
                    return 0;
                }
            }


        }

        public Task<List<AccDocumentHeader>> GetAccDocumentHeadersAsync()
        {
            throw new NotImplementedException();
        }




        #endregion
    }
}

  
