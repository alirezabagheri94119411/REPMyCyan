using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.Accounting.DocumentAccounting
{
    /// <summary>
    /// اینترفیس درخت حساب ها
    /// </summary>
   public interface ITreeAccountsService
    {
        Task<List<TreeAccount>> GetTreeAccountsAsync();
        Task<TreeAccount> GetTreeAccountAsync(int id);
        Task<TreeAccount> AddTreeAccountAsync(TreeAccount treeAccount);
        Task<TreeAccount> UpdateTreeAccountAsync(TreeAccount treeAccount);
        Task DeleteTreeAccountAsync(int treeAccountId);
        bool ContextHasChanges { get; }
    }
}
