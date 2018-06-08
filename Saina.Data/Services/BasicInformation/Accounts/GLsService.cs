using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System.Data.Entity;
using Saina.ApplicationCore.IServices.BasicInformation;
using System.Data.SqlClient;

namespace Saina.Data.Services.BasicInformation.Accounts
{
    /// <summary>
    /// سرویس حساب گروه
    /// </summary>
   public class GLsService: IGLsService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public GLsService(SainaDbContext uow, IAppContextService appContextService)
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
        public async  Task<List<GL>> GetGLsActiveAsync()
        {
            return await  _uow.GLs.AsNoTracking().Where(x => x.Status == true).ToListAsync().ConfigureAwait(false);

        }
        public async  Task<List<GL>> GetGLsAsync()
        {
            return await  _uow.Set<GL>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async  Task<GL> GetGLAsync(int id)
        {
            return await _uow.Set<GL>().FirstOrDefaultAsync(c => (c.GLId == id)).ConfigureAwait(false);
        }
        public async Task<GL> AddGLAsync(GL gL)
        {
            _uow.GLs.Add(gL);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return gL;
        }
        public async Task<GL> UpdateGLAsync(GL gL)
        {

            //var cmd = $"EXEC GL_Update @GLId = {gL.GLId}," +
            //$" @AccountGLEnum={(int)gL.AccountGLEnum}," +
            //$" @GLCode = {gL.GLCode}," +
            //$" @Title = N'{gL.Title}'," +
            //$" @Title2 = N'{gL.Title2}'," +
            //$" @Status = {Convert.ToInt16(gL.Status)} ";

            //;

            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(gL).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return gL;
        }
        public async Task DeleteGLAsync(int gLId)
        {
            var gL = _uow.Set<GL>().FirstOrDefault(c => c.GLId == gLId);
            if (gL != null)
            {
                _uow.Set<GL>().Remove(gL);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }

        public bool Hasduplicate(long code, int id)
        {
            var mm = _uow.GLs.ToList().Select(x => x.GLCode);
           return  _uow.GLs.Where(y => y.GLId != id).Any(x => x.GLCode == code);

        }
        public async Task<bool> HasduplicateTree(long code,int id)
        {
            return await _uow.GLs.Where(y => y.GLId != id).AnyAsync(x => x.GLCode == code);

        }
        public async Task<bool> HasTitleTree(string title,int id)
        {
            return await _uow.GLs.Where(y => y.GLId != id).AnyAsync(x => x.Title == title).ConfigureAwait(false);

        }
        public  bool HasTitle(string title,int id)
        {
            return  _uow.GLs.Where(y => y.GLId != id).Any(x => x.Title == title);


        }
        public long GetLastGLCode()
        {
            if (_uow.GLs.Any())
                return _uow.GLs.Select(x => x.GLCode).Max();
            return 0;
        }

     
        public async Task<bool> GetTLAsync(int id)
        {
            return await _uow.TLs.AnyAsync(x => x.GLId == id).ConfigureAwait(false);

        }


        #endregion
    }
}
