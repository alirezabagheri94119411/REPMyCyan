using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Accounts
{
  public interface IAccountsNaturesService
    {
        Task<List<AccountsNature>> GetAccountsNaturesAsync();
        Task<AccountsNature> GetAccountsNatureAsync(int id);
        Task<AccountsNature> AddAccountsNatureAsync(AccountsNature accountsNature);
        Task<AccountsNature> UpdateAccountsNatureAsync(AccountsNature accountsNature);
        Task DeleteAccountsNatureAsync(int accountsNatureId);
        bool ContextHasChanges { get; }
    }
}
