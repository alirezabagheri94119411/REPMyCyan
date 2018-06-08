using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.Accounting.DocumentAccounting
{
    /// <summary>
    /// تعریف ارز
    /// </summary>
   public interface ICurrenciesService
    {
        Task<List<Currency>> GetCurrenciesAsync();
        Task<Currency> GetCurrencyAsync(int id);
        Task<Currency> AddCurrencyAsync(Currency currency);
        Task<Currency> UpdateCurrencyAsync(Currency currency);
        Task DeleteCurrencyAsync(int currencyId);
        bool ContextHasChanges { get; }
        Task<bool> HasFarsiName(String title);
        Task<bool> HasEnglishName(String title2);
        Task<bool> GetExchangeRateAsync(int id);
        Task<IEnumerable<CurrencyExchangesDTO>> GetCurrencies2Async();

    }
}
