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
{ /// <summary>
        /// سرویس بانک
        /// </summary>
   public class BanksService: IBanksService
    {
       
      
            #region Constructors
            /// <summary>
            /// سازنده کلاس
            /// </summary>
            /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
            public BanksService(SainaDbContext uow, IAppContextService appContextService)
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
            public async Task<List<Bank>> GetBanksAsync()
            {
                return await _uow.Set<Bank>().AsNoTracking().ToListAsync().ConfigureAwait(false);
            }
            public async Task<Bank> GetBankAsync(int id)
            {
                return await  _uow.Set<Bank>().FirstOrDefaultAsync(c => (c.BankId == id)).ConfigureAwait(false);
            }
            public async Task<Bank> AddBankAsync(Bank bank)
            {
                _uow.Banks.Add(bank);
                await _uow.SaveChangesAsync().ConfigureAwait(false);
                return bank;
            }
            public async Task<Bank> UpdateBankAsync(Bank bank)
            {

            //var cmd = $"EXEC Bank_Update @BankId = {bank.BankId}," +
            //    $" @BankName = N'{bank.BankName}'," +
            //    $" @BankName2 = N'{bank.BankName2}'," +
            //    $" @BankTypeId = N'{bank.BankTypeId}'";
            //await _uow.Database.ExecuteSqlCommandAsync(cmd);
            _uow.Entry(bank).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return bank;
            }
            public async Task DeleteBankAsync(int bankId)
            {
                var bank = _uow.Set<Bank>().FirstOrDefault(c => c.BankId == bankId);
                if (bank != null)
                {
                    _uow.Set<Bank>().Remove(bank);
                }
                await _uow.SaveChangesAsync().ConfigureAwait(false);
            }

        public async Task<bool> HasBankName1(string bankName1)
        {
            return await _uow.Banks.AnyAsync(x => x.BankName == bankName1).ConfigureAwait(false);

        }

        public async Task<bool> GetBankAccountAsync(int id)
        {
            return await _uow.BankAccounts.AnyAsync(x => x.BankId == id).ConfigureAwait(false);

        }


        #endregion

    }
}
