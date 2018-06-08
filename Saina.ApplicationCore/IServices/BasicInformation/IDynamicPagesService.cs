using Saina.ApplicationCore.Entities;
using Saina.ApplicationCore.Entities.BasicInformation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.BasicInformation
{
    /// <summary>
    /// سرویس اطلاعات کاربران
    /// </summary>
    public interface IDynamicPagesService
    {
        Task<List<DynamicPage>> GetDynamicPagesAsync();
        Task<IEnumerable<DynamicPage>> GetDynamicPagesAsync(bool hasAllNodes);
    }
}
