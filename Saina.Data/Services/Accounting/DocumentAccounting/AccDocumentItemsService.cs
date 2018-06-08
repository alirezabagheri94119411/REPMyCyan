using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.Data.Services.Accounting.DocumentAccounting
{
    /// <summary>
    /// سرویس آیتم سند حسابداری
    /// </summary>
   public class AccDocumentItemsService: IAccDocumentItemsService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public AccDocumentItemsService(SainaDbContext uow, IAppContextService appContextService)
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
        public async Task<List<AccDocumentItem>> GetAccDocumentItemsAsync(int headerId)
        {
            var t = await _uow.Set<AccDocumentItem>().Where(x=>x.AccDocumentHeaderId==headerId).Include(x=>x.SL).Include(x => x.DL1).Include(x => x.DL2).AsNoTracking().ToListAsync().ConfigureAwait(false);
            return t;
        }
        public async Task<List<AccDocumentItem>> GetAccDocumentItemDateAsync(int date)
        {
           var result=(await _uow.AccDocumentItems.Where(x => _uow.ShamsiToMiladi(x.AccDocumentHeader.DocumentDate, "Saal") == date.ToString()).AsNoTracking().ToListAsync().ConfigureAwait(false));
            return result;
        }
        public bool HasAccDocumentItemDate(int date)
        {
            var has = false;
            var result = _uow.AccDocumentItems.Where(x => _uow.ShamsiToMiladi(x.AccDocumentHeader.DocumentDate, "Saal") == date.ToString()).AsNoTracking().ToList();
            if (result.Count() > 0)
            {
                has = true;
            };
            return has;
            // return result;
        }
       

            public async Task<AccDocumentItem> GetAccDocumentItemAsync(int id)
        {
            return await  _uow.Set<AccDocumentItem>().FirstOrDefaultAsync(c => (c.AccDocumentItemId == id)).ConfigureAwait(false);
        }
        //public async Task<AccDocumentItem> AddAccDocumentItemAsync(AccDocumentItem accDocumentItem)
        //{
        //    _uow.AccDocumentItems.Add(accDocumentItem);
        //    var xxx=await _uow.SaveChangesAsync().ConfigureAwait(false);
        //    return accDocumentItem;
        //}
        public  AccDocumentItem AddAccDocumentItemAsync(AccDocumentItem accDocumentItem)
        {
            _uow.AccDocumentItems.Add(accDocumentItem);
            var xxx =  _uow.SaveChangesAsync();
            return accDocumentItem;
        }
        public async Task<AccDocumentItem> UpdateAccDocumentItemAsync(AccDocumentItem accDocumentItem)
        {

            //      var cmd = $"EXEC AccDocumentItem_Update @AccDocumentItemId = {accDocumentItem.AccDocumentItemId},";
            ///*  $" @AccDocumentItemTitle = N'{accDocumentItem.AccDocumentItemTitle}'"*/;
            //      await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(accDocumentItem).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return accDocumentItem;
        }
        public async Task DeleteAccDocumentItemAsync(int accDocumentItemId)
        {
            var accDocumentItem = _uow.Set<AccDocumentItem>().FirstOrDefault(c => c.AccDocumentItemId == accDocumentItemId);
            if (accDocumentItem != null)
            {
                _uow.Set<AccDocumentItem>().Remove(accDocumentItem);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<AccDocumentItem> GetFirstAccDocumentItemAsync( int id)
        {
          return await _uow.AccDocumentItems.FirstAsync(xx => xx.AccDocumentItemId == id).ConfigureAwait(false);
        }

     
        public async Task<double> GetDifferenceAccDocumentItemAsync()
        {
            if (!_uow.AccDocumentItems.Any())
            {
                return 0;
            }
            return await _uow.AccDocumentItems.SumAsync(x => x.Debit-x.Credit).ConfigureAwait(false);

        }

        public async Task<double> GetSumDebitAccDocumentItemAsync()
        {
            if (!_uow.AccDocumentItems.Any())
            {
                return 0;
            }
            return await _uow.AccDocumentItems.SumAsync(x => x.Debit).ConfigureAwait(false);
        }

        public async Task<double> GetSumCreditAccDocumentItemAsync()
        {
            if (!_uow.AccDocumentItems.Any())
            {
                return 0;
            }
            return await _uow.AccDocumentItems.SumAsync(x => x.Credit).ConfigureAwait(false);
        }
        public async Task<int> DeleteItemsAccDocumentItemAsync(IEnumerable<int> ids)
        {
            var cmd = $"delete Acc.AccDocumentItems where AccDocumentItemId in({string.Join(",",ids)})";
           return await _uow.Database.ExecuteSqlCommandAsync(cmd);
        }
        public async Task<AccDocumentItem> AddItemsAccDocumentItemAsync(AccDocumentItem temp)
        {
         return    _uow.AccDocumentItems.Add(temp);
        }

        public async Task<bool> HasCurrency(int id)
        {
            return await _uow.SLs.AnyAsync(x =>x.SLCode==id && x.Property.HasFlag(ApplicationCore.Entities.BasicInformation.Accounts.PropertyEnum.Exchange)).ConfigureAwait(false);

        }

        public async Task<bool> HasTracking(int id)
        {
            return await _uow.SLs.AnyAsync(x => x.SLCode == id && x.Property.HasFlag(ApplicationCore.Entities.BasicInformation.Accounts.PropertyEnum.Consistency)).ConfigureAwait(false);

        }
        public DateTime Firstdate(int date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            var result = ( _uow.AccDocumentHeaders.Select(x => new { x.DocumentDate}).FirstOrDefault(x => (_uow.ShamsiToMiladi(x.DocumentDate, "Saal")) == date.ToString())).DocumentDate;

       //    var result = ( _uow.AccDocumentHeaders.FirstOrDefaultAsync(x => (persianCalendar.GetYear(x.DocumentDate) == date))).DocumentDate;
            return result;
        }
        public long FirstNumber(int date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            var result = (_uow.AccDocumentHeaders.Select(x => new { x.DocumentDate, x.DocumentNumber }).FirstOrDefault(x => (_uow.ShamsiToMiladi(x.DocumentDate, "Saal")) == date.ToString())).DocumentNumber;
           // var result = (_uow.AccDocumentHeaders.Select(x => new { x.DocumentDate, x.DocumentNumber }).FirstOrDefault(x => (persianCalendar.GetYear(x.DocumentDate) == date))).DocumentNumber;

            // var result = ( _uow.AccDocumentHeaders.FirstOrDefaultAsync(x => ( .GetYear(x.DocumentDate) == date)));
            return result;
        }


        //public int GetLastRow()
        //{
        //    if (_uow.AccDocumentItems.Any())
        //        return _uow.AccDocumentItems.Select(x => x.AccRow).Max();
        //    return 1;
        //}
        public void  AddAccDocumentItemsAsync(IEnumerable<AccDocumentItem> accDocumentItem)
        {
            //  _uow.AccDocumentItems.AddRange(accDocumentItem);
            foreach (var item in accDocumentItem)
            {
                //var st = _uow.Entry(item).State;
                //_uow.AccDocumentItems.Attach(item);
                _uow.Set<AccDocumentItem>().AddOrUpdate(x=>x.AccDocumentItemId, item);
            }
           // await _uow.SaveChangesAsync().ConfigureAwait(false);
            _uow.SaveChanges();
        }

       

        #endregion
    }
}
