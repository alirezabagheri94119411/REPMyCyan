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
    /// سرویس اطلاعات اشخاص
    /// </summary>
    public class PeopleService : IPeopleService
    {

        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public PeopleService(SainaDbContext uow, IAppContextService appContextService)
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
        
        public async  Task<List<Person>> GetPeopleAsync()
        {
            return await _uow.Set<Person>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async Task<List<Person>> GetPeopleCodeAsync( long? dCode)
        {
            return await _uow.People.Where(x => x.Dcode == dCode).AsNoTracking().ToListAsync().ConfigureAwait(false);
            //return await _uow.Set<Person>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async  Task<Person> GetPersonAsync(int id)
        {
            return await _uow.Set<Person>().FirstOrDefaultAsync(c => (c.PersonId == id)).ConfigureAwait(false);
        }
        public async Task<Person> AddPersonAsync(Person person)
        {
            _uow.People.Add(person);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return person;
        }
        public async Task<Person> UpdatePersonAsync(Person person)
        {
            //CultureInfo us = new CultureInfo("en-us");
            //var cmd = $"EXEC Person_Update @PersonId = {person.PersonId}," +
            // $" @NationalCode = {person.NationalCode}," +
            // $" @CertificateNumber = {person.CertificateNumber}," +
            // $" @BirthDate = '{person.BirthDate.ToString(us)}'," +
            // $" @FatherName = N'{person.FatherName}'," +
            // $" @NumberOfChildren = {person.NumberOfChildren}," +

            // $" @PostalCode = {person.PostalCode}," +
            // $" @BirthPlace = N'{person.BirthPlace}'," +
            // $" @Sexuality = N'{person.Sexuality}'," +
            // $" @CertificatePlace = N'{person.CertificatePlace}'," +
            // $" @CertificateSeries = N'{person.CertificateSeries}'," +
            // $" @WebSite = N'{person.WebSite}'," +
            // $" @Address = N'{person.Address}'," +
            // $" @Address2 = N'{person.Address2}'," +
            //  $" @FirstAccountBalance = {person.FirstAccountBalance}," +
            // $" @Fax = {person.Fax}," +
            // $" @Logo = N'{person.Logo}'";
            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(person).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return person;
        }
        public async Task DeletePersonAsync(int personId)
        {
            var person = _uow.Set<Person>().FirstOrDefault(c => c.PersonId == personId);
            if (person != null)
            {
                _uow.Set<Person>().Remove(person);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }
        #endregion
    }
}
