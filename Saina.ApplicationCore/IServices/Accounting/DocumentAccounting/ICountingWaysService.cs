using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.ApplicationCore.IServices.Accounting.DocumentAccounting
{
    /// <summary>
    /// اینترفیس شماره گذاری اسناد
    /// </summary>
   public interface ICountingWaysService
    {
        Task<List<CountingWay>> GetCountingWaysAsync();
        Task<CountingWay> GetCountingWayAsync(int id);
        Task<CountingWay> AddCountingWayAsync(CountingWay countingWay);
        Task<CountingWay> UpdateCountingWayAsync(CountingWay countingWay);
        Task DeleteCountingWayAsync(int countingWayId);
        bool ContextHasChanges { get; }
    }
}
