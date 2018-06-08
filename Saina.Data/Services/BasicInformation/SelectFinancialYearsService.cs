using Saina.ApplicationCore.IServices.BasicInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.Data.Context;
using System.Data.Entity;
using System.Globalization;

namespace Saina.Data.Services.BasicInformation
{
    public class SelectFinancialYearsService : ISelectFinancialYearsService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public SelectFinancialYearsService(SainaDbContext uow, IAppContextService appContextService)
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
        public async Task<FinancialYear> GetSelectedFinancialYearAsync()
        {
            return await _uow.FinancialYears.AsNoTracking().FirstOrDefaultAsync(x => x.Selected == true).ConfigureAwait(false);

        }
        public async Task<FinancialYear> UpdateFinancialYearAsync(FinancialYear financialYear)
        {
            var cmd = $"UPDATE info.FinancialYears SET Selected = 0 UPDATE info.FinancialYears SET Selected = 1 where FinancialYearId={financialYear.FinancialYearId}";
                await _uow.Database.ExecuteSqlCommandAsync(cmd);
            return financialYear;

        }

        public DateTime? GetFirstFinancialYear()
        {
            if (!_uow.FinancialYears.Any())
                return null;
            return  _uow.FinancialYears.Min(x => x.StartDate);
        }

        public  DateTime? GetLastFinancialYear()
        {
            if (!_uow.FinancialYears.Any())
                return null;
            return  _uow.FinancialYears.Max(x => x.EndDate);        }

        #endregion
    }
}
