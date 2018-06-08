using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Accounts
{
    /// <summary>
    ///اینترفیس حساب گروه  
    /// </summary>
    public interface IGLsService
    {
            Task<List<GL>> GetGLsAsync();
            Task<GL> GetGLAsync(int id);
        Task<List<GL>> GetGLsActiveAsync();
        Task<GL> AddGLAsync(GL gl);
            Task<GL> UpdateGLAsync(GL gl);
            Task DeleteGLAsync(int glId);
            bool ContextHasChanges { get; }
        bool Hasduplicate(long code, int id);
        Task<bool> HasduplicateTree(long code, int id);
        Task<bool> HasTitleTree(string title, int id);
        bool HasTitle(string title, int id);
        Task<bool> GetTLAsync(int id);
        long GetLastGLCode();

    }
}
