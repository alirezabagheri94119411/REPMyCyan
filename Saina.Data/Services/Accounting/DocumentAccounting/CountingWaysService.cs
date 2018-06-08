using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Data.Services.Accounting.DocumentAccounting
{
   public class CountingWaysService: ICountingWaysService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public CountingWaysService(SainaDbContext uow, IAppContextService appContextService)
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
        public async Task<List<CountingWay>> GetCountingWaysAsync()
        {
            return await _uow.Set<CountingWay>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async Task<CountingWay> GetCountingWayAsync(int id)
        {
            return await _uow.Set<CountingWay>().FirstOrDefaultAsync(c => (c.CountingWayId == id)).ConfigureAwait(false);
        }
        public async Task<CountingWay> AddCountingWayAsync(CountingWay countingWay)
        {
            _uow.CountingWays.Add(countingWay);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return countingWay;
        }
        public async Task<CountingWay> UpdateCountingWayAsync(CountingWay countingWay)
        {
            //if (!_uow.Set<CountingWay>().Local.Any(c => c.CountingWayId == countingWay.CountingWayId))
            ////{
            //_uow.Set<CountingWay>().Attach(countingWay);
            ////}
            ////_uow.Entry(countingWay).State = EntityState.Modified;
            //await _uow.SaveChangesAsync();
            //var cmd = $"EXEC CountingWay_Update @CountingWayId = {countingWay.CountingWayId}," +
            //   $" @CountingWayTitle = N'{countingWay.CountingWayTitle}'";

            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(countingWay).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return countingWay;
        }
        public async Task DeleteCountingWayAsync(int countingWayId)
        {
            var countingWay = _uow.Set<CountingWay>().FirstOrDefault(c => c.CountingWayId == countingWayId);
            if (countingWay != null)
            {
                _uow.Set<CountingWay>().Remove(countingWay);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }
        #endregion
    }
}
