using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.Accounting.DocumentAccounting
{
    /// <summary>
    /// تعریف نرخ ارز
    /// </summary>
   public interface IExchangeRatesService
    {
        Task<List<ExchangeRate>> GetExchangeRatesAsync();
        Task<ExchangeRate> GetExchangeRateAsync(int id);
        Task<ExchangeRate> AddExchangeRateAsync(ExchangeRate exchangeRate);
        Task<ExchangeRate> UpdateExchangeRateAsync(ExchangeRate exchangeRate);
        Task DeleteExchangeRateAsync(int exchangeRateId);
        bool ContextHasChanges { get; }
    }
}
