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
    /// <summary>
    /// سرویس نوع حساب تفصیل
    /// </summary>
    public class DLTypesService : IDLTypesService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public DLTypesService(SainaDbContext uow, IAppContextService appContextService)
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
        public async  Task<List<DLType>> GetDLTypesAsync()
        {
            return await  _uow.Set<DLType>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async  Task<DLType> GetDLTypeAsync(int id)
        {
            return await  _uow.Set<DLType>().FirstOrDefaultAsync(c => (c.DLTypeId == id)).ConfigureAwait(false);
        }
        public async Task<DLType> AddDLTypeAsync(DLType dLType)
        {

            _uow.DLTypes.Add(dLType);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return dLType;
        }

        public async Task<DLType> UpdateDLTypeAsync(DLType dLType)
        {

          //  _uow.DLs.Attach();
           // await _uow.SaveChangesAsync().ConfigureAwait(false);
            _uow.Entry(dLType).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return dLType;

        }
        public async Task DeleteDLTypeAsync(int dLTypeId)
        {
            var dLType = _uow.Set<DLType>().FirstOrDefault(c => c.DLTypeId == dLTypeId);
            if (dLType != null)
            {
                _uow.Set<DLType>().Remove(dLType);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<DLType> GetDLTypeIdAsync(int id)
        {
            return await _uow.Set<DLType>().FirstOrDefaultAsync(x => x.DLTypeId == id).ConfigureAwait(false);
        }
        #endregion
    }
}
