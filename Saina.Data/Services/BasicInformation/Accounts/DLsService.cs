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
    ///سرویس حساب تفصیل
    /// </summary>
    public class DLsService : IDLsService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public DLsService(SainaDbContext uow, IAppContextService appContextService)
        {
            _uow = uow;
            _appContextService = appContextService;
            _sLs = _uow.Set<SL>();
        }
        #endregion
        #region Fields
        SainaDbContext _uow;
        readonly IAppContextService _appContextService;
        private IDbSet<SL> _sLs;
        #endregion
        #region Properties
        public bool ContextHasChanges => _uow.ContextHasChanges;
        #endregion
        #region Methode
        public async  Task<List<DL>> GetDLsActiveAsync()
        {
            return await  _uow.DLs.AsNoTracking().Where(x => x.Status == true).ToListAsync().ConfigureAwait(false);

        }
        public async  Task<List<DL>> GetDLsAsync()
        {
            return await  _uow.Set<DL>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async  Task<DL> GetDLAsync(int id)
        {
            return await  _uow.Set<DL>().FirstOrDefaultAsync(c => (c.DLId == id)).ConfigureAwait(false);
        }
        public async Task<DL> AddDLAsync(DL dL)
        {
            _uow.DLs.Add(dL);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return dL;
        }
        public async Task<DL> UpdateDLAsync(DL dL)
        {


            _uow.Entry(dL).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);

            //var cmd = $"UPDATE [dbo].[DLs] SET[DLType] = { (int)dL.DLType}" +
            //    $",[DLCode] = {dL.DLCode}," +
            //    $"[Title] = N'{dL.Title}'," +
            //    $"[Title2] =N'{dL.Title2}'," +
            //    $"[Status] = {Convert.ToInt16(dL.Status)}," +
            //    $"[DLAccountsNature] = N'{(int)dL.DLAccountsNature}'," +
            //    $"WHERE([DLId] ={ dL.DLId}";
            //var cmd = $"EXEC DL_Update " +
            //    $"@DLId = {dL.DLId}," +
            //  $" @DLCode = {dL.DLCode}," +
            //  $" @DLType =N' {(int)dL.DLType}'," +
            //  $" @Title = N'{dL.Title}'," +
            //  $" @Title2 = N'{dL.Title2}'," +
            //  $" @DLAccountsNature = N'{(int)dL.DLAccountsNature}'," +
            //  $" @Status = {Convert.ToInt16(dL.Status)}";
            //$" @Customer = {Convert.ToInt16(dL.Customer)},"+
            //$" @Personnel = {Convert.ToInt16(dL.Personnel)},"+
            //$" @Buyer = {Convert.ToInt16(dL.Buyer)}";
            //  await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            return dL;
        }
        public async Task DeleteDLAsync(int dLId)
        {
            var dL = _uow.Set<DL>().FirstOrDefault(c => c.DLId == dLId);
            if (dL != null)
            {
                _uow.Set<DL>().Remove(dL);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }
        public  bool HasTitle(string title,int id)
        {
            return _uow.DLs.Where(y => y.DLId != id).Any(x => x.Title == title);

            //return await _uow.DLs.AnyAsync(x => x.Title == title);
        }
       

        public  bool Hasduplicate(int code, int id)
        {
            return  _uow.DLs.Where(y => y.DLId != id).Any(x => x.DLCode == code);

            //return await _uow.DLs.AnyAsync(x => x.DLCode == code).ConfigureAwait(false);
        }
        public int GetLastDLCode()
        {
            if (_uow.DLs.Any())
                return _uow.DLs.Select(x => x.DLCode).Max();
            return 0;
        }
        #endregion
    }
}
