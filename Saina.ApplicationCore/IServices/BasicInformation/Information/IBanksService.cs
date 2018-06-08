using Saina.ApplicationCore.Entities.BasicInformation.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Information
{
    /// <summary>
    /// اطلاعات بانک
    /// </summary>
   public interface IBanksService
    {
        Task<List<Bank>> GetBanksAsync();
        Task<Bank> GetBankAsync(int id);
        Task<Bank> AddBankAsync(Bank bank);
        Task<Bank> UpdateBankAsync(Bank bank);
        Task DeleteBankAsync(int bankId);
        bool ContextHasChanges { get; }
        Task<bool> HasBankName1(string bankName1);
        Task<bool> GetBankAccountAsync(int id);


    }
}
