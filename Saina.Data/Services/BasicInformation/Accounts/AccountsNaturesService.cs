using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Data.Services.BasicInformation.Accounts
{
   public class AccountsNaturesService: IAccountsNaturesService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public AccountsNaturesService(SainaDbContext uow, IAppContextService appContextService)
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
        public async  Task<List<AccountsNature>> GetAccountsNaturesAsync()
        {
            return await  _uow.Set<AccountsNature>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async  Task<AccountsNature> GetAccountsNatureAsync(int id)
        {
            return await  _uow.Set<AccountsNature>().FirstOrDefaultAsync(c => (c.AccountsNatureId == id)).ConfigureAwait(false);
        }
        public async Task<AccountsNature> AddAccountsNatureAsync(AccountsNature accountsNature)
        {
            _uow.AccountsNatures.Add(accountsNature);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return accountsNature;
        }
        public async Task<AccountsNature> UpdateAccountsNatureAsync(AccountsNature accountsNature)
        {
            _uow.Entry(accountsNature).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            //var cmd = $"EXEC AccountsNature_Update @AccountsNatureId = {accountsNature.AccountsNatureId}," +
            //  $" @AccountsNatureTitle =N' {accountsNature.AccountsNatureTitle}'";

            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            return accountsNature;
        }
        public async Task DeleteAccountsNatureAsync(int accountsNatureId)
        {
            var accountsNature = _uow.Set<AccountsNature>().FirstOrDefault(c => c.AccountsNatureId == accountsNatureId);
            if (accountsNature != null)
            {
                _uow.Set<AccountsNature>().Remove(accountsNature);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }

       
        #endregion
    }
}
