using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
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
    /// سرویس حساب معین
    /// </summary>
    public class SLsService : ISLsService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public SLsService(SainaDbContext uow, IAppContextService appContextService)
        {
            _uow = uow;
            _appContextService = appContextService;
            // _tLs = _uow.Set<TL>();
        }
        #endregion
        #region Fields
        SainaDbContext _uow;
        readonly IAppContextService _appContextService;
        // private IDbSet<TL> _tLs;
        #endregion
        #region Properties
        public bool ContextHasChanges => _uow.ContextHasChanges;
        #endregion
        #region Methode
        public List<SL> GetSL()
        {
            return _uow.SLs.Include(y => y.TL).ToList();
        }
        public async Task<List<SL>> GetSLsAsync()
        {

            return await _uow.SLs.AsNoTracking().ToListAsync().ConfigureAwait(false);

        }
        public async Task<List<SL>> GetSLsActiveAsync()
        {
            var result = await _uow.SLs.AsNoTracking().Where(x => x.Status == true).ToListAsync().ConfigureAwait(false);
            //foreach (var x in result)
            //{
            //    //var t1 = GetDLsAsync(x.SLId, "DLType1");
            //    //var t2 = GetDLsAsync(x.SLId, "DLType2");
            //    //await Task.WhenAll(t1,t2).ConfigureAwait(false);
            //    //x.Dls1 = t1.Result;
            //    //x.Dls2 = t2.Result;
            //    //IEnumerable<SL> result1;
            //    //IEnumerable<SL> result2;
            //    var result1Task = GetResult(x.SLId, "DLType1");
            //    var result2Task = GetResult(x.SLId, "DLType2");
            //    await Task.WhenAll(result1Task, result2Task).ConfigureAwait(false);
            //    x.Dls1 = result1Task.Result;
            //    x.Dls2 = result2Task.Result;
            //}
            return result;
        }
        public async Task<IEnumerable<DL>> GetResult(int slId, string dlType)
        {
            using (var context = new SainaDbContext())
            {
                var q = $@"SELECT dls.* FROM Info.SLs sls 
                    JOIN Info.DLs dls ON(sls.{dlType} & dls.DLType) > 0 WHERE sls.SLId ={slId}";
                return await context.DLs.SqlQuery(q).ToListAsync().ConfigureAwait(false);
            }
        }
        public async Task<List<DL>> GetDLsAsync(int slId, string dlType)
        {
            var q = $@"SELECT dls.* FROM Info.SLs sls 
                    JOIN Info.DLs dls ON(sls.{dlType} & dls.DLType) > 0 WHERE sls.SLId ={slId}";
            return await _uow.DLs.SqlQuery(q).ToListAsync().ConfigureAwait(false);

        }
        public async Task<SL> GetSLAsync(int id)
        {
            return await _uow.SLs.AsNoTracking().FirstOrDefaultAsync(c => (c.SLId == id)).ConfigureAwait(false);

        }
        public async Task<SL> AddSLAsync(SL sL)
        {
            _uow.SLs.Add(sL);
            await _uow.SaveChangesAsync().ConfigureAwait(false);

            return sL;
        }
        public async Task<SL> UpdateSLAsync(SL sL)
        {
            // var cmd = $"EXEC SL_Update @SLId = {sL.SLId}," +
            //$" @TLId ={(object)sL.TLId ?? "null"}," +
            //$" @SLCode =  {sL.SLCode}," +
            //$" @Title = N'{sL.Title}'," +
            //$" @Title2 = N'{sL.Title2}'," +
            //$" @AccountsNatureEnum = {(int)sL.AccountsNatureEnum}," +
            //$" @Property = {(int)sL.Property}," +
            //$" @DLType1 = {(int)sL.DLType1}," +
            //$" @DLType2 = {(int)sL.DLType2}," +
            //$" @Status = {Convert.ToInt16(sL.Status)}";

            // await _uow.Database.ExecuteSqlCommandAsync(cmd);
            // return sL;
            //if (!_uow.Set<SL>().Local.Any(c => c.SLId == sL.SLId))
            //{
            //.Set<SL>().Attach(sL);
            //}
            _uow.Entry(sL).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);

            return sL;
        }
        public async Task DeleteSLAsync(int sLId)
        {
            var sL = _uow.Set<SL>().FirstOrDefault(c => c.SLId == sLId);
            if (sL != null)
            {
                _uow.Set<SL>().Remove(sL);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }


        public bool HasTitle(string title, int id)
        {
         
            var e = ( _uow.SLs.Where(y => y.SLId != id).ToList());
            var r = e.Any(x => x.Title == title);
            return r;
        }

        public bool HasDuplicate(long code, int id)
        {
            return  _uow.SLs.Where(y => y.SLId != id).Any(x => x.SLCode == code);

        }
        public async Task<bool> HasduplicateTree(long code, int id)
        {
            return await _uow.SLs.Where(y => y.SLId != id).AnyAsync(x => x.SLCode == code);

        }
        public async Task<bool> HasTitleTree(string title, int id)
        {
            var e = (await _uow.SLs.Where(y => y.SLId != id).ToListAsync());
            var r = e.Any(x => x.Title == title);
            return r;

        }
        public long GetLastSLCode(int tlId)
        {
            if (!_uow.SLs.Any(x => x.TLId == tlId))
                return tlId;
            return _uow.SLs.Where(x => x.TLId == tlId).Max(x => x.SLCode);
        }

        public async Task<List<Currency>> GetCurrenciesAsync()
        {
            //var q = $@"SELECT c.*  FROM Info.ExchangeRates e
            //        INNER JOIN Info.Currencies c ON c.CurrencyId = e.CurrencyId
            //        INNER JOIN Info.SLs sls  ON(sls.Property & {(int)PropertyEnum.Exchange}) > 0
            //        WHERE sls.SLId = {slId}";
            //return await _uow.Currencies.SqlQuery(q).ToListAsync().ConfigureAwait(false);
            return await _uow.Currencies.ToListAsync();
        }

        public async Task<List<ExchangeRate>> GetExchangeRatesAsync(int slId, string exchangeRate)
        {
            var q = $@"SELECT e.*,c.CurrencyTitle  FROM Info.ExchangeRates e
                    INNER JOIN Info.Currencies c ON c.CurrencyId = e.CurrencyId
                    INNER JOIN Info.SLs sls  ON(sls.Property & {(int)PropertyEnum.Exchange}) > 0
                    WHERE sls.SLId = {slId}";
            return await _uow.ExchangeRates.SqlQuery(q).ToListAsync().ConfigureAwait(false);
        }

        public async Task<List<SLStandardDescription>> GetSLStandardDescriptionsAsync(int slId)
        {
            return (await _uow.SLs.FirstAsync(x => x.SLId == slId)).SLStandardDescriptions.ToList();

            //var q = $@"SELECT sLStandardDescriptions.* FROM Info.SLs sls 
            //        JOIN Info.SLStandardDescriptions sLStandardDescriptions ON(sls.{sLStandardDescription}) > 0 WHERE sls.SLId ={slId}";
            //return _uow.SLStandardDescriptions.SqlQuery(q).ToListAsync();
        }



        //public async Task<bool> HasTL(int tLId)
        //{
        //    return await _uow.TLs.AnyAsync(x => x.TLId != tLId);
        //}
        #endregion
    }
}
