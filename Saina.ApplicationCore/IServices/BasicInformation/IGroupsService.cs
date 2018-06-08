using Saina.ApplicationCore.Entities;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.Entities.BasicInformation.UserAndGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation
{
    /// <summary>
    /// سرویس اطلاعات گروه کاربران
    /// </summary>
    public interface IGroupsService
    {
        Task<List<Group>> GetGroupsAsync();
        Task<Group> GetGroupAsync(int id);
        Task<Group> AddGroupAsync(Group group);
        Task<Group> UpdateGroupAsync(Group group);
        Task DeleteGroupAsync(int groupId);
         int SaveChanges();

        bool ContextHasChanges { get; }
       
    }
}
