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
    /// سرویس  درخت حساب ها
    /// </summary>
   public class TreeAccountsService: ITreeAccountsService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public TreeAccountsService(SainaDbContext uow, IAppContextService appContextService)
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
        public async Task<List<TreeAccount>> GetTreeAccountsAsync()
        {
            return await _uow.Set<TreeAccount>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async Task<TreeAccount> GetTreeAccountAsync(int id)
        {
            return await _uow.Set<TreeAccount>().FirstOrDefaultAsync(c => (c.TreeAccountId == id)).ConfigureAwait(false);
        }
        public async Task<TreeAccount> AddTreeAccountAsync(TreeAccount treeAccount)
        {
            _uow.TreeAccounts.Add(treeAccount);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return treeAccount;
        }
        public async Task<TreeAccount> UpdateTreeAccountAsync(TreeAccount treeAccount)
        {

            //var cmd = $"EXEC TreeAccount_Update @TreeAccountId = {treeAccount.TreeAccountId}," +
            //   $" @GroupCode = {treeAccount.GroupCode}," +
            //   $" @GroupName =N' {treeAccount.GroupName}'," +
            //   $" @TreeAccountType = N'{treeAccount.TreeAccountType}'" ;

            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(treeAccount).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return treeAccount;
        }
        public async Task DeleteTreeAccountAsync(int treeAccountId)
        {
            var treeAccount = _uow.Set<TreeAccount>().FirstOrDefault(c => c.TreeAccountId == treeAccountId);
            if (treeAccount != null)
            {
                _uow.Set<TreeAccount>().Remove(treeAccount);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }
        #endregion
    }
}
