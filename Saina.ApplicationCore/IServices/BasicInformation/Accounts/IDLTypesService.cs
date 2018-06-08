using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Accounts
{
    /// <summary>
    /// اینترفیس نوع حساب تفصیل
    /// </summary>
   public interface IDLTypesService
    {
        Task<List<DLType>> GetDLTypesAsync();
        Task<DLType> GetDLTypeAsync(int id);
        Task<DLType> GetDLTypeIdAsync(int id);
        Task<DLType> AddDLTypeAsync(DLType dLType);
        Task<DLType> UpdateDLTypeAsync(DLType dLType);
        Task DeleteDLTypeAsync(int dLTypeId);
        bool ContextHasChanges { get; }
    }
}
