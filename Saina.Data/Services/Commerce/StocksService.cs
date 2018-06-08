using Saina.ApplicationCore.Entities.Commerce;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.Commerce;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Data.Services.Commerce
{
    /// <summary>
    /// سرویس انبار
    /// </summary>
   public class StocksService: IStocksService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public StocksService(SainaDbContext uow, IAppContextService appContextService)
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
        public async  Task<List<Stock>> GetStocksAsync()
        {
            return await  _uow.Set<Stock>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async  Task<Stock> GetStockAsync(int id)
        {
            return await  _uow.Set<Stock>().FirstOrDefaultAsync(c => (c.StockId == id)).ConfigureAwait(false);
        }
        public async Task<Stock> AddStockAsync(Stock stock)
        {
            _uow.Stocks.Add(stock);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return stock;
        }
        public async Task<Stock> UpdateStockAsync(Stock stock)
        {

            //      var cmd = $"EXEC Stock_Update @StockId = {stock.StockId}," +
            //$" @StockCode = {stock.StockCode}," +
            //$" @StockTitle1 = N'{stock.StockTitle1}'," +
            //$" @StockTitle2 = N'{stock.StockTitle2}'," +
            //$" @UserId ={(object)stock.UserId ?? "null"}," +
            //$" @Phone = N'{stock.Phone}'," +
            //$" @Address = N'{stock.Address}'," +
            //$" @ProductId = {(object)stock.ProductId ?? "null"}," +
            //$" @SLId = {(object)stock.SLId ?? "null"}";

            //      await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(stock).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return stock;
        }
        public async Task DeleteStockAsync(int stockId)
        {
            var stock = _uow.Set<Stock>().FirstOrDefault(c => c.StockId == stockId);
            if (stock != null)
            {
                _uow.Set<Stock>().Remove(stock);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }
        public async Task<bool> HasTitle(string title)
        {
            return await _uow.Stocks.AnyAsync(x => x.StockTitle1 == title);
        }


        public async Task<bool> Hasduplicate(string code)
        {
            return await _uow.Stocks.AnyAsync(x => x.StockCode == code).ConfigureAwait(false);
        }

      
        #endregion
    }
}
