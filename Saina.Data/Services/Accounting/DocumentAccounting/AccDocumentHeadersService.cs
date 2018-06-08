using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
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
    public class AccDocumentHeadersService : IAccDocumentHeadersService
    {
        #region Constructors
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="uow">وهله‌ای از الگوی واحد کار یا زمینه ایی اف</param>
        public AccDocumentHeadersService(SainaDbContext uow, IAppContextService appContextService)
        {
            _uow = uow;
            _appContextService = appContextService;
            // var header = new AccDocumentHeader {AccDocumentHeaderId=36, HeaderDescription = "My Header Description!!" };
            //var h= _uow.AccDocumentHeaders.Find(36);
            // _uow.Entry(h).CurrentValues.SetValues(header);
            // _uow.SaveChanges();
            //var h=uow.AccDocumentHeaders.Find(36);
            //uow.Entry(h).Collection(x => x.AccDocumentItems).Load();
            //h.AccDocumentItems.Add( new AccDocumentItem {Description1="MyTest" });
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
        public async Task<List<AccDocumentHeader>> GetAccSystemDocumentHeadersAsync(int date, int typeDocumentId)
        {
            return (await _uow.AccDocumentHeaders.Where(x => x.TypeDocumentId == typeDocumentId && _uow.ShamsiToMiladi(x.DocumentDate, "Saal") ==date.ToString()).AsNoTracking().ToListAsync().ConfigureAwait(false));

        }
        public async Task<List<AccDocumentHeader>> GetOpenCloseHeadersAsync(int date, int typeDocumentId, int typeDocumentId2)
        {
            return (await _uow.AccDocumentHeaders.Where(x => (x.TypeDocumentId == typeDocumentId || x.TypeDocumentId == typeDocumentId2) && _uow.ShamsiToMiladi(x.DocumentDate, "Saal") == date.ToString()).AsNoTracking().ToListAsync().ConfigureAwait(false));

        }
        //public async Task<List<AccDocumentHeader>> GetAccDocumentHeadersAsync(int date)
        //{
        //    PersianCalendar persianCalendar = new PersianCalendar();
        //    return (await _uow.AccDocumentHeaders.AsNoTracking().ToListAsync().ConfigureAwait(false)).Where(x => persianCalendar.GetYear(x.DocumentDate) == date).ToList();



        //}
        public async Task<List<AccDocumentHeaderDTO>> GetAccDocumentHeadersAsync(int date)
        {
            // return (await _uow.AccDocumentHeaders.Include(x => x.TypeDocument).AsNoTracking().ToListAsync().ConfigureAwait(false)).Where(x => persianCalendar.GetYear(x.DocumentDate) == date).ToList();
            return (await _uow.AccDocumentHeaders.Where((x => (x.Status == StatusEnum.End || x.Status == StatusEnum.Permanent) && _uow.ShamsiToMiladi(x.DocumentDate, "Saal") == date.ToString()))
                .Select(x => new AccDocumentHeaderDTO
            {
                AccDocumentHeaderId = x.AccDocumentHeaderId,
                DailyNumber = x.DailyNumber,
                SystemFixNumber = x.SystemFixNumber,
                DocumentNumber = x.DocumentNumber,
                Exporter = x.Exporter,
                Seconder = x.Seconder,
                Status = x.Status,
                SystemName = x.SystemName,
                ManualDocumentNumber = x.ManualDocumentNumber,
                HeaderDescription = x.HeaderDescription,
                DocumentDate = x.DocumentDate,
                AmountDocument = x.AccDocumentItems.Sum(y => y.Debit),
                TypeDocumentTitle = x.TypeDocument.TypeDocumentTitle
            }).AsNoTracking().ToListAsync().ConfigureAwait(false));


        }
        public async Task<AccDocumentHeader> GetAccDocumentHeaderAsync(int id)
        {
            return await _uow.Set<AccDocumentHeader>().FirstOrDefaultAsync(c => (c.AccDocumentHeaderId == id)).ConfigureAwait(false);
        }
        public async Task<AccDocumentHeader> AddAccDocumentHeaderAsync(AccDocumentHeader accDocumentHeader)
        {
            using (var context = new Saina.Data.Context.SainaDbContext())
            {
                context.AccDocumentHeaders.Add(accDocumentHeader);
                await context.SaveChangesAsync().ConfigureAwait(false);

            }
            return accDocumentHeader;
        }
        public AccDocumentHeader AddAccDocumentHeader(AccDocumentHeader accDocumentHeader)
        {
            using (var context = new Saina.Data.Context.SainaDbContext())
            {
                context.AccDocumentHeaders.Add(accDocumentHeader);
                 context.SaveChanges();

            }
            return accDocumentHeader;
        }
        public async Task<AccDocumentHeader> UpdateAccDocumentHeaderAsync(AccDocumentHeader accDocumentHeader)
        {
            //    CultureInfo us = new CultureInfo("en-us");

            //    var cmd = $"EXEC AccDocumentHeader_Update @AccDocumentHeaderId = {accDocumentHeader.AccDocumentHeaderId}," +
            //$" @DocumentNumber = {accDocumentHeader.DocumentNumber}," +
            // $" @DailyNumber = {accDocumentHeader.DailyNumber}," +
            // $" @SystemFixNumber = {accDocumentHeader.SystemFixNumber}," +
            // $" @DocumentDate = '{accDocumentHeader.DocumentDate.ToString(us)}'," +
            // $" @SystemName = N'{accDocumentHeader.SystemName}'," +
            // $" @Exporter = N'{accDocumentHeader.Exporter}'," +
            // $" @Seconder = N'{accDocumentHeader.Seconder}'," +
            // $" @HeaderDescription = N'{accDocumentHeader.HeaderDescription}'," +
            // $" @ManualDocumentNumber = {accDocumentHeader.ManualDocumentNumber}," +
            // $" @BaseDocument = N'{accDocumentHeader.BaseDocument}'," +
            // $" @Seconder = N'{accDocumentHeader.Seconder}'," +
            // $" @TypeDocumentId = {accDocumentHeader.TypeDocumentId}," +
            // $" @DocumentHistory = N'{accDocumentHeader.DocumentHistory}'," +
            // $" @Attachment = N'{accDocumentHeader.Attachment}'," +
            // $" @Status = {Convert.ToInt16(accDocumentHeader.Status)}";
            //    await _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            _uow.Entry(accDocumentHeader).State = EntityState.Modified;
            await _uow.SaveChangesAsync().ConfigureAwait(false);
            return accDocumentHeader;
        }
        public async Task DeleteAccDocumentHeaderAsync(int accDocumentHeaderId)
        {
            var accDocumentitem = $@"DELETE  FROM AccDocumentItems
                                   WHERE AccDocumentHeaderId = {accDocumentHeaderId}";
            var accDocumentHeader = _uow.Set<AccDocumentHeader>().FirstOrDefault(c => c.AccDocumentHeaderId == accDocumentHeaderId);

            if (accDocumentHeader != null)
            {
                _uow.Set<AccDocumentHeader>().Remove(accDocumentHeader);
            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);
        }

        public long GetLastAccDocumentHeaderCode(int date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            if (_uow.AccDocumentHeaders.ToList().Any(x => (persianCalendar.GetYear(x.DocumentDate) == date)))
                //if (_uow.AccDocumentHeaders.Any())
                return _uow.AccDocumentHeaders.Select(x => x.DocumentNumber).Max() + 1;
            return 0;

            //if (_uow.AccDocumentHeaders.ToList().Any(x => (_uow.ShamsiToMiladi(x.DocumentDate, "Saal") ==date.ToString() )))
            ////if (_uow.AccDocumentHeaders.Any())
            //    return _uow.AccDocumentHeaders.Select(x => x.DocumentNumber).Max() + 1;
            //return 0;
        }

        public Task<DocumentNumbering> GetDocumentNumberingAsync()
        {
            return _uow.Set<DocumentNumbering>().AsNoTracking().FirstOrDefaultAsync(x => x.AccountDocument.AccountDocumentId == 1);
        }
        public DocumentNumbering GetDocumentNumbering()
        {
            return _uow.Set<DocumentNumbering>().AsNoTracking().FirstOrDefault(x => x.AccountDocument.AccountDocumentId == 1);
        }
        public async Task<List<TypeDocument>> GetTypeDocumentsAsync()
        {
            var ids = new List<int> { 1, 3, 4, 5, 6 };
            return await _uow.TypeDocuments.Where(x => !ids.Contains(x.TypeDocumentId)).ToListAsync().ConfigureAwait(false);
            // return await _uow.TypeDocuments.Where(x => ids.Contains(x.TypeDocumentId)).ToListAsync().ConfigureAwait(false);
            //return await _uow.Set<TypeDocument>().AsNoTracking().ToListAsync();

        }

        public async Task<long> GetLastDailyNumberCode()
        {
            if (_uow.AccDocumentHeaders.Any(x => x.DocumentDate.Day == DateTime.Now.Day && x.DocumentDate.Month == DateTime.Now.Month && x.DocumentDate.Year == DateTime.Now.Year))
                return await _uow.AccDocumentHeaders.Where(x => x.DocumentDate.Day == DateTime.Now.Day && x.DocumentDate.Month == DateTime.Now.Month && x.DocumentDate.Year == DateTime.Now.Year).Select(x => x.DailyNumber).MaxAsync().ConfigureAwait(false);
            var tt = _uow.AccDocumentHeaders.ToList();
            return await Task.FromResult(1).ConfigureAwait(false);
            //  var maxNum = _uow.Docs.Where(x => x.DocDate == DateTime.Now).Max().Num;
        }
        public long GetLastDailyNumberCode1()
        {
            if (_uow.AccDocumentHeaders.Any(x => x.DocumentDate.Day == DateTime.Now.Day && x.DocumentDate.Month == DateTime.Now.Month && x.DocumentDate.Year == DateTime.Now.Year))
                return  _uow.AccDocumentHeaders.Where(x => x.DocumentDate.Day == DateTime.Now.Day && x.DocumentDate.Month == DateTime.Now.Month && x.DocumentDate.Year == DateTime.Now.Year)
                    .Select(x => x.DailyNumber).Max();
         //   var tt = _uow.AccDocumentHeaders.ToList();
            return 1;
            //  var maxNum = _uow.Docs.Where(x => x.DocDate == DateTime.Now).Max().Num;
        }
        public DateTime? GetFirstAcc(int date)
        {
            var q = $"SELECT Min(DocumentDate) FROM Acc.AccDocumentHeaders  WHERE dbo.ShamsiToMiladi(DocumentDate,'Saal')='{date}'";
            return  _uow.Database.SqlQuery<DateTime?>(q).FirstOrDefault();
        }
        public async Task SaveHeaderAndItemsAsync(AccDocumentHeader accDocumentHeader, IEnumerable<AccDocumentItem> accDocumentItems, IEnumerable<AccDocumentItem> itemsToBeDeleted)
        {
            // TODO: Ensure only Destinations & Lodgings are passed in
            _uow.AccDocumentHeaders.Add(accDocumentHeader);
            if (accDocumentHeader.AccDocumentHeaderId > 0)
            {
                var test = _uow.Entry(accDocumentHeader).State;
                _uow.Entry(accDocumentHeader).State = EntityState.Modified;
            }
            accDocumentItems.ToList().ForEach(accDocumentHeader.AccDocumentItems.Add);
            if (accDocumentHeader.AccDocumentItems?.Any() == true)
            {
                var arr = accDocumentHeader.AccDocumentItems.ToArray();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].AccDocumentItemId > 0)
                    {
                        arr[i].SL = null;
                        arr[i].DL1 = null;
                        arr[i].DL2 = null;
                        _uow.AccDocumentItems.Attach(arr[i]);
                        _uow.Entry(arr[i]).State = EntityState.Modified;
                    }
                    else
                    {
                        arr[i].SL = null;
                        arr[i].DL1 = null;
                        arr[i].DL2 = null;
                        _uow.AccDocumentItems.Attach(arr[i]);
                        _uow.Entry(arr[i]).State = EntityState.Added;
                    }
                    //_uow.AccDocumentItems.AddOrUpdate(x => x.AccDocumentItemId, arr[i]);
                }
            }
            if (itemsToBeDeleted?.Any() == true)
            {
                foreach (var accDocumentItem in itemsToBeDeleted)
                {
                    _uow.Entry(accDocumentItem).State = EntityState.Deleted;
                }
            }
            await _uow.SaveChangesAsync();
        }




        #endregion
    }
}
