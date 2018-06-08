
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation.Information;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Data.Services.BasicInformation.Information
{
    /// <summary>
    /// سرویس حساب های بانکی
    /// </summary>
    public class BankAccountsService : IBankAccountsService
    {

        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public BankAccountsService(SainaDbContext uow, IAppContextService appContextService)
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
       
        public async Task<List<BankAccount>> GetBankAccountsAsync()
        {
            return await _uow.Set<BankAccount>().Include(x => x.CurrencyType).AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async Task<BankAccount> GetBankAccountAsync(int id)
        {
            return await _uow.Set<BankAccount>().FirstOrDefaultAsync(c => (c.BankAccountId == id)).ConfigureAwait(false);
        }
        public async Task<BankAccount> AddBankAccountAsync(BankAccount bankAccount)
        {
            _uow.BankAccounts.Add(bankAccount);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return bankAccount;
        }
        public async Task<BankAccount> UpdateBankAccountAsync(BankAccount bankAccount)
        {
            //CultureInfo us = new CultureInfo("en-us");

            //var cmd = $"EXEC BankAccount_Update @BankAccountId = {bankAccount.BankAccountId}," +
            //    $" @AccountNumber = {bankAccount.AccountNumber}," +

            //    $" @BankId = {(object)bankAccount.BankId ?? "null"}," +
            //    $" @Branch = N'{bankAccount.Branch}'," +
            //    $" @AccountTypeId = {bankAccount.AccountTypeId}," +

            //    $" @ShabaNumber = N'{bankAccount.ShabaNumber}'," +
            //    $" @Addrress = N'{bankAccount.Addrress}'," +

            //    $" @PostalCode = {bankAccount.PostalCode}," +
            //    $" @CardNumber = {bankAccount.CardNumber}," +

            //    $" @PoseId = N'{bankAccount.PoseId}'," +
            //    $" @WebSite = N'{bankAccount.WebSite}'," +

            //    $" @OpeningDate = '{bankAccount.OpeningDate.ToString(us)}'," +
            //    $" @FirstReservePeriod ={bankAccount.FirstReservePeriod}," +
            //    $" @InventoryReservation = {bankAccount.InventoryReservation}," +

            //    $" @CurrencyId = {(object)bankAccount.CurrencyId ?? "null"}," +
            //    $" @ExchangeRate = N'{bankAccount.ExchangeRate}'," +
            //    $" @Status = {Convert.ToInt16(bankAccount.Status)}";






            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(bankAccount).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return bankAccount;
        }
        public async Task DeleteBankAccountAsync(int bankAccountId)
        {
            var bankAccount = _uow.Set<BankAccount>().FirstOrDefault(c => c.BankAccountId == bankAccountId);
            if (bankAccount != null)
            {
                _uow.Set<BankAccount>().Remove(bankAccount);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }
        #endregion

    }


}
