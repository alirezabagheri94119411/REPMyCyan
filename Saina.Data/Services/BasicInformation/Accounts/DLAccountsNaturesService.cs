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
  public  class DLAccountsNaturesService: IDLAccountsNaturesService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public DLAccountsNaturesService(SainaDbContext uow, IAppContextService appContextService)
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
        public async Task<List<DLAccountsNature>> GetDLAccountsNaturesAsync()
        {
            return await  _uow.Set<DLAccountsNature>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async  Task<DLAccountsNature> GetDLAccountsNatureAsync(int id)
        {
            return await  _uow.Set<DLAccountsNature>().FirstOrDefaultAsync(c => (c.DLAccountsNatureId == id)).ConfigureAwait(false);
        }
        public async Task<DLAccountsNature> AddDLAccountsNatureAsync(DLAccountsNature dLAccountsNature)
        {
            _uow.DLAccountsNatures.Add(dLAccountsNature);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return dLAccountsNature;
        }
        public async Task<DLAccountsNature> UpdateDLAccountsNatureAsync(DLAccountsNature dLAccountsNature)
        {

            //var cmd = $"EXEC DLAccountsNature_Update @DLAccountsNatureId = {dLAccountsNature.DLAccountsNatureId}," +
            //  $" @DLAccountsNatureTitle =N' {dLAccountsNature.DLAccountsNatureTitle}'";

            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(dLAccountsNature).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return dLAccountsNature;
        }
        public async Task DeleteDLAccountsNatureAsync(int dLAccountsNatureId)
        {
            var dLAccountsNature = _uow.Set<DLAccountsNature>().FirstOrDefault(c => c.DLAccountsNatureId == dLAccountsNatureId);
            if (dLAccountsNature != null)
            {
                _uow.Set<DLAccountsNature>().Remove(dLAccountsNature);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }


        #endregion
    }
}
