using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.Accounting.DocumentAccounting
{
    /// <summary>
    /// اینترفیس روش
    /// </summary>
    public interface IStylesService
    {
        Task<List<Style>> GetStylesAsync();
        Task<Style> GetStyleAsync(int id);
        Task<Style> AddStyleAsync(Style style);
        Task<Style> UpdateStyleAsync(Style style);
        Task DeleteStyleAsync(int styleId);
        bool ContextHasChanges { get; }
    }
}
