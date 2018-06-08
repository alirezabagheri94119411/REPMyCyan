using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation.Accounts
{
    /// <summary>
    /// اینترفیس انتخاب عامل
    /// </summary>
   public interface ISelectAgentsService
    {
        Task<List<SelectAgent>> GetSelectAgentsAsync();
        Task<SelectAgent> GetSelectAgentAsync(int id);
        Task<SelectAgent> AddSelectAgentAsync(SelectAgent selectAgent);
        Task<SelectAgent> UpdateSelectAgentAsync(SelectAgent selectAgent);
        Task DeleteSelectAgentAsync(int selectAgentId);
        bool ContextHasChanges { get; }
    }
}
