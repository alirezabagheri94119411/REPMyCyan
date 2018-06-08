using Saina.ApplicationCore.Entities.BasicInformation.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Information
{
    /// <summary>
    /// اینترفیس نوع حساب
    /// </summary>
   public interface IAccountTypesService
    {
        Task<List<AccountType>> GetAccountTypesAsync();
        Task<AccountType> GetAccountTypeAsync(int id);
        Task<AccountType> AddAccountTypeAsync(AccountType accountType);
        Task<AccountType> UpdateAccountTypeAsync(AccountType accountType);
        Task DeleteAccountTypeAsync(int accountTypeId);
        bool ContextHasChanges { get; }
    }
}
