using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
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
    /// بستن حساب ها
    /// </summary>
  public  class CloseProfitLossAccountsService: ICloseProfitLossAccountsService
    {
        #region Constructors
       
        public CloseProfitLossAccountsService(SainaDbContext uow, IAppContextService appContextService)
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
        public async Task<List<AccDocumentItem>> GetAccDocumentItemsAsync(int date)
        {
           return await _uow.Set<AccDocumentItem>().AsNoTracking().ToListAsync().ConfigureAwait(false);
         //  return await _uow.AccDocumentItems.Where(x=> _uow.ShamsiToMiladi(x.AccDocumentHeader.DocumentDate, "Saal") ==date.ToString()  && x.SL.TL.GL.AccountGLEnum== AccountGLEnum.ProfitLoss ).ToListAsync().ConfigureAwait(false);
        }
        public async Task<DateTime?> GetLastDateDocAsync(int date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            var years = (await (_uow.AccDocumentHeaders.Where(x => x.TypeDocument.TypeDocumentId == 1).ToListAsync().ConfigureAwait(false))).Select(x => new { DocumentDateYear = persianCalendar.GetYear(x.DocumentDate), x.DocumentDate });
            var docYear = years.Where(x => x.DocumentDateYear == date);
            if (docYear.Any())
            {
                return (docYear.Max(y => y.DocumentDate));
            }
            return null;
        }

        public async Task<List<AccDocumentItemGroupedDTO>> GetGroupedAccDocumentItemsAsync(int date)
        {
           
          var originalCulture = Thread.CurrentThread.CurrentCulture;
          Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var query = $@"SELECT        SLs.SLId,SLs.SLCode, SLs.Title, DLs_1.DLCode, DLs_1.Title, DLs.DLCode, DLs.Title, SUM(AccDocumentItems.Debit) SumDebit, SUM(AccDocumentItems.Credit) SumCredit, SUM(AccDocumentItems.CurrencyAmount)sumCurrencyAmount , Currencies.CurrencyTitle, 
                         AccDocumentItems.CurrencyId
FROM            Info.GLs INNER JOIN
                         Info.TLs ON GLs.GLId = TLs.GLId INNER JOIN
                         Info.SLs ON TLs.TLId = SLs.TLId INNER JOIN
                         Acc.AccDocumentItems ON SLs.SLId = AccDocumentItems.SLId INNER JOIN
                         Acc.AccDocumentHeaders ON AccDocumentItems.AccDocumentHeaderId = AccDocumentHeaders.AccDocumentHeaderId LEFT OUTER JOIN
                         Info.Currencies ON AccDocumentItems.CurrencyId = Currencies.CurrencyId LEFT OUTER JOIN
                         Info.DLs AS DLs_1 ON AccDocumentItems.DL1Id = DLs_1.DLId LEFT OUTER JOIN
                         Info.DLs ON AccDocumentItems.DL2Id = DLs.DLId
WHERE        (GLs.AccountGLEnum = 2) AND (  dbo.ShamsiToMiladi(AccDocumentHeaders.DocumentDate,'Saal')='{date}') and
((SELECT (SUM(AccDocumentItems.Debit- AccDocumentItems.Credit) ) FROM Acc.AccDocumentItems)<>0)
GROUP BY SLs.SLId,SLs.SLCode, SLs.Title, DLs_1.DLCode, DLs_1.Title, DLs.DLCode, DLs.Title, Currencies.CurrencyTitle, AccDocumentItems.CurrencyId";
            Thread.CurrentThread.CurrentCulture = originalCulture;
            var result = await _uow.Database.SqlQuery<AccDocumentItemGroupedDTO>(query).ToListAsync().ConfigureAwait(false);
            return result;

        }
        public List<AccDocumentItemGroupedDTO> GetGroupedAccDocumentItems(int date)
        {

            var originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var query = $@"SELECT        SLs.SLId,SLs.SLCode, SLs.Title, DLs_1.DLCode, DLs_1.Title, DLs.DLCode, DLs.Title, SUM(AccDocumentItems.Debit) SumDebit, SUM(AccDocumentItems.Credit) SumCredit, SUM(AccDocumentItems.CurrencyAmount)sumCurrencyAmount , Currencies.CurrencyTitle, 
                         Acc.AccDocumentItems.CurrencyId
FROM            Info.GLs INNER JOIN
                         Info.TLs ON GLs.GLId = TLs.GLId INNER JOIN
                         Info.SLs ON TLs.TLId = SLs.TLId INNER JOIN
                         Acc.AccDocumentItems ON SLs.SLId = AccDocumentItems.SLId INNER JOIN
                         Acc.AccDocumentHeaders ON AccDocumentItems.AccDocumentHeaderId = AccDocumentHeaders.AccDocumentHeaderId LEFT OUTER JOIN
                         Info.Currencies ON AccDocumentItems.CurrencyId = Currencies.CurrencyId LEFT OUTER JOIN
                         Info.DLs AS DLs_1 ON AccDocumentItems.DL1Id = DLs_1.DLId LEFT OUTER JOIN
                         Info.DLs ON AccDocumentItems.DL2Id = DLs.DLId
WHERE        (GLs.AccountGLEnum = 2) AND (  dbo.ShamsiToMiladi(AccDocumentHeaders.DocumentDate,'Saal')='{date}') and
((SELECT (SUM(AccDocumentItems.Debit- AccDocumentItems.Credit) ) FROM Acc.AccDocumentItems)<>0)
GROUP BY SLs.SLId,SLs.SLCode, SLs.Title, DLs_1.DLCode, DLs_1.Title, DLs.DLCode, DLs.Title, Currencies.CurrencyTitle, AccDocumentItems.CurrencyId";
            Thread.CurrentThread.CurrentCulture = originalCulture;
            var result =  _uow.Database.SqlQuery<AccDocumentItemGroupedDTO>(query).ToList();
            return result;

        }
        public async Task<List<AccDocumentItemGroupedDTO>> GetGroupedAccDocumentItemsAsync1(DateTime fromDate, DateTime toDate)
        {

            var originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var query = $@" WITH table1 (slid,SLCode,AccountGLEnum,SLTitle,DL1Id,DL2id,DL1Code,DL2Code,DLTitle,SumDebit,SumCredit,
 TypeDocumentId,DocumentDate)As(SELECT sl.SLId,sl.SLCode,gl.AccountGLEnum,sl.Title SLTitle,dl1.DLId,dl2.DLId,dl1.DLCode DL1Code
  ,dl2.DLCode DL2Code ,dl1.Title DLTitle,ISNULL(SUM(di.Debit), 0) SumDebit,ISNULL(SUM(di.Credit),0)
   ,h.TypeDocumentId,h.DocumentDate FROM Acc.AccDocumentItems di
    LEFT JOIN Acc.AccDocumentHeaders h ON
    h.AccDocumentHeaderId = di.AccDocumentHeaderId
	    left JOIN Info.SLs sl ON sl.SLId = di.SLId
		LEFT JOIN Info.TLs tl ON tl.TLId = sl.TLId
		LEFT JOIN Info.GLs gl ON gl.GLId = tl.GLId
		left JOIN Info.DLs dl1 ON dl1.DLId = di.DL1Id
		left JOIN Info.DLs dl2 ON dl2.DLId = di.DL2Id
			      left JOIN Info.Currencies c  ON c.CurrencyId = di.CurrencyId
				  GROUP BY sl.SLId,sl.SLCode,gl.AccountGLEnum,sl.Title,dl1.DLId,dl2.DLId,dl1.DLCode  
				  ,dl2.DLCode,dl1.Title,h.TypeDocumentId,h.DocumentDate )
				  SELECT slid,SLCode,SLTitle,DL1Id,DL2id,DL1Code,DL2Code  ,DLTitle,          SumDebit,     
				     SumCredit,  TypeDocumentId,  DocumentDate FROM table1 WHERE NOT EXISTS(    SELECT 1    FROM table1 t2    WHERE t2.SLCode = table1.SLCode       
					    AND t2.AccountGLEnum=2)
AND (table1.DocumentDate BetWeen '{fromDate}' And '{toDate}')";
            Thread.CurrentThread.CurrentCulture = originalCulture;
            var result = await _uow.Database.SqlQuery<AccDocumentItemGroupedDTO>(query).ToListAsync().ConfigureAwait(false);
            return result;

        }


        #endregion
    }
}
