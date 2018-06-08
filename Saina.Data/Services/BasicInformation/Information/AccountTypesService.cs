using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation.Information;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Data.Services.BasicInformation.Information
{
   public class AccountTypesService: IAccountTypesService
    {
            #region Constructors
            /// <summary>
            /// سازنده کلاس
            /// </summary>
            /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
            public AccountTypesService(SainaDbContext uow, IAppContextService appContextService)
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
            public async  Task<List<AccountType>> GetAccountTypesAsync()
            {
                return await  _uow.Set<AccountType>().AsNoTracking().ToListAsync().ConfigureAwait(false);
            }
            public async  Task<AccountType> GetAccountTypeAsync(int id)
            {
                return await  _uow.Set<AccountType>().FirstOrDefaultAsync(c => (c.AccountTypeId == id)).ConfigureAwait(false);
            }
            public async Task<AccountType> AddAccountTypeAsync(AccountType accountType)
            {
                _uow.AccountTypes.Add(accountType);
                await _uow.SaveChangesAsync().ConfigureAwait(false);
                return accountType;
            }
            public async Task<AccountType> UpdateAccountTypeAsync(AccountType accountType)
            {

            //var cmd = $"EXEC AccountType_Update @AccountTypeId = {accountType.AccountTypeId}," +
            //    $" @AccountTypeTitle = N'{accountType.AccountTypeTitle}'";
            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(accountType).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return accountType;
            }
            public async Task DeleteAccountTypeAsync(int accountTypeId)
            {
                var accountType = _uow.Set<AccountType>().FirstOrDefault(c => c.AccountTypeId == accountTypeId);
                if (accountType != null)
                {
                    _uow.Set<AccountType>().Remove(accountType);
                }
                await _uow.SaveChangesAsync().ConfigureAwait(false);
            }
            #endregion
    }
}
