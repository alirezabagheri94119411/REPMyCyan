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
    /// حساب کل
    /// </summary>
    public class TLsService : ITLsService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public TLsService(SainaDbContext uow, IAppContextService appContextService)
        {
            _uow = uow;
            _appContextService = appContextService;
            _gLs = _uow.Set<GL>();
        }
        #endregion
        #region Fields
        SainaDbContext _uow;
        readonly IAppContextService _appContextService;
        private IDbSet<GL> _gLs;
        #endregion
        #region Properties
        public bool ContextHasChanges => _uow.ContextHasChanges;
        #endregion
        #region Methode
        public async Task<List<TL>> GetTLsActiveAsync()
        {
            return await _uow.TLs.AsNoTracking().Where(x => x.Status == true).ToListAsync().ConfigureAwait(false);

        }
        public async  Task<List<TL>> GetTLsAsync()
        {
           // var result = await _uow.SLs.AsNoTracking().Where(x => x.Status == true).ToListAsync().ConfigureAwait(false);
            // return _uow.TLs.AsNoTracking().ToListAsync();
          //  return _uow.TLs.Include(y => y.GL).ToList();
           return  await  _uow.TLs.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async Task<TL> GetTLAsync(int id)
        {
            ///return _uow.TLs.AsNoTracking().FirstOrDefaultAsync(c => (c.TLId == id));

            return await  _uow.Set<TL>().FirstOrDefaultAsync(c => (c.TLId == id)).ConfigureAwait(false);
        }
        public async Task<TL> AddTLAsync(TL tL)
        {
            _uow.TLs.Add(tL);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return tL;
           
        }
        public async Task<TL> UpdateTLAsync(TL tL)
        {
            _uow.Entry(tL).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            //   var cmd = $"EXEC TL_Update @TLId = {tL.TLId}," +
            //$" @GLId = {(object)tL.GLId ?? "null"}," +
            //$" @TLCode = {tL.TLCode}," +
            //$" @Title = N'{tL.Title}'," +
            //$" @Title2 = N'{tL.Title2}'," +
            //$" @Status = {Convert.ToInt16(tL.Status)}";
            //   await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            return tL;
        }
        public async Task DeleteTLAsync(int tLId)
        {
            var tL = _uow.Set<TL>().FirstOrDefault(c => c.TLId == tLId);
            if (tL != null)
            {
                _uow.Set<TL>().Remove(tL);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }

        public bool Hasduplicate(long code, int id)
        {
            return  _uow.TLs.Where(y => y.TLId != id).Any(x => x.TLCode == code);

        }

        public bool HasTitle(string title, int id)
        {
            return  _uow.TLs.Where(y => y.TLId != id).Any(x => x.Title == title);


        }
        public async Task<bool> HasduplicateTree(long code, int id)
        {
            return await _uow.TLs.Where(y => y.TLId != id).AnyAsync(x => x.TLCode == code);

        }
        public async Task<bool> HasTitleTree(string title, int id)
        {
            return await _uow.TLs.Where(y => y.TLId != id).AnyAsync(x => x.Title == title).ConfigureAwait(false);

        }
        public long GetLastTLCode( int glId)
        {
            ///حتما این موضوع رو پیدا کن
            if (!_uow.TLs.Any(x => x.GLId == glId))
                return glId;
           return _uow.TLs.Where(x => x.GLId == glId).Max(x => x.TLCode);
           
        }

        public async Task<bool> GetSLAsync(int id)
        {
            return await _uow.SLs.AnyAsync(x => x.TLId == id).ConfigureAwait(false);
        }

        public List<TL> GetTL()
        {
            return _uow.TLs.Include(y => y.GL).ToList();
        }
        #endregion
    }
}
