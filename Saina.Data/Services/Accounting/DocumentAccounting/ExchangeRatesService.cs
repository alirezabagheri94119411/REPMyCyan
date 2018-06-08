
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
using System.Threading.Tasks;

namespace Saina.Data.Services.Accounting.DocumentAccounting
{
    /// <summary>
    /// سرویس نرخ ارز
    /// </summary>
   public class ExchangeRatesService: IExchangeRatesService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public ExchangeRatesService(SainaDbContext uow, IAppContextService appContextService)
        {
            _uow = uow;
            _appContextService = appContextService;
            _currencies = _uow.Set<Currency>();
        }
        #endregion
        #region Fields
        SainaDbContext _uow;
        readonly IAppContextService _appContextService;
        private IDbSet<Currency> _currencies;
        #endregion
        #region Properties
        public bool ContextHasChanges => _uow.ContextHasChanges;
        #endregion
        #region Methode
        public async Task<List<ExchangeRate>> GetExchangeRatesAsync()
        {
            return await _uow.Set<ExchangeRate>().Include(x=>x.Currency).AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async Task<ExchangeRate> GetExchangeRateAsync(int id)
        {
            return await _uow.Set<ExchangeRate>().FirstOrDefaultAsync(c => (c.ExchangeRateId == id)).ConfigureAwait(false);
        }
        public async Task<ExchangeRate> AddExchangeRateAsync(ExchangeRate exchangeRate)
        {
            _uow.ExchangeRates.Add(exchangeRate);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return exchangeRate;
        }
        public async Task<ExchangeRate> UpdateExchangeRateAsync(ExchangeRate exchangeRate)
        {
            //CultureInfo us = new CultureInfo("en-us");

            //var cmd = $"EXEC ExchangeRate_Update @ExchangeRateId = {exchangeRate.ExchangeRateId}," +
            //$" @CurrencyId = {(object)exchangeRate.CurrencyId ?? "null"} ," +
            //$" @EffectiveDate =N' {exchangeRate.EffectiveDate.ToString(us)}'," +
            //$" @ParityRate = {exchangeRate.ParityRate}," +
            //$" @Description = N'{exchangeRate.Description}',"+
            //$" @ExchangeUnitId = {(object)exchangeRate.ExchangeUnitId ?? "null"} ";
            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(exchangeRate).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return exchangeRate;
        }
        public async Task DeleteExchangeRateAsync(int exchangeRateId)
        {
            var exchangeRate = _uow.Set<ExchangeRate>().FirstOrDefault(c => c.ExchangeRateId == exchangeRateId);
            if (exchangeRate != null)
            {
                _uow.Set<ExchangeRate>().Remove(exchangeRate);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }
        #endregion
    }
}
