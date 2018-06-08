using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.DTOs.Accounting.ReviewAcc;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Saina.Data.Services.Accounting.DocumentAccounting
{


    /// <summary>
    /// مرور حساب ها
    /// </summary>
    public class ReviewAccountsService : IReviewAccountsService
    {
        #region Constructors

        public ReviewAccountsService(SainaDbContext uow, IAppContextService appContextService)
        {
            _uow = uow;
            _appContextService = appContextService;
        }
        #endregion
        #region Fields
        SainaDbContext _uow;
        readonly IAppContextService _appContextService;
        private List<AccDocumentItemDTO1> _accDocumentItems;
        #endregion
        #region Properties
        public bool ContextHasChanges => _uow.ContextHasChanges;
        private AccDocumentHeaderFilter _accDocumentHeaderFilter;

        public AccDocumentHeaderFilter AccDocumentHeaderFilter
        {
            get { return _accDocumentHeaderFilter; }
            set
            {
                _accDocumentHeaderFilter = value;
            }
        }

        #endregion
        #region Methode
        public async Task<List<FinancialYear>> GetFinancialYearAsync(int year)
        {
            var q = $@"SELECT * FROM dbo.FinancialYears WHERE YearName> {year}";
            return await _uow.FinancialYears.SqlQuery(q).ToListAsync().ConfigureAwait(false);

        }
        public async Task<List<AccDocumentItemDTO>> GetAccDocumentItemsAsync()
        {
            return ToAccDocumentItemDTO(await _uow.Set<AccDocumentItem>().AsNoTracking().ToListAsync().ConfigureAwait(false));
        }

        public async Task<AccDocumentItemDTO> UpdateAccDocumentItemAsync(AccDocumentItem accDocumentItem)
        {

            //var cmd = $"EXEC AccDocumentItem_Update @AccDocumentItemId = {accDocumentItem.AccDocumentItemId},";
            ///*  $" @AccDocumentItemTitle = N'{accDocumentItem.AccDocumentItemTitle}'"*/
            //;
            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(accDocumentItem).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return new AccDocumentItemDTO();// accDocumentItem;
        }
        public async Task ApplyFilter()
        {
            if (_accDocumentHeaderFilter != null)
                _accDocumentItems = await _uow.AccDocumentItems.AsNoTracking()
                                  .Include(x => x.AccDocumentHeader).Include(x => x.Currency).Include(x => x.DL1).Include(x => x.DL2).Include(x => x.SL).Include(x => x.SL.TL).Include(x => x.SL.TL.GL)
                                   .Where(x => x.AccDocumentHeader.DocumentNumber >= _accDocumentHeaderFilter.FromNumber
                                   && x.AccDocumentHeader.DocumentNumber <= _accDocumentHeaderFilter.ToNumber
                                   && x.AccDocumentHeader.DocumentDate >= _accDocumentHeaderFilter.FromDate
                                   && x.AccDocumentHeader.DocumentDate <= _accDocumentHeaderFilter.ToDate)
                                   .Select(x => new AccDocumentItemDTO1
                                   {
                                       Debit = x.Debit,
                                       Credit = x.Credit,
                                       TypeDocumentId = x.AccDocumentHeader.TypeDocumentId,
                                       SLId = x.SLId,
                                       SLCode = x.SL.SLCode,
                                       SLTitle = x.SL.Title,
                                       DL1Code = x.DL1.DLCode,
                                       DL1Id = x.DL1Id,
                                       DL1Title = x.DL1.Title,
                                       DL2Id = x.DL2Id,
                                       DL2Code = x.DL2.DLCode,
                                       DL2Title = x.DL2.Title,
                                       TLCode = x.SL.TL.TLCode,
                                       TLId = x.SL.TLId,
                                       TLTitle = x.SL.TL.Title,
                                       GLCode = x.SL.TL.GL.GLCode,
                                       GLId = x.SL.TL.GLId,
                                       GLTitle = x.SL.TL.GL.Title,
                                       CurrencyId = x.Currency.CurrencyId,
                                       CurrencyTitle = x.Currency.CurrencyTitle,
                                       TrackingNumber = x.TrackingNumber
                                   })
                                       .ToListAsync().ConfigureAwait(false);

        }
        public List<AccDocumentItemDTO> GetGroupedGL()
        {
            //var originalCulture = Thread.CurrentThread.CurrentCulture;
            //            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //            var q = $@"SELECT ROW_NUMBER() OVER(ORDER BY gl.GLCode ASC) AS AccRow,gl.GLCode AS Code,SUM(di.Debit) AS SumDebit,SUM(di.Credit) SumCredit FROM dbo.AccDocumentItems AS di
            //INNER JOIN dbo.AccDocumentHeaders AS h ON h.AccDocumentHeaderId = di.AccDocumentHeaderId
            //INNER JOIN dbo.SLs AS sl ON sl.SLId = di.SLId
            //INNER JOIN dbo.TLs AS tl ON tl.TLId = sl.TLId
            //INNER JOIN dbo.GLs AS gl ON gl.GLId = tl.GLId
            //WHERE h.DocumentNumber BETWEEN {accDocumentHeaderFilter.FromNumber} AND {accDocumentHeaderFilter.ToNumber}  AND  di.TrackingDate BETWEEN '{accDocumentHeaderFilter.FromDate.ToShortDateString()}' AND '{accDocumentHeaderFilter.ToDate.ToShortDateString()}' 
            //GROUP BY gl.GLCode";



            //            Thread.CurrentThread.CurrentCulture = originalCulture;
            //            var accDocumentItems =await _uow.Database.SqlQuery<AccDocumentItemDTO>(q).ToListAsync().ConfigureAwait(false);

            var accDocumentItems = ToAccDocumentItemDTO(_accDocumentItems.Where(x => x.GLId.HasValue).GroupBy(x =>new IdCode { Id = x.GLId.Value ,Code=x.GLCode}));
            //.Select((x, i) =>
            //new AccDocumentItemDTO
            //{
            //    AccRow = i + 1,
            //    Code = x.Key,
            //    SumDebit = x.Where(y => y.TypeDocumentId != 3 || accDocumentHeaderFilter.HasOpening == false).Sum(y => y.Debit),
            //    SumCredit = x.Where(y => y.TypeDocumentId != 3 || accDocumentHeaderFilter.HasOpening == false).Sum(y => y.Credit),
            //    OpeningSumDebit = x.Where(y => y.TypeDocumentId == 3 && accDocumentHeaderFilter.HasOpening == true).Sum(y => y.Debit),
            //    OpeningSumCredit = x.Where(y => y.TypeDocumentId == 3 && accDocumentHeaderFilter.HasOpening == true).Sum(y => y.Credit),
            //}).ToList();
            return accDocumentItems;

        }
        public List<AccDocumentItemDTO> GetGroupedTL()
        {
            return ToAccDocumentItemDTO(_accDocumentItems.Where(x => x.TLId.HasValue).GroupBy(x =>new IdCode {Id= x.TLId.Value,Code=x.TLCode } ));
        }

        public List<AccDocumentItemDTO> GetGroupedSL()
        {
            //_accDocumentItems = _accDocumentItems.Where(x => x.SLCode == AccDocumentHeaderFilter.SLCode || _accDocumentHeaderFilter.SLCode == 0).ToList();
            var accDocumentItems = ToAccDocumentItemDTO(_accDocumentItems.Where(x => x.SLId.HasValue).GroupBy(x =>new IdCode {Id= x.SLId.Value,Code=x.SLCode }));
            return accDocumentItems;
        }

        public List<AccDocumentItemDTO> GetGroupedDL1()
        {
           // _accDocumentItems = _accDocumentItems.Where(x => x.DL1Code == AccDocumentHeaderFilter.DL1Code || _accDocumentHeaderFilter.DL1Code == 0).ToList();
            var accDocumentItems = ToAccDocumentItemDTO(_accDocumentItems.Where(x => x.DL1Id.HasValue).GroupBy(x =>new IdCode {Id= x.DL1Id.Value,Code= x.DL1Code.Value } ));
            return accDocumentItems;
        }

        public List<AccDocumentItemDTO> GetGroupedDL2()
        {
            //_accDocumentItems = _accDocumentItems.Where(x => x.DL2Code == AccDocumentHeaderFilter.DL2Code || _accDocumentHeaderFilter.DL2Code == 0).ToList();
            var accDocumentItems = ToAccDocumentItemDTO(_accDocumentItems.Where(x => x.DL2Id.HasValue).GroupBy(x =>new IdCode { Id = x.DL2Id.Value, Code = x.DL2Code .Value}));
            return accDocumentItems;
        }

        public List<AccDocumentItemDTO> GetGroupedCurrency()
        {
            var accDocumentItems = ToAccDocumentItemDTO(_accDocumentItems.Where(x => x.CurrencyId.HasValue).GroupBy(x =>new IdCode { Id = x.CurrencyId.Value }));
            return accDocumentItems;
        }

        public List<AccDocumentItemDTO> GetGroupedTracking()
        {
            var accDocumentItems = ToAccDocumentItemDTO(_accDocumentItems.Where(x=>x.TrackingNumber.HasValue).GroupBy(x =>new IdCode { Code = x.TrackingNumber.Value }));
            return accDocumentItems;
        }

        public List<AccDocumentItemDTO> GetDetailedGL()
        {
            List<AccDocumentItemDTO1> temp = FilterByCode();
            return ToAccDocumentItemDTO(temp.GroupBy(x =>new IdCode {Id= x.GLId.Value } ));
        }
        public List<AccDocumentItemDTO> GetDetailedTL()
        {
            List<AccDocumentItemDTO1> temp = FilterByCode();
            return ToAccDocumentItemDTO(temp.GroupBy(x =>new IdCode {Id= x.TLId.Value,Code=x.TLCode } ));
        }

        public List<AccDocumentItemDTO> GetDetailedSL()
        {
            List<AccDocumentItemDTO1> temp = FilterByCode();
            return ToAccDocumentItemDTO(temp.GroupBy(x =>new IdCode {Id= x.SLId.Value,Code=x.SLCode }));
        }
        public List<AccDocumentItemDTO> GetDetailedDL1()
        {
            List<AccDocumentItemDTO1> temp = FilterByCode();
            return ToAccDocumentItemDTO(temp.Where(x=>x.DL1Id.HasValue).GroupBy(x =>new IdCode {Id= x.DL1Id.Value,Code=x.DL1Code.Value }));
        }

        public List<AccDocumentItemDTO> GetDetailedDL2()
        {
            List<AccDocumentItemDTO1> temp = FilterByCode();
            return ToAccDocumentItemDTO(temp.Where(x=>x.DL2Id.HasValue).GroupBy(x =>new IdCode {Id= x.DL2Id.Value,Code=x.DL2Code.Value }));
        }


        public List<AccDocumentItemDTO> GetDetailedCurrency()
        {
            List<AccDocumentItemDTO1> temp = FilterByCode();
            return ToAccDocumentItemDTO(temp.Where(x=>x.CurrencyId.HasValue).GroupBy(x =>new IdCode {Id= x.CurrencyId.Value,Code=x.CurrencyId.Value }));
        }

        public List<AccDocumentItemDTO> GetDetailedTracking()
        {
            List<AccDocumentItemDTO1> temp = FilterByCode();
            return ToAccDocumentItemDTO(temp.Where(x=>x.TrackingNumber.HasValue).GroupBy(x =>new IdCode {Code= x.TrackingNumber.Value }));
        }
        private List<AccDocumentItemDTO1> FilterByCode()
        {
            var temp = _accDocumentItems.Where(x =>
            (AccDocumentHeaderFilter.CurrentMethodName == "OnGLGrouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnGLDetailed") && x.GLCode == AccDocumentHeaderFilter.Code ||
            (AccDocumentHeaderFilter.CurrentMethodName == "OnTLGrouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnTLDetailed") && x.TLCode == AccDocumentHeaderFilter.Code ||
            (AccDocumentHeaderFilter.CurrentMethodName == "OnSLGrouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnSLDetailed") && x.SLCode == AccDocumentHeaderFilter.Code ||
            (AccDocumentHeaderFilter.CurrentMethodName == "OnDL1Grouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnDL1Detailed") && x.DL1Code == AccDocumentHeaderFilter.Code ||
            (AccDocumentHeaderFilter.CurrentMethodName == "OnDL2Grouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnDL2Detailed") && x.DL2Code == AccDocumentHeaderFilter.Code ||
            (AccDocumentHeaderFilter.CurrentMethodName == "OnCurrencyGrouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnCurrencyDetailed") && x.CurrencyId == AccDocumentHeaderFilter.Code ||
            (AccDocumentHeaderFilter.CurrentMethodName == "OnTrackingGrouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnTrackingDetailed") && x.TrackingNumber == AccDocumentHeaderFilter.Code
            ).ToList();
            return temp;
        }
        private List<AccDocumentItemDTO> ToAccDocumentItemDTO(List<AccDocumentItem> accDocumentItems)
        {
            return accDocumentItems.Select(x => new AccDocumentItemDTO { }).ToList();
        }

        private List<AccDocumentItemDTO> ToAccDocumentItemDTO(IEnumerable<IGrouping<IdCode, AccDocumentItemDTO1>> accDocumentItems)
        {
            return accDocumentItems

                .Select((x, i) =>
                new AccDocumentItemDTO
                {
                    AccRow = i + 1,
                    Id=x.Key.Id,
                    Code = x.Key.Code,
                    SumDebit = x.Where(y => y.TypeDocumentId != 3 || AccDocumentHeaderFilter.HasOpening == false).Sum(y => y.Debit),
                    SumCredit = x.Where(y => y.TypeDocumentId != 3 || AccDocumentHeaderFilter.HasOpening == false).Sum(y => y.Credit),
                    OpeningSumDebit = x.Where(y => y.TypeDocumentId == 3 && AccDocumentHeaderFilter.HasOpening == true).Sum(y => y.Debit),
                    OpeningSumCredit = x.Where(y => y.TypeDocumentId == 3 && AccDocumentHeaderFilter.HasOpening == true).Sum(y => y.Credit),
                }).ToList();

        }
        #endregion
    }
    public class IdCode
    {
        public int Id { get; set; }
        public long Code { get; set; }
    }
}
