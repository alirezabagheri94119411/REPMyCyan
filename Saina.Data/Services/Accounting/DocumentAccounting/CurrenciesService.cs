using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Data.Services.Accounting.DocumentAccounting
{
    /// <summary>
    /// تعریف سرویس ارز
    /// </summary>
   public class CurrenciesService: ICurrenciesService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public CurrenciesService(SainaDbContext uow, IAppContextService appContextService)
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
        public async Task<List<Currency>> GetCurrenciesAsync()
        {
            return await _uow.Set<Currency>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async Task<IEnumerable<CurrencyExchangesDTO>> GetCurrencies2Async()
        {
            return await _uow.Set<ExchangeRate>().AsNoTracking().GroupBy(x=>new { x.CurrencyId ,x.Currency}).Select(x=>new CurrencyExchangesDTO { CurrencyId=x.Key.CurrencyId, CurrencyTitle=x.Key.Currency.CurrencyTitle, ParityRate=x.OrderByDescending(y=>y.CurrencyId).Select(y=>y.ParityRate).FirstOrDefault() }).ToListAsync().ConfigureAwait(false);
        }

        public async Task<Currency> GetCurrencyAsync(int id)
        {
            return await _uow.Set<Currency>().FirstOrDefaultAsync(c => (c.CurrencyId == id)).ConfigureAwait(false);
        }
        public async Task<Currency> AddCurrencyAsync(Currency currency)
        {
            _uow.Currencies.Add(currency);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return currency;
        }
        public async Task<Currency> UpdateCurrencyAsync(Currency currency)
        {
            //if (!_uow.Set<Currency>().Local.Any(c => c.CurrencyId == currency.CurrencyId))
            ////{
            //_uow.Set<Currency>().Attach(currency);
            ////}
            ////_uow.Entry(currency).State = EntityState.Modified;
            //await _uow.SaveChangesAsync();
            //var cmd = $"EXEC Currency_Update @CurrencyId = {currency.CurrencyId}," +
            //   $" @CurrencyTitle = N'{currency.CurrencyTitle}'," +
            //   $" @CurrencyTitle2 =N' {currency.CurrencyTitle2}'," +
            //   $" @ExchangeUnit = {currency.ExchangeUnit}," +
            //   $" @NumberDecimal = {currency.NumberDecimal}," +
            //   $" @TitleDecimal = N'{currency.TitleDecimal}'," +
            //   $" @TitleDecimal2 = N'{currency.TitleDecimal2}'";

            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(currency).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return currency;
        }
        public async Task DeleteCurrencyAsync(int currencyId)
        {
            var currency = _uow.Set<Currency>().FirstOrDefault(c => c.CurrencyId == currencyId);
            if (currency != null)
            {
                _uow.Set<Currency>().Remove(currency);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<bool> HasFarsiName(string title)
        {
            return await _uow.Currencies.AnyAsync(x => x.CurrencyTitle == title).ConfigureAwait(false);
        }

        public async Task<bool> HasEnglishName(string title2)
        {
            return await _uow.Currencies.AnyAsync(x => x.CurrencyTitle == title2).ConfigureAwait(false);

        }

        public async Task<bool> GetExchangeRateAsync(int id)
        {
            return await _uow.ExchangeRates.AnyAsync(x => x.CurrencyId == id).ConfigureAwait(false);

        }
        #endregion
    }

}
