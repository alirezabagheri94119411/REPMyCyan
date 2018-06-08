using Saina.ApplicationCore.Entities.BasicInformation.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Information
{
    /// <summary>
    /// اینترفیس نوع بانک
    /// </summary>
   public interface IBankTypesService
    {
        Task<List<BankType>> GetBankTypesAsync();
        Task<BankType> GetBankTypeAsync(int id);
        Task<BankType> AddBankTypeAsync(BankType bankType);
        Task<BankType> UpdateBankTypeAsync(BankType bankType);
        Task DeleteBankTypeAsync(int bankTypeId);
        bool ContextHasChanges { get; }
    }
}
