
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Information
{
    /// <summary>
    /// اطلاعات حساب بانکی
    /// </summary>
   public interface IBankAccountsService
    {
       Task<List<BankAccount>> GetBankAccountsAsync();
       // Task<List<BankAccountList>> GetBankAccountsAsync();
        Task<BankAccount> GetBankAccountAsync(int id);
        Task<BankAccount> AddBankAccountAsync(BankAccount bankAccount);
        Task<BankAccount> UpdateBankAccountAsync(BankAccount bankAccount);
        Task DeleteBankAccountAsync(int bankAccountId);
        bool ContextHasChanges { get; }
    }
}
