using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation.Information;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Data.Services.BasicInformation.Information
{
    /// <summary>
    /// سرویس اطلاعات شرکت
    /// </summary>
   public class CompaniesService: ICompaniesService
    {

        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public CompaniesService(SainaDbContext uow, IAppContextService appContextService)
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
       
        public async Task<List<Company>> GetCompaniesAsync()
        {
            return await _uow.Set<Company>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async  Task<Company> GetCompanyAsync(int id)
        {
            return await _uow.Set<Company>().FirstOrDefaultAsync(c => (c.CompanyId == id)).ConfigureAwait(false);
        }
        public async Task<Company> AddCompanyAsync(Company company)
        {
            _uow.Companies.Add(company);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return company;
        }
        public async Task<Company> UpdateCompanyAsync(Company company)
        {

            //CultureInfo us = new CultureInfo("en-us");
            //var cmd = $"EXEC Company_Update @CompanyId = {company.CompanyId}," +
            //    $" @DateRegistration = '{company.DateRegistration.ToString(us)}'," +
            //    $" @RegistrationNumber =N'{company.RegistrationNumber}'," +
            //    $" @EconomicalNumber = N'{company.EconomicalNumber}'," +
            //    $" @City = N'{company.City}'," +
            //    $" @Province = N'{company.Province}'," +
            //    $" @Town = N'{company.Town}'," +
            //    $" @PostalCode = {company.PostalCode}," +

            //    $" @PhoneManager = N'{company.PhoneManager}'," +
            //    $" @ManagerName = N'{company.ManagerName}'," +
            //    $" @Phone1 = N'{company.Phone1}'," +
            //    $" @Phone2 = N'{company.Phone2}'," +
            //    $" @Phone3 = N'{company.Phone3}'," +

            //    $" @Address = N'{company.Address}'," +
            //    $" @WebSite = N'{company.WebSite}'," +
            //    $" @LatinName = N'{company.LatinName}'," +
            //    $" @FirstAccountBalance = {company.FirstAccountBalance}," +
            //    $" @Fax = {company.Fax}," +
            //    $" @Logo = N'{company.Logo}'";

            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(company).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return company;
        }
        public async Task DeleteCompanyAsync(int companyId)
        {
            var company = _uow.Set<Company>().FirstOrDefault(c => c.CompanyId == companyId);
            if (company != null)
            {
                _uow.Set<Company>().Remove(company);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }
        #endregion
    }
}
