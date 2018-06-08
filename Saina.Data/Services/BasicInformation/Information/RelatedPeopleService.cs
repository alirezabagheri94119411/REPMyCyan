using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation.Information;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Data.Services.BasicInformation.Information
{
   public class RelatedPeopleService: IRelatedPeopleService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public RelatedPeopleService(SainaDbContext uow, IAppContextService appContextService)
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
        public async  Task<List<RelatedPerson>> GetRelatedPersonsAsync(int relatedPersonId)
        {
            return await  _uow.Set<RelatedPerson>().Where(x => x.RelatedPersonId == relatedPersonId).AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async  Task<RelatedPerson> GetRelatedPersonAsync(int id)
        {
            return await   _uow.Set<RelatedPerson>().FirstOrDefaultAsync(c => (c.RelatedPersonId == id)).ConfigureAwait(false);
        }
        public async Task<RelatedPerson> AddRelatedPersonAsync(RelatedPerson relatedPerson)
        {
            _uow.RelatedPeople.Add(relatedPerson);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return relatedPerson;
        }
        public async Task<RelatedPerson> UpdateRelatedPersonAsync(RelatedPerson relatedPerson)
        {

            //var cmd = $"EXEC RelatedPerson_Update @RelatedPersonId = {relatedPerson.RelatedPersonId}," +
            //$" @Name = N'{relatedPerson.Name}',"+
            //$" @Family = N'{relatedPerson.Family}',"+
            //$" @Phone = N'{relatedPerson.Phone}'";

            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(relatedPerson).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return relatedPerson;
        }
        public async Task DeleteRelatedPersonAsync(int relatedPersonId)
        {
            var relatedPerson = _uow.Set<RelatedPerson>().FirstOrDefault(c => c.RelatedPersonId == relatedPersonId);
            if (relatedPerson != null)
            {
                _uow.Set<RelatedPerson>().Remove(relatedPerson);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }


        #endregion
    }
}
