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
   public class BankTypesService: IBankTypesService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public BankTypesService(SainaDbContext uow, IAppContextService appContextService)
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
        public async Task<List<BankType>> GetBankTypesAsync()
        {
            return await _uow.Set<BankType>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
        public async Task<BankType> GetBankTypeAsync(int id)
        {
            return await _uow.Set<BankType>().FirstOrDefaultAsync(c => (c.BankTypeId == id)).ConfigureAwait(false);
        }
        public async Task<BankType> AddBankTypeAsync(BankType bankType)
        {
            _uow.BankTypes.Add(bankType);
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return bankType;
        }
        public async Task<BankType> UpdateBankTypeAsync(BankType bankType)
        {

            //var cmd = $"EXEC BankType_Update @BankTypeId = {bankType.BankTypeId}," +
            //$" @BankTypeTitle=N'{bankType.BankTypeTitle}'";

            //await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(bankType).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return bankType;
        }
        public async Task DeleteBankTypeAsync(int bankTypeId)
        {
            var bankType = _uow.Set<BankType>().FirstOrDefault(c => c.BankTypeId == bankTypeId);
            if (bankType != null)
            {
                _uow.Set<BankType>().Remove(bankType);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }
        #endregion
    }
}
