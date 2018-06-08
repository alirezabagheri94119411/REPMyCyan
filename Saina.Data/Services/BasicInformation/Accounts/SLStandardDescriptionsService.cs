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
  public  class SLStandardDescriptionsService: ISLStandardDescriptionsService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public SLStandardDescriptionsService(SainaDbContext uow, IAppContextService appContextService)
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
        public async  Task<List<SLStandardDescription>> GetSLStandardDescriptionsAsync(int sLId)
        {
            return await  _uow.Set<SLStandardDescription>().Where(x=>x.SLId==sLId).AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async  Task<SLStandardDescription> GetSLStandardDescriptionAsync(int id)
        {
            return await _uow.Set<SLStandardDescription>().FirstOrDefaultAsync(c => (c.SLStandardDescriptionId == id)).ConfigureAwait(false);
        }
        public async Task<SLStandardDescription> AddSLStandardDescriptionAsync(SLStandardDescription sLStandardDescription)
        {
            _uow.SLStandardDescriptions.Add(sLStandardDescription);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return sLStandardDescription;
        }
        public async Task<SLStandardDescription> UpdateSLStandardDescriptionAsync(SLStandardDescription sLStandardDescription)
        {
            _uow.Entry(sLStandardDescription).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            //var cmd = $"EXEC SLStandardDescription_Update @SLStandardDescriptionId = {sLStandardDescription.SLStandardDescriptionId}," +
            //$" @SLStandardDescriptionTitle = N'{sLStandardDescription.SLStandardDescriptionTitle}'";

            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);

            return sLStandardDescription;
        }
        public async Task DeleteSLStandardDescriptionAsync(int sLStandardDescriptionId)
        {
            var sLStandardDescription = _uow.Set<SLStandardDescription>().FirstOrDefault(c => c.SLStandardDescriptionId == sLStandardDescriptionId);
            if (sLStandardDescription != null)
            {
                _uow.Set<SLStandardDescription>().Remove(sLStandardDescription);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }

     
        #endregion
    }
}
