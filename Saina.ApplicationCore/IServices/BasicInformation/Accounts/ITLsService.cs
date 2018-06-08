using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Accounts
{
    /// <summary>
    ///اینترفیس حساب کل  
    /// </summary>
    public interface ITLsService
    {
        Task<List<TL>> GetTLsAsync();
        Task<List<TL>> GetTLsActiveAsync();
        Task<TL> GetTLAsync(int id);
        Task<TL> AddTLAsync(TL tl);
        Task<TL> UpdateTLAsync(TL tl);
        Task DeleteTLAsync(int tlId);
        bool ContextHasChanges { get; }
        bool HasTitle(string title, int id);
        bool Hasduplicate(long code, int id);
        long GetLastTLCode(int glId);
        Task<bool> GetSLAsync(int id);
        Task<bool> HasduplicateTree(long code, int id);
        Task<bool> HasTitleTree(string title, int id);
        List<TL> GetTL();
    }
}
