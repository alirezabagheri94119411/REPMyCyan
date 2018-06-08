using Saina.ApplicationCore.Entities.Commerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.Commerce
{
   public interface IStocksService
    {
        Task<List<Stock>> GetStocksAsync();
        Task<Stock> GetStockAsync(int id);
        Task<Stock> AddStockAsync(Stock stock);
        Task<Stock> UpdateStockAsync(Stock stock);
        Task DeleteStockAsync(int stockId);
        bool ContextHasChanges { get; }
        Task<bool> Hasduplicate(string code);
        Task<bool> HasTitle(string title);
    }
}
