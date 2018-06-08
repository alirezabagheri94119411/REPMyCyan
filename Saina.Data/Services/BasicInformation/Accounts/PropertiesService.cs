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
   public class PropertiesService: IPropertiesService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public PropertiesService(SainaDbContext uow, IAppContextService appContextService)
        {
            _uow = uow;
            _appContextService = appContextService;
            //_accountGroups = _uow.Set<AccountGroup>();
        }
        #endregion
        #region Fields
        SainaDbContext _uow;
        readonly IAppContextService _appContextService;
        // private IDbSet<AccountGroup> _accountGroups;
        #endregion
        #region Properties
        public bool ContextHasChanges => _uow.ContextHasChanges;
        #endregion
        #region Methode
        public async Task<List<Property>> GetPropertiesAsync()
        {
            return await _uow.Set<Property>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async Task<Property> GetPropertyAsync(int id)
        {
            return await  _uow.Set<Property>().FirstOrDefaultAsync(c => (c.PropertyId == id)).ConfigureAwait(false);
        }
        public async Task<Property> AddPropertyAsync(Property property)
        {
            _uow.Properties.Add(property);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return property;
        }
        public async Task<Property> UpdatePropertyAsync(Property property)
        {
            _uow.Entry(property).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            //var cmd = $"EXEC Property_Update @PropertyId = {property.PropertyId}," +
            //$" @PropertyTitle=N'{property.PropertyTitle}'";

            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);

            return property;
        }
        public async Task DeletePropertyAsync(int propertyId)
        {
            var property = _uow.Set<Property>().FirstOrDefault(c => c.PropertyId == propertyId);
            if (property != null)
            {
                _uow.Set<Property>().Remove(property);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }
        #endregion
    }
}
