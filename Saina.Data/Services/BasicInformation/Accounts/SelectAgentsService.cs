using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Data.Services.BasicInformation.Accounts
{
    /// <summary>
    /// سرویس انتخاب عامل
    /// </summary>
  public class SelectAgentsService: ISelectAgentsService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public SelectAgentsService(SainaDbContext uow, IAppContextService appContextService)
        {
            _uow = uow;
            _appContextService = appContextService;
        }
        #endregion
        #region Fields
        SainaDbContext _uow;
        readonly IAppContextService _appContextService;
        #endregion
        #region Properties
        public bool ContextHasChanges => _uow.ContextHasChanges;
        #endregion
        #region Methode
        public async Task<List<SelectAgent>> GetSelectAgentsAsync()
        {
            return await _uow.Set<SelectAgent>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async Task<SelectAgent> GetSelectAgentAsync(int id)
        {
            return await _uow.Set<SelectAgent>().FirstOrDefaultAsync(c => (c.SelectAgentId == id)).ConfigureAwait(false);
        }
        public async Task<SelectAgent> AddSelectAgentAsync(SelectAgent selectAgent)
        {
            _uow.SelectAgents.Add(selectAgent);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return selectAgent;
        }
        public async Task<SelectAgent> UpdateSelectAgentAsync(SelectAgent selectAgent)
        {
            //    var cmd = $"EXEC SelectAgent_Update @SelectAgentId = {selectAgent.SelectAgentId}," +
            //$" @SelectAgentTitle = N'{selectAgent.SelectAgentTitle}'";
            //    await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(selectAgent).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return selectAgent;
        }
        public async Task DeleteSelectAgentAsync(int selectAgentId)
        {
            var selectAgent = _uow.Set<SelectAgent>().FirstOrDefault(c => c.SelectAgentId == selectAgentId);
            if (selectAgent != null)
            {
                _uow.Set<SelectAgent>().Remove(selectAgent);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }
        #endregion
    }
}
