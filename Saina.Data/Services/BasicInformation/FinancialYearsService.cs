using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Data.Services.BasicInformation
{
    /// <summary>
    /// سرویس سال مالی
    /// </summary>
    public class FinancialYearsService : IFinancialYearsService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public FinancialYearsService(SainaDbContext uow, IAppContextService appContextService)
        {
            _uow = uow;
            _appContextService = appContextService;
        
        }
        #endregion
        #region Fields
    
        readonly SainaDbContext _uow;
        private IAppContextService _appContextService;
        #endregion
        #region Properties
        public bool ContextHasChanges => _uow.ContextHasChanges;
        #endregion
        #region Methode
        public async  Task<List<FinancialYear>> GetFinancialYearsActiveAsync()
        {
            return await  _uow.FinancialYears.AsNoTracking().Where(x => x.IsActive == true).ToListAsync().ConfigureAwait(false);

        }
        public async  Task<List<FinancialYear>> GetFinancialYearsAsync()
        {
            // return _uow.Users.AsNoTracking().FirstOrDefaultAsync(c => (c.UserId == id));
            return await _uow.FinancialYears.SqlQuery("select * from Info.FinancialYears").ToListAsync();
          ///  return await _uow.FinancialYears.ToListAsync().ConfigureAwait(false);
        }
        public  List<FinancialYear> GetFinancialYears()
        {
            // return _uow.Users.AsNoTracking().FirstOrDefaultAsync(c => (c.UserId == id));
            return _uow.Database.SqlQuery<FinancialYear>("select * from Info.FinancialYears").ToList();
            ///  return await _uow.FinancialYears.ToListAsync().ConfigureAwait(false);
        }
        public async Task<FinancialYear> GetFinancialYearAsync(int id)
        {
            return await _uow.Set<FinancialYear>().FirstOrDefaultAsync(c => (c.FinancialYearId == id)).ConfigureAwait(false);
        }
        public async Task<FinancialYear> AddFinancialYearAsync(FinancialYear financialYear)
        {
            _uow.FinancialYears.Add(financialYear);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return financialYear;
        }
        public async Task<FinancialYear> UpdateFinancialYearAsync(FinancialYear financialYear)
        {
            CultureInfo us = new CultureInfo("en-us");

            //var cmd = $"Update FinancialYear @FinancialYearId = {financialYear.FinancialYearId}," +
            //    $" @YearName = N'{financialYear.YearName}'," +
            //    $" @Description = N'{financialYear.Description}'," +
            //    $" @StartDate = '{financialYear.StartDate.ToString(us)}'," +
            //    $" @EndDate =  '{financialYear.EndDate.ToString(us)}'," +
            //    $" @IsActive = {Convert.ToInt16(financialYear.IsActive)}";
            var cmd = $"UPDATE Info.FinancialYears SET YearName={financialYear.YearName},Description='{financialYear.Description}',StartDate='{financialYear.StartDate.ToString(us)}',EndDate='{financialYear.EndDate.ToString(us)}'" +
                $",IsActive={Convert.ToInt16(financialYear.IsActive)} WHERE FinancialYearId={financialYear.FinancialYearId}";
            await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            //_uow.Entry(financialYear).State = EntityState.Modified;
            //await _uow.SaveChangesAsync().ConfigureAwait(false);
            return financialYear;

        }
        public async Task DeleteFinancialYearAsync(int financialYearId)
        {
            var financialYear = _uow.Set<FinancialYear>().FirstOrDefault(c => c.FinancialYearId == financialYearId);
            if (financialYear != null)
            {
                _uow.Set<FinancialYear>().Remove(financialYear);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }

      

        public async Task<bool> HasFinancialYear(int financialYear)
        {
            return await _uow.FinancialYears.AnyAsync(x => x.YearName == financialYear).ConfigureAwait(false);

        }

        public async Task<bool> HasduplicateStart(DateTime startDate)
        {
            return await _uow.FinancialYears.AnyAsync(x => x.StartDate == startDate).ConfigureAwait(false);
        }

        public async Task<bool> HasduplicateEnd(DateTime endDate)
        {
            return await _uow.FinancialYears.AnyAsync(x => x.EndDate == endDate).ConfigureAwait(false);
        }

        public int GetLastFinancialYear()
        {
            if (_uow.FinancialYears.Any())
                return _uow.FinancialYears.Select(x => x.YearName).Max();
            return 0;
        }
        #endregion
    }
}
