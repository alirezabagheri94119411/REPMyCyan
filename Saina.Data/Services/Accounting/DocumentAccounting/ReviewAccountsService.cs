using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.DTOs.Accounting.ReviewAcc;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.Data.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Threading.Tasks;

namespace Saina.Data.Services.Accounting.DocumentAccounting
{
    /// <summary>
    /// مرور حساب ها
    /// </summary>
    public class ReviewAccountsService : IReviewAccountsService
    {
        #region Constructors

        public ReviewAccountsService(SainaDbContext uow)
        {
            _uow = uow;
        }
        #endregion
        #region Fields
        SainaDbContext _uow;
        private List<AccDocumentItemDTO1> _accDocumentItems;
        #endregion
        #region Properties
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
        public async Task<List<AccDocumentItemDTO>> GetGroupedGLAsync()
        {
            //var originalCulture = Thread.CurrentThread.CurrentCulture;
            //            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //            var q = $@"SELECT ROW_NUMBER() OVER(ORDER BY gl.GLCode ASC) AS AccRow,gl.GLCode AS Code,SUM(di.Debit) AS SumDebit,SUM(di.Credit) SumCredit FROM Acc.AccDocumentItems AS di
            //INNER JOIN Acc.AccDocumentHeaders AS h ON h.AccDocumentHeaderId = di.AccDocumentHeaderId
            //INNER JOIN Info.SLs AS sl ON sl.SLId = di.SLId
            //INNER JOIN Info.TLs AS tl ON tl.TLId = sl.TLId
            //INNER JOIN Info.GLs AS gl ON gl.GLId = tl.GLId
            //WHERE h.DocumentNumber BETWEEN {accDocumentHeaderFilter.FromNumber} AND {accDocumentHeaderFilter.ToNumber}  AND  di.TrackingDate BETWEEN '{accDocumentHeaderFilter.FromDate.ToShortDateString()}' AND '{accDocumentHeaderFilter.ToDate.ToShortDateString()}' 
            //GROUP BY gl.GLCode";



            //            Thread.CurrentThread.CurrentCulture = originalCulture;
            //            var accDocumentItems =await _uow.Database.SqlQuery<AccDocumentItemDTO>(q).ToListAsync().ConfigureAwait(false);

            //var accDocumentItems = ToAccDocumentItemDTOOld(_accDocumentItems.GroupBy(x => x.GLCode));
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




            //var accDocumentItems1 = _uow.AccDocumentItems.AsNoTracking()
            //        .Include(x => x.AccDocumentHeader).Include(x => x.Currency).Include(x => x.DL1).Include(x => x.DL2).Include(x => x.SL).Include(x => x.SL.TL).Include(x => x.SL.TL.GL)
            //         .Where(x => x.AccDocumentHeader.DocumentNumber >= _accDocumentHeaderFilter.FromNumber
            //         && x.AccDocumentHeader.DocumentNumber <= _accDocumentHeaderFilter.ToNumber
            //         && x.AccDocumentHeader.DocumentDate >= _accDocumentHeaderFilter.FromDate
            //         && x.AccDocumentHeader.DocumentDate <= _accDocumentHeaderFilter.ToDate);
            //var queryableAccDocumentItems = ApplyFilter();

            //var groupedAccDocumentItems = ApplyFilter().GroupBy(x => new Grouped { Id = x.SL.TL.GLId, Code = x.SL.TL.GL.GLCode, Title = x.SL.TL.GL.Title });
            //var accDocumentItems=ToAccDocumentItemDTO(ApplyFilter().GroupBy(x => new Grouped { Id = x.SL.TL.GLId, Code = x.SL.TL.GL.GLCode, Title = x.SL.TL.GL.Title }));

            if (!ApplyFilter().Any()) return new List<AccDocumentItemDTO>();
            var z= ToAccDocumentItemDTO(ApplyFilter().GroupBy(x => new Grouped { Id = x.SL.TL.GLId, Code = x.SL.TL.GL.GLCode, Title = x.SL.TL.GL.Title }));
            return await z;
            //      return await ToAccDocumentItemDTO(ApplyFilter().GroupBy(x => new Grouped { Id = x.SL.TL.GLId, Code = x.SL.TL.GL.GLCode, Title = x.SL.TL.GL.Title }));

        }
        public async Task<List<AccDocumentItemDTO>> GetGroupedTLAsync()
        {
            if (!ApplyFilter().Any()) return new List<AccDocumentItemDTO>();
            return await ToAccDocumentItemDTO(ApplyFilter().GroupBy(x => new Grouped { Id = x.SL.TLId, Code = x.SL.TL.TLCode, Title = x.SL.TL.Title }));
        }
        public async Task<List<AccDocumentItemDTO>> GetGroupedSLAsync()
        {
            if (!ApplyFilter().Any()) return new List<AccDocumentItemDTO>();
            return await ToAccDocumentItemDTO(ApplyFilter().GroupBy(x => new Grouped { Id = x.SLId, Code = x.SL.SLCode, Title = x.SL.Title }));
        }
        public async Task<List<AccDocumentItemDTO>> GetGroupedDL1Async()
        {
            if (!ApplyFilter().Any(x=>x.DL1Id!=null)) return new List<AccDocumentItemDTO>();
            var z = ApplyFilter().Where(x => x.DL1Id != null).GroupBy(x => new Grouped { Id = x.DL1Id, Code = x.DL1.DLCode, Title = x.DL1.Title }).ToList();
            if (z.Count > 0)
            {
                return await ToAccDocumentItemDTO(ApplyFilter().Where(x => x.DL1Id != null).GroupBy(x => new Grouped { Id = x.DL1Id, Code = x.DL1.DLCode, Title = x.DL1.Title }));
            }
            else
            {
                return new List<AccDocumentItemDTO>();
            }
        }
        public async Task<List<AccDocumentItemDTO>> GetGroupedDL2Async()
        {
            if (!ApplyFilter().Any(x=>x.DL2Id!=null)) return new List<AccDocumentItemDTO>();
            var z = ApplyFilter().Where(x => x.DL2Id != null).GroupBy(x => new Grouped { Id = x.DL2Id, Code = x.DL2.DLCode, Title = x.DL2.Title }).ToList();
            if (z.Count > 0)
            {
                return await ToAccDocumentItemDTO(ApplyFilter().Where(x => x.DL2Id != null).GroupBy(x => new Grouped { Id = x.DL2Id, Code = x.DL2.DLCode, Title = x.DL2.Title }));
            }
            else
            {
                return new List<AccDocumentItemDTO>();
            }
        }
        public async Task<List<AccDocumentItemDTO>> GetGroupedCurrencyAsync()
        {
            if (!ApplyFilter().Any(x => x.CurrencyId != null)) return new List<AccDocumentItemDTO>();
            var z = ApplyFilter().Where(x => x.CurrencyId != null).GroupBy(x => new GroupedCur { Id = x.CurrencyId, CurrencyAmount = x.CurrencyAmount, Code = 0, Title = x.Currency.CurrencyTitle }).ToList();
            if (z.Count > 0)
            {
                return await ToAccDocumentItemDTOCur(ApplyFilter().Where(x => x.CurrencyId != null).GroupBy(x => new GroupedCur { Id = x.CurrencyId, CurrencyAmount = x.CurrencyAmount, Code = 0, Title = x.Currency.CurrencyTitle }));
            }
            else
            {
                return new List<AccDocumentItemDTO>();
            }
        }
        public async Task<List<AccDocumentItemDTO>> GetGroupedTrackingAsync()
        {
            if (await ApplyFilter().AnyAsync(x => x.TrackingNumber != 0) != true)
                return new List<AccDocumentItemDTO>();
            return await ToAccDocumentItemDTO(ApplyFilter().Where(x => x.TrackingNumber != 0).GroupBy(x => new Grouped { Id = (int)x.TrackingNumber,Title=x.TrackingDate.ToString(), Code = x.TrackingNumber }));
        }
        public async Task<List<AccDocumentItemDTO>> GetDetailedGLAsync()
        {
            if (!ApplyFilter().Any()) return new List<AccDocumentItemDTO>();
            return await ToAccDocumentItemDTO(FilterDetail(ApplyFilter()).GroupBy(x => new Grouped { Id = x.SL.TL.GLId, Code = x.SL.TL.GL.GLCode, Title = x.SL.TL.GL.Title }));
        }
        public async Task<List<AccDocumentItemDTO>> GetDetailedTLAsync()
        {
            if (!ApplyFilter().Any()) return new List<AccDocumentItemDTO>();
            return await ToAccDocumentItemDTO(FilterDetail(ApplyFilter()).GroupBy(x => new Grouped { Id = x.SL.TLId, Code = x.SL.TL.TLCode, Title = x.SL.TL.Title }));
        }
        public async Task<List<AccDocumentItemDTO>> GetDetailedSLAsync()
        {
            if (!ApplyFilter().Any()) return new List<AccDocumentItemDTO>();
            return await ToAccDocumentItemDTO(FilterDetail(ApplyFilter()).GroupBy(x => new Grouped { Id = x.SLId, Code = x.SL.SLCode, Title = x.SL.Title }));
        }
        public async Task<List<AccDocumentItemDTO>> GetDetailedDL1Async()
        {
            
            if (!ApplyFilter().Any(x=>x.DL1Id!=null)) return new List<AccDocumentItemDTO>();
            
                var z= FilterDetail(ApplyFilter()).Where(x => x.DL1Id != null).GroupBy(x => new Grouped { Id = x.DL1Id, Code = x.DL1.DLCode, Title = x.DL1.Title }).ToList();
                if (z.Count > 0)
                {
                    return await ToAccDocumentItemDTO(FilterDetail(ApplyFilter()).Where(x => x.DL1Id != null).GroupBy(x => new Grouped { Id = x.DL1Id, Code = x.DL1.DLCode, Title = x.DL1.Title }));
                }
                else
                {
                    return new List<AccDocumentItemDTO>();
                }
            
        }
        public async Task<List<AccDocumentItemDTO>> GetDetailedDL2Async()
        {
            if (!ApplyFilter().Any(x=>x.DL2Id!=null)) return new List<AccDocumentItemDTO>();

            var z = FilterDetail(ApplyFilter()).Where(x => x.DL2Id != null).GroupBy(x => new Grouped { Id = x.DL2Id, Code = x.DL2.DLCode, Title = x.DL2.Title }).ToList();
            if (z.Count > 0)
            {
                var dl2 = await ToAccDocumentItemDTO(FilterDetail(ApplyFilter()).Where(x => x.DL2Id != null).GroupBy(x => new Grouped { Id = x.DL2Id, Code = x.DL2.DLCode, Title = x.DL2.Title }));
                return dl2;
            }
            else
            {
                return new List<AccDocumentItemDTO>(); 
            }
        }
        public async Task<List<AccDocumentItemDTO>> GetDetailedCurrencyAsync()
        {
            if (!ApplyFilter().Any(x => x.CurrencyId != null)) return new List<AccDocumentItemDTO>();

            var z = FilterDetail(ApplyFilter()).Where(x => x.CurrencyId != null).GroupBy(x => new GroupedCur { Id = x.CurrencyId, Code = 0, CurrencyAmount = x.CurrencyAmount, Title = x.Currency.CurrencyTitle }).ToList();
            if (z.Count > 0)
            {
                return await ToAccDocumentItemDTOCur(FilterDetail(ApplyFilter()).Where(x => x.CurrencyId != null).GroupBy(x => new GroupedCur { Id = x.CurrencyId, Code = 0, CurrencyAmount = x.CurrencyAmount, Title = x.Currency.CurrencyTitle }));
            }
            else
            {
                return new List<AccDocumentItemDTO>();
            }
        }
        public async Task<List<AccDocumentItemDTO>> GetDetailedTrackingAsync()
        {
            if (!ApplyFilter().Any(x => x.TrackingNumber != 0)) return new List<AccDocumentItemDTO>();
            return await ToAccDocumentItemDTO(FilterDetail(ApplyFilter()).Where(x => x.TrackingNumber != 0).GroupBy(x => new Grouped {Id=(int)x.TrackingNumber, Title="", Code = x.TrackingNumber }));
        }
        private IQueryable<AccDocumentItem> ApplyFilter()
        {
            _uow = new SainaDbContext();
            //    var xx= _uow.AccDocumentItems.ToList();
            
            var accDocumentItems = _uow.AccDocumentItems.AsNoTracking()
                                .Include(x => x.AccDocumentHeader).Include(x => x.Currency).Include(x => x.DL1).Include(x => x.DL2).Include(x => x.SL).Include(x => x.SL.TL).Include(x => x.SL.TL.GL)
                                .Where(x => x.AccDocumentHeader.DocumentNumber >= _accDocumentHeaderFilter.FromNumber
                                 && x.AccDocumentHeader.DocumentNumber <= _accDocumentHeaderFilter.ToNumber
                                 && x.AccDocumentHeader.DocumentDate >= _accDocumentHeaderFilter.FromDate
                                 && x.AccDocumentHeader.DocumentDate <= _accDocumentHeaderFilter.ToDate
                                 && (x.AccDocumentHeader.TypeDocumentId & ((int)_accDocumentHeaderFilter.TLDocumentType))== x.AccDocumentHeader.TypeDocumentId
                                 && (_accDocumentHeaderFilter.AccountGLEnum.HasFlag(x.SL.TL.GL.AccountGLEnum ))
                                 &&(x.AccDocumentHeader.Status!=StatusEnum.Draft)
                                 );
            //  AccountGLEnum TLDocumentType          AccDocumentHeaders = new QueryableCollectionView(_uow.AccDocumentHeaders.Include(x => x.AccDocumentItems).Where(x => _uow.ShamsiToMiladi(x.DocumentDate, "Saal") == _appContextService.CurrentFinancialYear.ToString()).ToList());
            //if (accDocumentItems.ToList().Any(x => x.DL1Id == null))
            //{
            //    int y = 0;
            //    var z = accDocumentItems.ToList();
            //}
            return accDocumentItems;
        }

