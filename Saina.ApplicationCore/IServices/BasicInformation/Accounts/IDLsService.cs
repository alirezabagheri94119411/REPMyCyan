using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Accounts
{
    /// <summary>
    ///اینترفیس حساب تفصیل  
    /// </summary>
    public interface IDLsService
    {
        Task<List<DL>> GetDLsActiveAsync();

        Task<List<DL>> GetDLsAsync();
        Task<DL> GetDLAsync(int id);
        Task<DL> AddDLAsync(DL dl);
        Task<DL> UpdateDLAsync(DL dl);
        Task DeleteDLAsync(int dlId);
        bool ContextHasChanges { get; }
        bool Hasduplicate(int code, int id);
        bool HasTitle(string title, int id);
        int GetLastDLCode();


    }
}
