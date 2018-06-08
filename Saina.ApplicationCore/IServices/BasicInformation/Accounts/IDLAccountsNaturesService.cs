using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Accounts
{
   public interface IDLAccountsNaturesService
    {
        Task<List<DLAccountsNature>> GetDLAccountsNaturesAsync();
        Task<DLAccountsNature> GetDLAccountsNatureAsync(int id);
        Task<DLAccountsNature> AddDLAccountsNatureAsync(DLAccountsNature dLAccountsNature);
        Task<DLAccountsNature> UpdateDLAccountsNatureAsync(DLAccountsNature dLAccountsNature);
        Task DeleteDLAccountsNatureAsync(int dLAccountsNatureId);
        bool ContextHasChanges { get; }
    }
}