        private IQueryable<AccDocumentItem> FilterDetail(IQueryable<AccDocumentItem> accDocumentItems)
        {
            return accDocumentItems.Where(x =>
            (AccDocumentHeaderFilter.CurrentMethodName == "OnGLGrouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnGLDetailed") && x.SL.TL.GLId == AccDocumentHeaderFilter.Id ||
            (AccDocumentHeaderFilter.CurrentMethodName == "OnTLGrouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnTLDetailed") && x.SL.TLId == AccDocumentHeaderFilter.Id ||
            (AccDocumentHeaderFilter.CurrentMethodName == "OnSLGrouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnSLDetailed") && x.SLId == AccDocumentHeaderFilter.Id ||
            (AccDocumentHeaderFilter.CurrentMethodName == "OnDL1Grouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnDL1Detailed") && x.DL1Id == AccDocumentHeaderFilter.Id ||
            (AccDocumentHeaderFilter.CurrentMethodName == "OnDL2Grouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnDL2Detailed") && x.DL2Id == AccDocumentHeaderFilter.Id ||
            (AccDocumentHeaderFilter.CurrentMethodName == "OnCurrencyGrouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnCurrencyDetailed") && x.CurrencyId == AccDocumentHeaderFilter.Id ||
            (AccDocumentHeaderFilter.CurrentMethodName == "OnTrackingGrouped" || AccDocumentHeaderFilter.CurrentMethodName == "OnTrackingDetailed") && x.TrackingNumber == AccDocumentHeaderFilter.TrackingNumber
            );
        }
        private SL _SL;
        public SL GetSL()
        {
            return _SL;
        }
        private async Task<List<AccDocumentItemDTO>> ToAccDocumentItemDTO(IQueryable<IGrouping<Grouped, AccDocumentItem>> groupedAccDocumentItems)
        {
            List<AccDocumentItemDTO> result = await groupedAccDocumentItems.Select(x=>
   new AccDocumentItemDTO
   {
       Id = x.Key.Id,
       Code = x.Key.Code,
       Title = x.Key.Title,
       SumDebit = !x.Any(y => y.AccDocumentHeader.TypeDocumentId != 3 || AccDocumentHeaderFilter.HasOpening == false) ? 0 :
       x.Where(y => y.AccDocumentHeader.TypeDocumentId != 3 || AccDocumentHeaderFilter.HasOpening == false).Sum(y => y.Debit),
       SumCredit = !x.Any(y => y.AccDocumentHeader.TypeDocumentId != 3 || AccDocumentHeaderFilter.HasOpening == false) ? 0 :
       x.Where(y => y.AccDocumentHeader.TypeDocumentId != 3 || AccDocumentHeaderFilter.HasOpening == false).Sum(y => y.Credit),
       OpeningSumDebit = AccDocumentHeaderFilter.HasOpening == false ? 0 : x.Where(y => y.AccDocumentHeader.TypeDocumentId == 3).Sum(y => y.Debit),
       OpeningSumCredit = AccDocumentHeaderFilter.HasOpening == false ? 0 : x.Where(y => y.AccDocumentHeader.TypeDocumentId == 3).Sum(y => y.Credit),
   }).AsNoTracking().ToListAsync().ConfigureAwait(false);
            _SL = groupedAccDocumentItems.Select(x => x.Select(u => u.SL)).FirstOrDefault().FirstOrDefault();

            return result;
           // AccDocumentHeaders = new QueryableCollectionView(_uow.AccDocumentHeaders.Include(x => x.AccDocumentItems).Where(x => _uow.ShamsiToMiladi(x.DocumentDate, "Saal") == _appContextService.CurrentFinancialYear.ToString()).ToList());

        }
        private async Task<List<AccDocumentItemDTO>> ToAccDocumentItemDTOCur(IQueryable<IGrouping<GroupedCur, AccDocumentItem>> groupedAccDocumentItems)
        {
            List<AccDocumentItemDTO> result = await groupedAccDocumentItems.Select(x =>
   new AccDocumentItemDTO
   {
       Id = x.Key.Id,
       Code = x.Key.Code,
       Title = x.Key.Title,
       CurrencyAmount = x.Key.CurrencyAmount,
       SumDebit = !x.Any(y => y.AccDocumentHeader.TypeDocumentId != 3 || AccDocumentHeaderFilter.HasOpening == false) ? 0 :
       x.Where(y => y.AccDocumentHeader.TypeDocumentId != 3 || AccDocumentHeaderFilter.HasOpening == false).Sum(y => y.Debit),
       SumCredit = !x.Any(y => y.AccDocumentHeader.TypeDocumentId != 3 || AccDocumentHeaderFilter.HasOpening == false) ? 0 :
       x.Where(y => y.AccDocumentHeader.TypeDocumentId != 3 || AccDocumentHeaderFilter.HasOpening == false).Sum(y => y.Credit),
       OpeningSumDebit = AccDocumentHeaderFilter.HasOpening == false ? 0 : x.Where(y => y.AccDocumentHeader.TypeDocumentId == 3).Sum(y => y.Debit),
       OpeningSumCredit = AccDocumentHeaderFilter.HasOpening == false ? 0 : x.Where(y => y.AccDocumentHeader.TypeDocumentId == 3).Sum(y => y.Credit),
   }).AsNoTracking().ToListAsync().ConfigureAwait(false);
            _SL = groupedAccDocumentItems.Select(x => x.Select(u => u.SL)).FirstOrDefault().FirstOrDefault();

            return result;
            // AccDocumentHeaders = new QueryableCollectionView(_uow.AccDocumentHeaders.Include(x => x.AccDocumentItems).Where(x => _uow.ShamsiToMiladi(x.DocumentDate, "Saal") == _appContextService.CurrentFinancialYear.ToString()).ToList());

        }
        #endregion
    }

    internal class Grouped
    {
        public int? Id { get; set; }
        public long Code { get; set; }
        public string Title { get; set; }
    }
    internal class GroupedCur
    {
        public int? Id { get; set; }
        public long Code { get; set; }
        public double? CurrencyAmount { get; set; }
        public string Title { get; set; }
    }
}
