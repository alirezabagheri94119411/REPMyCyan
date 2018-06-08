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
    /// افتتاحیه و اختتامیه
    /// </summary>
   public class OpeningClosingsService: IOpeningClosingsService
    {
        #region Constructors

        public OpeningClosingsService(SainaDbContext uow, IAppContextService appContextService)
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
        /// <summary>
        /// باید فانکشن صدا زده شود.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<List<AccDocumentItem>> GetAccDocumentItemsAsync(int date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            //نمایش آیتم های سند حسابداری که معین آن ها انتظامی یا انتظامی هستند.
            var xxx = (await _uow.AccDocumentItems.Where(x => (x.SL.TL.GL.AccountGLEnum == AccountGLEnum.BalanceSheet
           || x.SL.TL.GL.AccountGLEnum == AccountGLEnum.Disciplinary) && (_uow.ShamsiToMiladi(x.AccDocumentHeader.DocumentDate, "Saal") == date.ToString())).ToListAsync().ConfigureAwait(false));
            //var xxx = (await _uow.AccDocumentItems.Where(x => x.SL.TL.GL.AccountGLEnum == AccountGLEnum.BalanceSheet
            // || x.SL.TL.GL.AccountGLEnum == AccountGLEnum.Disciplinary).ToListAsync().ConfigureAwait(false))
            //.Where(y=> persianCalendar.GetYear(y.AccDocumentHeader.DocumentDate) == date).ToList();
            return xxx;
            // return  (await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.TypeDocumentId == 1).ToListAsync().ConfigureAwait(false));
        }

        public async Task<bool> GetCloseProfitLossAccountAsync(int date)
        {
            //PersianCalendar persianCalendar = new PersianCalendar();
            //var acc = (await (_uow.AccDocumentItems.Where(x => x.SL.TL.GL.AccountGLEnum == AccountGLEnum.ProfitLoss).ToListAsync().ConfigureAwait(false))).Where(y=>persianCalendar.GetYear(y.AccDocumentHeader.DocumentDate) == date).Sum(z=>z.Debit-z.Credit);
            var acc = (await (_uow.AccDocumentItems.Where(x => x.SL.TL.GL.AccountGLEnum == AccountGLEnum.ProfitLoss &&  (_uow.ShamsiToMiladi(x.AccDocumentHeader.DocumentDate, "Saal") == date.ToString())).ToListAsync().ConfigureAwait(false))).Sum(z=>z.Debit-z.Credit);


            return acc==0;
        }

        public async Task<bool> GetCloseAccAsync(int date)
        {
           // PersianCalendar persianCalendar = new PersianCalendar();
           //return (await (_uow.AccDocumentHeaders.Where(x => x.TypeDocumentId == 4).ToListAsync().ConfigureAwait(false))).Any(x => persianCalendar.GetYear(x.DocumentDate)==date);
          return (await (_uow.AccDocumentHeaders.AnyAsync(x => x.TypeDocumentId == 4 && (_uow.ShamsiToMiladi(x.DocumentDate, "Saal") == date.ToString())).ConfigureAwait(false)));
         //  return (await (_uow.AccDocumentItems.AnyAsync(x => x.SL.TL.GL.AccountGLEnum==AccountGLEnum.ProfitLoss && (_uow.ShamsiToMiladi(x.DocumentDate, "Saal") == date.ToString())).ConfigureAwait(false)));
        }
        public bool GetCloseAcc(int date)
        {
            // PersianCalendar persianCalendar = new PersianCalendar();
            //return (await (_uow.AccDocumentHeaders.Where(x => x.TypeDocumentId == 4).ToListAsync().ConfigureAwait(false))).Any(x => persianCalendar.GetYear(x.DocumentDate)==date);
            return ( (_uow.AccDocumentHeaders.Any(x => x.TypeDocumentId == 4 && (_uow.ShamsiToMiladi(x.DocumentDate, "Saal") == date.ToString()))));
            //  return (await (_uow.AccDocumentItems.AnyAsync(x => x.SL.TL.GL.AccountGLEnum==AccountGLEnum.ProfitLoss && (_uow.ShamsiToMiladi(x.DocumentDate, "Saal") == date.ToString())).ConfigureAwait(false)));
        }
        public async Task<bool> GetOpenAccAsync(int date)
        {
           // PersianCalendar persianCalendar = new PersianCalendar();
            return (await (_uow.AccDocumentHeaders.AnyAsync(x => x.TypeDocumentId == 3 && (_uow.ShamsiToMiladi(x.DocumentDate, "Saal") ==date.ToString())).ConfigureAwait(false)));
           // return (await (_uow.AccDocumentHeaders.Where(x => x.TypeDocumentId == 3).ToListAsync().ConfigureAwait(false))).Any(x => persianCalendar.GetYear(x.DocumentDate) == date);

        }
        public async Task<List<AccDocumentItemOpenCloseDTO>> AddOpenAccAsync(DateTime start , DateTime end)
        {
            var originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var q = $@"SELECT sl.SLId,sl.SLCode,sl.Title,DL1Id,dl1.DLCode,dl1.Title,di.DL2Id,dl2.DLCode,dl2.Title,di.CurrencyId,
CASE WHEN (SUM(Debit)-SUM(Credit)) > 0 THEN (SUM(Debit)-SUM(Credit))*-1 ELSE 0 END SumDebit,
CASE WHEN (SUM(Debit)-SUM(Credit)) < 0 THEN (SUM(Debit)-SUM(Credit)) ELSE 0 END SumCredit,
SUM(CurrencyAmount) SumCurrencyAmount FROM Acc.AccDocumentItems di
    INNER JOIN Acc.AccDocumentHeaders dh ON dh.AccDocumentHeaderId = di.AccDocumentHeaderId
	INNER JOIN Info.SLs sl ON sl.SLId = di.SLId
	INNER JOIN Info.TLs tl ON tl.TLId = sl.TLId
	INNER JOIN  Info.GLs gl ON gl.GLId = tl.GLId
	LEFT JOIN Info.DLs dl1 ON dl1.DLId = di.DL1Id
	LEFT JOIN Info.DLs dl2 ON dl2.DLId = di.DL2Id
WHERE gl.AccountGLEnum&5=gl.AccountGLEnum AND dh.DocumentDate BETWEEN '{start.ToShortDateString()}' AND '{end.ToShortDateString()}'
GROUP BY sl.SLId,sl.SLCode,sl.Title,DL1Id,dl1.DLCode,dl1.Title,di.DL2Id,dl2.DLCode,dl2.Title,di.CurrencyId";
            Thread.CurrentThread.CurrentCulture = originalCulture;
            var result = await _uow.Database.SqlQuery<AccDocumentItemOpenCloseDTO>(q).ToListAsync().ConfigureAwait(false);
          
            return result;
        }
        public async Task<List<AccDocumentItemOpenCloseDTO>> AddCloseAccAsync(DateTime start, DateTime end)
        {
            var originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var q = $@"SELECT sl.SLId,sl.SLCode,sl.Title,DL1Id,dl1.DLCode,dl1.Title,di.DL2Id,dl2.DLCode,dl2.Title,di.CurrencyId,
CASE WHEN (SUM(Debit)-SUM(Credit)) < 0 THEN (SUM(Debit)-SUM(Credit))*-1 ELSE 0 END SumDebit,
CASE WHEN (SUM(Debit)-SUM(Credit)) > 0 THEN (SUM(Debit)-SUM(Credit)) ELSE 0 END SumCredit,
SUM(CurrencyAmount) SumCurrencyAmount FROM Acc.AccDocumentItems di
    INNER JOIN Acc.AccDocumentHeaders dh ON dh.AccDocumentHeaderId = di.AccDocumentHeaderId
	INNER JOIN Info.SLs sl ON sl.SLId = di.SLId
	INNER JOIN Info.TLs tl ON tl.TLId = sl.TLId
	INNER JOIN  Info.GLs gl ON gl.GLId = tl.GLId
	LEFT JOIN Info.DLs dl1 ON dl1.DLId = di.DL1Id
	LEFT JOIN Info.DLs dl2 ON dl2.DLId = di.DL2Id
WHERE gl.AccountGLEnum&5=gl.AccountGLEnum AND dh.DocumentDate BETWEEN '{start.ToShortDateString()}' AND '{end.ToShortDateString()}'
GROUP BY sl.SLId,sl.SLCode,sl.Title,DL1Id,dl1.DLCode,dl1.Title,di.DL2Id,dl2.DLCode,dl2.Title,di.CurrencyId";
            Thread.CurrentThread.CurrentCulture = originalCulture;
            var result = await _uow.Database.SqlQuery<AccDocumentItemOpenCloseDTO>(q).ToListAsync().ConfigureAwait(false);

            return result;
        }
        #endregion
    }
}
