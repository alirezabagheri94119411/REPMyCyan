using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Data.Services.BasicInformation
{
   /// <summary>
   /// سرویس گروه کاربران
   /// </summary>
    public class GroupsService : IGroupsService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public GroupsService(SainaDbContext uow, IAppContextService appContextService)
        {
            _uow = uow;
            _appContextService = appContextService;
            _groups = _uow.Set<Group>();
        }
        #endregion
        #region Fields
        readonly SainaDbContext _uow;
        private IAppContextService _appContextService;
        readonly IDbSet<Group> _groups;
        #endregion
        #region Properties
        public bool ContextHasChanges => _uow.ContextHasChanges;
        #endregion
        #region Methode
        public int SaveChanges()
        {
            return _uow.SaveChanges();
        }
        public async  Task<List<Group>> GetGroupsAsync()
        {
            return await  _uow.Set<Group>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async  Task<Group> GetGroupAsync(int id)
        {
            return await  _uow.Set<Group>().FirstOrDefaultAsync(c => (c.GroupId == id)).ConfigureAwait(false);
        }
        public async Task<Group> AddGroupAsync(Group group)
        {
            _uow.Groups.Add(group);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return group;
        }
        public async Task<Group> UpdateGroupAsync(Group group)
        {

            //var cmd = $"EXEC Group_Update @GroupId = {group.GroupId}, @YearName = N'{group.Name}'";
            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(group).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return group;

        }
        public async Task DeleteGroupAsync(int groupId)
        {
            var group = _uow.Set<Group>().FirstOrDefault(c => c.GroupId == groupId);
            if (group != null)
            {
                _uow.Set<Group>().Remove(group);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }
     
        #endregion
       





    }
}
