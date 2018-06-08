using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Saina.Data.Services.Accounting.DocumentAccounting
{
    public class TLDocumentsService : ITLDocumentsService
    {
        #region Constructors

        public TLDocumentsService(SainaDbContext uow, IAppContextService appContextService)
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

        public async Task<int> GetLastNumberDocAsync(int date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            var years = (await (_uow.TLDocumentHeaders.AsNoTracking().ToListAsync().ConfigureAwait(false))).Select(x => new { TLDocumentHeaderDate = persianCalendar.GetYear(x.TLDocumentHeaderDate), TLDocumentNumber = x.TLDocumentNumber });
            var docYear = years.Where(x => x.TLDocumentHeaderDate == date);
            if (docYear.Any())
            {
                return (docYear.Max(y => y.TLDocumentNumber));

            }
            return 0;

        }
        public int GetLastNumberDoc(int date)
        {
            //PersianCalendar persianCalendar = new PersianCalendar();
            //var years = ( (_uow.TLDocumentHeaders.AsNoTracking().ToList())).Select(x => new { TLDocumentHeaderDate = persianCalendar.GetYear(x.TLDocumentHeaderDate), TLDocumentNumber = x.TLDocumentNumber });
            //var docYear = years.Where(x => x.TLDocumentHeaderDate == date);
            //if (docYear.Any())
            //{
            //    return (docYear.Max(y => y.TLDocumentNumber));

            //}
            //return 0;
            var q = $"SELECT MAX(TLDocumentNumber) FROM Acc.TLDocumentHeaders  WHERE dbo.ShamsiToMiladi(TLDocumentHeaderDate,'Saal')='{date}'";
            var result = _uow.Database.SqlQuery<int?>(q).FirstOrDefault();
            if (result == null) return 0;
            return result.Value;

        }
        public int LastNumber(int date)
        {
            //PersianCalendar persianCalendar = new PersianCalendar();
            //var years = ( (_uow.TLDocumentHeaders.AsNoTracking().ToList())).Select(x => new { TLDocumentHeaderDate = persianCalendar.GetYear(x.TLDocumentHeaderDate), TLDocumentNumber = x.TLDocumentNumber });
            //var docYear = years.Where(x => x.TLDocumentHeaderDate == date);
            //if (docYear.Any())
            //{
            //    return (docYear.Max(y => y.TLDocumentNumber));

            //}
            //return 0;
            var q = $"SELECT MAX(TLDocumentNumber) FROM Acc.TLDocumentHeaders  WHERE dbo.ShamsiToMiladi(TLDocumentHeaderDate,'Saal')='{date}'";
            var result = _uow.Database.SqlQuery<int?>(q).FirstOrDefault();
            if (result == null) return 0;
            return result.Value;
        }
        public int HasType(int date)
        {
            var result = _uow.TLDocumentHeaders.Where(x => _uow.ShamsiToMiladi(x.TLDocumentHeaderDate, "Saal") == date.ToString()).FirstOrDefault();
            if (result != null)
            {
                var TLDocumentExport = result.TLDocumentExport;
                return Convert.ToInt16(TLDocumentExport);
            }
            else
            {
                return 0;
            }

        }
        public bool HasTLDocumentItem(int date)
        {

            var result = _uow.TLDocumentHeaders.Any(x => (_uow.ShamsiToMiladi(x.TLDocumentHeaderDate, "Saal") == date.ToString()));

            return result;
        }
        //public long LastNumber(int date)
        //{
        //    PersianCalendar persianCalendar = new PersianCalendar();
        //   var result = _uow.TLDocumentHeaders.Where(x => (persianCalendar.GetYear(x.TLDocumentHeaderDate) == date)).Select(z => z.ToNumber).Max();
        //    return result;
        //}
        public DateTime LastDate(int date)
        {
            //  PersianCalendar persianCalendar = new PersianCalendar();
            //var years = ( (_uow.TLDocumentHeaders.AsNoTracking().ToList())).Select(x => new { TLDocumentHeaderDate = persianCalendar.GetYear(x.TLDocumentHeaderDate), TLDocumentNumber = x.TLDocumentNumber });
            //var docYear = years.Where(x => x.TLDocumentHeaderDate == date);
            //if (docYear.Any())
            //{
            //    return (docYear.Max(y => y.TLDocumentNumber));

            //}
            //return 0;
            //PersianCalendar persianCalendar = new PersianCalendar();
            //var result =( _uow.TLDocumentHeaders.Where(x=> _uow.ShamsiToMiladi(x.DocumentDate, "Saal") ==date.ToString()).AsNoTracking().ToList().Where(x => (persianCalendar.GetYear(x.TLDocumentHeaderDate) == date))).Max(z => z.ToDate);
            var q = $"SELECT MAX(TOdate) FROM Acc.TLDocumentHeaders  WHERE dbo.ShamsiToMiladi(TLDocumentHeaderDate,'Saal')='{date}'";
            var result = _uow.Database.SqlQuery<DateTime?>(q).FirstOrDefault();
            if (result == null) return DateTime.Now;
            return result.Value;
        }
        public void DeleteTLDocumentHeaderAsync(int TLDocumentHeaderId)
        {
            var q = $"DELETE  FROM Acc.TLDocumentItems WHERE TLDocumentHeaderId = { TLDocumentHeaderId } " +
                $"delete Acc.TLDocumentHeaders where  TLDocumentHeaderId = { TLDocumentHeaderId }";
            var query = _uow.Database.ExecuteSqlCommand(q);

            //var tLDocumentitem = $@"DELETE  FROM TLDocumentItems
            //                       WHERE TLDocumentHeaderId = {TLDocumentHeaderId}";
            //var TLDocumentHeader = _uow.Set<TLDocumentHeader>().FirstOrDefault(c => c.TLDocumentHeaderId == TLDocumentHeaderId);

            //if (TLDocumentHeader != null)
            //{
            //    _uow.Set<TLDocumentHeader>().Remove(TLDocumentHeader);
            //}
            //await _uow.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<List<TLDocumentHeader>> GetTLDocumentHeadersAsync(int date)
        {
            var res = (await _uow.TLDocumentHeaders.Where(x => _uow.ShamsiToMiladi(x.TLDocumentHeaderDate, "Saal") == date.ToString()).AsNoTracking().ToListAsync().ConfigureAwait(false));
            return res;
        }

        public DateTime? GetLastDateDoc(int date)
        {
            var q = $"SELECT MAX(TLDocumentHeaderDate) FROM Acc.TLDocumentHeaders  WHERE dbo.ShamsiToMiladi(TLDocumentHeaderDate,'Saal')='{date}'";
            var result = _uow.Database.SqlQuery<DateTime?>(q).FirstOrDefault();
            if (result == null) return null;
            return result.Value;
            //PersianCalendar persianCalendar = new PersianCalendar();
            //var docYear = (await (_uow.TLDocumentHeaders.Where(x => _uow.ShamsiToMiladi(x.TLDocumentHeaderDate, "Saal") == date.ToString() ).max(x=>x.TLDocumentHeaderDate).AsNoTracking().ToListAsync().ConfigureAwait(false))).;
            //var docYear = years.Where(x => x.DocumentDateYear == date);
            //if (docYear.Any())
            //{
            //    return (docYear.Max(y => y.TLDocumentHeaderDate));

            //}

            //return null;

        }
        public async Task<DateTime?> GetLastDateDocAsync(int date)
        {
            var q = $"SELECT MAX(TLDocumentHeaderDate) FROM Acc.TLDocumentHeaders  WHERE dbo.ShamsiToMiladi(TLDocumentHeaderDate,'Saal')='{date}'";
            var result = await _uow.Database.SqlQuery<DateTime?>(q).FirstOrDefaultAsync();
            if (result == null) return null;
            return result.Value;
            //PersianCalendar persianCalendar = new PersianCalendar();
            //var docYear = (await (_uow.TLDocumentHeaders.Where(x => _uow.ShamsiToMiladi(x.TLDocumentHeaderDate, "Saal") == date.ToString() ).max(x=>x.TLDocumentHeaderDate).AsNoTracking().ToListAsync().ConfigureAwait(false))).;
            //var docYear = years.Where(x => x.DocumentDateYear == date);
            //if (docYear.Any())
            //{
            //    return (docYear.Max(y => y.TLDocumentHeaderDate));

            //}

            //return null;

        }

        public async Task<List<TLDocumentItem>> GetTLDocumentItemsAsync(int Id)
        {

            return (await _uow.TLDocumentItems.AsNoTracking().ToListAsync().ConfigureAwait(false)).Where(x => x.TLDocumentHeaderId == Id).ToList();

        }
        public async Task<int> GetExportDocumentDate1(int date, long toNumber, DateTime fromDate, DateTime toDate, TLDocumentExportEnum tLDocumentExportEnum, TLDocumentTypeEnum tLDocumentTypeEnum)
        {
            int i = 1;

            //        var dateTLDoc = DateTime.Now;
            //    PersianCalendar persianCalendar = new PersianCalendar();
            //   var dateTL= persianCalendar.GetYear(dateTLDoc);

            ////  var year =  _uow.ShamsiToMiladi(dateTLDoc, "Saal") == date.ToString();
            //    var end = (await _uow.FinancialYears.Select(x => new { x.YearName, x.EndDate }).FirstOrDefaultAsync(x => x.YearName == date)).EndDate;

            //    if (date!= dateTL)
            //    {
            //        dateTLDoc = end;
            //    }

            var tLDocumentNumber = 1;
            if (_uow.TLDocumentHeaders.Any())
            {
                tLDocumentNumber = _uow.TLDocumentHeaders.Select(x => x.TLDocumentNumber).Max() + 1;
            }

            //Enumerable<dynamic> en = new List<dynamic>();
            if (tLDocumentExportEnum == TLDocumentExportEnum.Daily)
            {
                i = 1; ;
                var groupedAccDocumentItems = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                           .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TLId, x.SL.TL.Title }).ToListAsync();
                //.Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);

                foreach (var groups in groupedAccDocumentItems)
                {
                    var f = groups.FirstOrDefault();
                    var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                    foreach (var x in groups)
                    {
                        h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                    }
                    _uow.TLDocumentHeaders.Add(h);

                    tLDocumentNumber++;
                }
            }
            else if (tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
            {
                i = 1;
                var groupedAccDocumentItems = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                                 .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title }).ToListAsync();
                foreach (var groups in groupedAccDocumentItems)
                {
                    var f = groups.FirstOrDefault();
                    var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                    foreach (var x in groups)
                    {
                        h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                    }
                    _uow.TLDocumentHeaders.Add(h);

                    tLDocumentNumber++;
                }
            }


            if (tLDocumentTypeEnum.HasFlag(TLDocumentTypeEnum.Closing))
            {
                var result = await _uow.AccDocumentItems.AnyAsync(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.TypeDocumentId == 4);
                if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Daily)
                {
                    i = 1; ;
                    var groupedAccDocumentItems = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                               .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TLId, x.SL.TL.Title }).ToListAsync();
                    //.Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);
                    foreach (var groups in groupedAccDocumentItems)
                    {
                        var f = groups.FirstOrDefault();
                        var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                        foreach (var x in groups)
                        {
                            h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                        }
                        _uow.TLDocumentHeaders.Add(h);

                        tLDocumentNumber++;
                    }
                }
                else if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
                {
                    i = 1;
                    var groupedAccDocumentItems = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                                     .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title }).ToListAsync();
                    foreach (var groups in groupedAccDocumentItems)
                    {
                        var f = groups.FirstOrDefault();
                        var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                        foreach (var x in groups)
                        {
                            h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                        }
                        _uow.TLDocumentHeaders.Add(h);

                        tLDocumentNumber++;
                    }
                }

            }
            if (tLDocumentTypeEnum.HasFlag(TLDocumentTypeEnum.Opening))
            {
                var result = await _uow.AccDocumentItems.AnyAsync(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.TypeDocumentId == 3);
                if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Daily)
                {
                    i = 1; ;
                    var groupedAccDocumentItems = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                               .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TLId, x.SL.TL.Title }).ToListAsync();
                    //.Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);
                    foreach (var groups in groupedAccDocumentItems)
                    {
                        var f = groups.FirstOrDefault();
                        var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                        foreach (var x in groups)
                        {
                            h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                        }
                        _uow.TLDocumentHeaders.Add(h);

                        tLDocumentNumber++;
                    }
                }
                else if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
                {
                    i = 1;
                    var groupedAccDocumentItems = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                                     .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title }).ToListAsync();
                    foreach (var groups in groupedAccDocumentItems)
                    {
                        var f = groups.FirstOrDefault();
                        var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                        foreach (var x in groups)
                        {
                            h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                        }
                        _uow.TLDocumentHeaders.Add(h);

                        tLDocumentNumber++;
                    }
                }

            }
            if (tLDocumentTypeEnum.HasFlag(TLDocumentTypeEnum.Litter))
            {
                var result = await _uow.AccDocumentItems.AnyAsync(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.TypeDocumentId == 6);
                if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Daily)
                {
                    i = 1; ;
                    var groupedAccDocumentItems = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                               .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TLId, x.SL.TL.Title }).ToListAsync();
                    //.Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);
                    foreach (var groups in groupedAccDocumentItems)
                    {
                        var f = groups.FirstOrDefault();
                        var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                        foreach (var x in groups)
                        {
                            h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                        }
                        _uow.TLDocumentHeaders.Add(h);

                        tLDocumentNumber++;
                    }
                }
                else if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
                {
                    i = 1; ;
                    var groupedAccDocumentItems = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                                .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title }).ToListAsync();
                    foreach (var groups in groupedAccDocumentItems)
                    {
                        var f = groups.FirstOrDefault();
                        var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                        foreach (var x in groups)
                        {
                            h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                        }
                        _uow.TLDocumentHeaders.Add(h);

                        tLDocumentNumber++;
                    }
                }

            }
            if (tLDocumentTypeEnum.HasFlag(TLDocumentTypeEnum.Profit))
            {
                var result = await _uow.AccDocumentItems.AnyAsync(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.TypeDocumentId == 1);
                if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Daily)
                {
                    i = 1; ;
                    var groupedAccDocumentItems = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                               .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TLId, x.SL.TL.Title }).ToListAsync();
                    //.Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);
                    foreach (var groups in groupedAccDocumentItems)
                    {
                        var f = groups.FirstOrDefault();
                        var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                        foreach (var x in groups)
                        {
                            h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                        }
                        _uow.TLDocumentHeaders.Add(h);

                        tLDocumentNumber++;
                    }
                }
                else if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
                {
                    i = 1; ;
                    var groupedAccDocumentItems = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                                  .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title }).ToListAsync();
                    foreach (var groups in groupedAccDocumentItems)
                    {
                        var f = groups.FirstOrDefault();
                        var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                        foreach (var x in groups)
                        {
                            h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                        }
                        _uow.TLDocumentHeaders.Add(h);

                        tLDocumentNumber++;
                    }
                }

            }

            return _uow.SaveChanges();

        }
        public bool GetAccDocumentHeaders(DateTime fromDate, DateTime toDate)
        {
            return (_uow.AccDocumentHeaders.Any((x => x.Status == StatusEnum.Draft && x.DocumentDate >= fromDate && x.DocumentDate <= toDate)));
}
        public int GetExportDocumentDate(int date, long toNumber, DateTime fromDate, DateTime toDate, TLDocumentExportEnum tLDocumentExportEnum, TLDocumentTypeEnum tLDocumentTypeEnum)
        {
            int i = 1;

            //        var dateTLDoc = DateTime.Now;
            //    PersianCalendar persianCalendar = new PersianCalendar();
            //   var dateTL= persianCalendar.GetYear(dateTLDoc);

            ////  var year =  _uow.ShamsiToMiladi(dateTLDoc, "Saal") == date.ToString();
            //    var end = (await _uow.FinancialYears.Select(x => new { x.YearName, x.EndDate }).FirstOrDefaultAsync(x => x.YearName == date)).EndDate;

            //    if (date!= dateTL)
            //    {
            //        dateTLDoc = end;
            //    }

            var tLDocumentNumber = 1;
            if (_uow.TLDocumentHeaders.Any())
            {
                tLDocumentNumber = _uow.TLDocumentHeaders.Select(x => x.TLDocumentNumber).Max() + 1;
            }

            //Enumerable<dynamic> en = new List<dynamic>();
            if (tLDocumentExportEnum == TLDocumentExportEnum.Daily)
            {
                i = 1; ;
                var groupedAccDocumentItems = _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                           .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TLId, x.SL.TL.Title }).ToList();
                //.Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);

                foreach (var groups in groupedAccDocumentItems)
                {
                    var f = groups.FirstOrDefault();
                    var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                    foreach (var x in groups)
                    {
                        h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                    }
                    _uow.TLDocumentHeaders.Add(h);

                    tLDocumentNumber++;
                }
            }
            else if (tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
            {
                i = 1;
                var groupedAccDocumentItems = _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                                 .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title }).ToList();
                foreach (var groups in groupedAccDocumentItems)
                {
                    var f = groups.FirstOrDefault();
                    var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                    foreach (var x in groups)
                    {
                        h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                    }
                    _uow.TLDocumentHeaders.Add(h);

                    tLDocumentNumber++;
                }
            }


            if (tLDocumentTypeEnum.HasFlag(TLDocumentTypeEnum.Closing))
            {
                var result = _uow.AccDocumentItems.Any(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate
               && x.AccDocumentHeader.TypeDocumentId == 4);
                if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Daily)
                {
                    i = 1; ;
                    var groupedAccDocumentItems = _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                               .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TLId, x.SL.TL.Title }).ToList();
                    //.Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);
                    foreach (var groups in groupedAccDocumentItems)
                    {
                        var f = groups.FirstOrDefault();
                        var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                        foreach (var x in groups)
                        {
                            h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                        }
                        _uow.TLDocumentHeaders.Add(h);

                        tLDocumentNumber++;
                    }
                }
                else if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
                {
                    i = 1;
                    var groupedAccDocumentItems = _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                                     .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title }).ToList();
                    foreach (var groups in groupedAccDocumentItems)
                    {
                        var f = groups.FirstOrDefault();
                        var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                        foreach (var x in groups)
                        {
                            h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                        }
                        _uow.TLDocumentHeaders.Add(h);

                        tLDocumentNumber++;
                    }
                }

            }
            if (tLDocumentTypeEnum.HasFlag(TLDocumentTypeEnum.Opening))
            {
                var result = _uow.AccDocumentItems.Any(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.TypeDocumentId == 3);
                if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Daily)
                {
                    i = 1; ;
                    var groupedAccDocumentItems = _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                               .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TLId, x.SL.TL.Title }).ToList();
                    //.Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);
                    foreach (var groups in groupedAccDocumentItems)
                    {
                        var f = groups.FirstOrDefault();
                        var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                        foreach (var x in groups)
                        {
                            h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                        }
                        _uow.TLDocumentHeaders.Add(h);

                        tLDocumentNumber++;
                    }
                }
                else if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
                {
                    i = 1;
                    var groupedAccDocumentItems = _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                                     .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title }).ToList();
                    foreach (var groups in groupedAccDocumentItems)
                    {
                        var f = groups.FirstOrDefault();
                        var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                        foreach (var x in groups)
                        {
                            h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                        }
                        _uow.TLDocumentHeaders.Add(h);

                        tLDocumentNumber++;
                    }
                }

            }
            if (tLDocumentTypeEnum.HasFlag(TLDocumentTypeEnum.Litter))
            {
                var result = _uow.AccDocumentItems.Any(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.TypeDocumentId == 6);
                if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Daily)
                {
                    i = 1; ;
                    var groupedAccDocumentItems = _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                               .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TLId, x.SL.TL.Title }).ToList();
                    //.Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);
                    foreach (var groups in groupedAccDocumentItems)
                    {
                        var f = groups.FirstOrDefault();
                        var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                        foreach (var x in groups)
                        {
                            h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                        }
                        _uow.TLDocumentHeaders.Add(h);

                        tLDocumentNumber++;
                    }
                }
                else if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
                {
                    i = 1; ;
                    var groupedAccDocumentItems = _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                                .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title }).ToList();
                    foreach (var groups in groupedAccDocumentItems)
                    {
                        var f = groups.FirstOrDefault();
                        var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                        foreach (var x in groups)
                        {
                            h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                        }
                        _uow.TLDocumentHeaders.Add(h);

                        tLDocumentNumber++;
                    }
                }

            }
            if (tLDocumentTypeEnum.HasFlag(TLDocumentTypeEnum.Profit))
            {
                var result = _uow.AccDocumentItems.Any(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.TypeDocumentId == 1);
                if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Daily)
                {
                    i = 1; ;
                    var groupedAccDocumentItems = _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                               .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TLId, x.SL.TL.Title }).ToList();
                    //.Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);
                    foreach (var groups in groupedAccDocumentItems)
                    {
                        var f = groups.FirstOrDefault();
                        var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                        foreach (var x in groups)
                        {
                            h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                        }
                        _uow.TLDocumentHeaders.Add(h);

                        tLDocumentNumber++;
                    }
                }
                else if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
                {
                    i = 1; ;
                    var groupedAccDocumentItems = _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                                  .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title }).ToList();
                    foreach (var groups in groupedAccDocumentItems)
                    {
                        var f = groups.FirstOrDefault();
                        var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = f.AccDocumentHeader.DocumentDate, TLDocumentNumber = tLDocumentNumber, TLDocumentType = TLDocumentTypeEnum.None, ToNumber = toNumber, ToDate = toDate };
                        foreach (var x in groups)
                        {
                            h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = f.AccDocumentHeader.DocumentDate, TLId = groups.Key.TLId, TLTitle = groups.Key.Title, Debit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) > 0 ? (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) : 0, Credit = (groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit)) < 0 ? Math.Abs((groups.Sum(g => g.Debit) - groups.Sum(g => g.Credit))) : 0 });
                        }
                        _uow.TLDocumentHeaders.Add(h);

                        tLDocumentNumber++;
                    }
                }

            }

            return _uow.SaveChanges();

        }

        public async Task GetExportDocumentDate1(DateTime fromDate, DateTime toDate, int fromNumber, TLDocumentExportEnum tLDocumentExportEnum, TLDocumentTypeEnum tLDocumentTypeEnum, DateTime TLDocumentDate)
        {
            int i = 1; ;
            IEnumerable<dynamic> en = new List<dynamic>();
            if (tLDocumentExportEnum == TLDocumentExportEnum.Daily)
            {
                i = 1; ;
                en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                          .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TL.TLId, x.SL.TL.Title })
                          .Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);
                var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = TLDocumentDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.None };
                foreach (var x in en)
                {
                    h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                }
                _uow.TLDocumentHeaders.Add(h);
                fromNumber++;
            }
            else if (tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
            {
                en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status == StatusEnum.Permanent)
                                .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title })
                                .Select(x => new { TLDocumentItemDate = x.Key.Month, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);
                var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = TLDocumentDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.None };
                i = 1; ;
                foreach (var x in en)
                {
                    h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                }
                _uow.TLDocumentHeaders.Add(h);
                fromNumber++;
            }


            if (tLDocumentTypeEnum.HasFlag(TLDocumentTypeEnum.Closing))
            {
                var result = await _uow.AccDocumentItems.AnyAsync(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.TypeDocumentId == 4);
                if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Daily)
                {
                    en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                         .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TL.TLId, x.SL.TL.Title })
                         .Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);
                    var h1 = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = toDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.Closing };
                    i = 1; ;
                    foreach (var x in en)
                    {
                        h1.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                    }
                    _uow.TLDocumentHeaders.Add(h1);
                    fromNumber++;
                }
                else if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
                {
                    en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                                .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title })
                                .Select(x => new { TLDocumentItemDate = x.Key.Month, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);
                    var h1 = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = toDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.Closing };
                    i = 1; ;
                    foreach (var x in en)
                    {
                        h1.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                    }
                    _uow.TLDocumentHeaders.Add(h1);
                    fromNumber++;
                }

            }
            if (tLDocumentTypeEnum.HasFlag(TLDocumentTypeEnum.Opening))
            {
                var result = await _uow.AccDocumentItems.AnyAsync(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.TypeDocumentId == 3);
                if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Daily)
                {
                    en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                         .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TL.TLId, x.SL.TL.Title })
                         .Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);
                    var h1 = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = toDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.Opening };
                    i = 1; ;
                    foreach (var x in en)
                    {
                        h1.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                    }
                    _uow.TLDocumentHeaders.Add(h1);
                    fromNumber++;
                }
                else if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
                {
                    en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                                .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title })
                                .Select(x => new { TLDocumentItemDate = x.Key.Month, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);
                    var h1 = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = toDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.Opening };
                    i = 1; ;
                    foreach (var x in en)
                    {
                        h1.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                    }
                    _uow.TLDocumentHeaders.Add(h1);
                    fromNumber++;
                }

            }
            if (tLDocumentTypeEnum.HasFlag(TLDocumentTypeEnum.Litter))
            {
                var result = await _uow.AccDocumentItems.AnyAsync(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.TypeDocumentId == 6);
                if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Daily)
                {
                    en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                         .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TL.TLId, x.SL.TL.Title })
                         .Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);
                    var h1 = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = toDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.Litter };
                    i = 1; ;
                    foreach (var x in en)
                    {
                        h1.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                    }
                    _uow.TLDocumentHeaders.Add(h1);
                    fromNumber++;
                }
                else if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
                {
                    en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                                .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title })
                                .Select(x => new { TLDocumentItemDate = x.Key.Month, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);
                    var h1 = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = toDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.Litter };
                    i = 1; ;
                    foreach (var x in en)
                    {
                        h1.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                    }
                    _uow.TLDocumentHeaders.Add(h1);
                    fromNumber++;
                }

            }
            if (tLDocumentTypeEnum.HasFlag(TLDocumentTypeEnum.Profit))
            {
                var result = await _uow.AccDocumentItems.AnyAsync(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.TypeDocumentId == 1);
                if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Daily)
                {
                    en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                         .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TL.TLId, x.SL.TL.Title })
                         .Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);
                    var h1 = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = toDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.Profit };
                    i = 1; ;
                    foreach (var x in en)
                    {
                        h1.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                    }
                    _uow.TLDocumentHeaders.Add(h1);
                    fromNumber++;
                }
                else if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
                {
                    en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate && x.AccDocumentHeader.Status != StatusEnum.Draft)
                                .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title })
                                .Select(x => new { TLDocumentItemDate = x.Key.Month, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);
                    var h1 = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = toDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.Profit };
                    i = 1; ; foreach (var x in en)
                    {
                        h1.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                    }
                    _uow.TLDocumentHeaders.Add(h1);
                    fromNumber++;
                }

            }

            await _uow.SaveChangesAsync().ConfigureAwait(false);

        }
        public async Task GetExportDocumentNumber(int fromNumber, int toNumber, DateTime fromDate, TLDocumentExportEnum tLDocumentExportEnum, TLDocumentTypeEnum tLDocumentTypeEnum, DateTime TLDocumentDate)
        {
            int i = 1; ;
            dynamic en = new List<TLDocumentItem>();
            if (tLDocumentExportEnum == TLDocumentExportEnum.Daily)
            {
                //برای بقیه هم همین کار رو کنیم
                en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentNumber >= fromNumber && x.AccDocumentHeader.DocumentNumber <= toNumber
                && x.AccDocumentHeader.Status != StatusEnum.Draft && !_uow.TLDocumentHeaders.Any(t => t.TLDocumentNumber >= fromNumber && t.TLDocumentNumber <= toNumber))
                          .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TL.TLId, x.SL.TL.Title })
                          .Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit - y.Credit) > 0 ? Math.Abs(x.Sum(y => y.Debit - y.Credit)) : 0, Credit = x.Sum(y => y.Debit - y.Credit) < 0 ? Math.Abs(x.Sum(y => y.Debit - y.Credit)) : 0 }).ToListAsync().ConfigureAwait(false);
                var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = TLDocumentDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.None };
                i = 1; ;
                foreach (var x in en)
                {
                    h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                }
                _uow.TLDocumentHeaders.Add(h);
                fromNumber++;
            }
            else if (tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
            {
                en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentNumber >= fromNumber && x.AccDocumentHeader.DocumentNumber <= toNumber && x.AccDocumentHeader.Status == StatusEnum.Permanent)
                                .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title })
                                .Select(x => new { TLDocumentItemDate = x.Key.Month, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit - y.Credit) > 0 ? x.Sum(y => y.Debit - y.Credit) : 0, Credit = x.Sum(y => y.Debit - y.Credit) < 0 ? x.Sum(y => y.Debit - y.Credit) : 0 }).ToListAsync().ConfigureAwait(false);
                var h = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = TLDocumentDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.None };
                i = 1; ;
                foreach (var x in en)
                {
                    h.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                }
                _uow.TLDocumentHeaders.Add(h);
                fromNumber++;
            }

            if (tLDocumentTypeEnum.HasFlag(TLDocumentTypeEnum.Closing))
            {
                var result = await _uow.AccDocumentItems.AnyAsync(x => x.AccDocumentHeader.DocumentNumber >= fromNumber && x.AccDocumentHeader.DocumentNumber <= toNumber && x.AccDocumentHeader.TypeDocumentId == 4);
                if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Daily)
                {
                    en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentNumber >= fromNumber && x.AccDocumentHeader.DocumentNumber <= toNumber && x.AccDocumentHeader.Status != StatusEnum.Draft)
                         .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TL.TLId, x.SL.TL.Title })
                         .Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit - y.Credit) > 0 ? x.Sum(y => y.Debit - y.Credit) : 0, Credit = x.Sum(y => y.Debit - y.Credit) < 0 ? x.Sum(y => y.Debit - y.Credit) : 0 }).ToListAsync().ConfigureAwait(false);
                    var h1 = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = TLDocumentDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.Closing };
                    i = 1; ;
                    foreach (var x in en)
                    {
                        h1.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                    }
                    _uow.TLDocumentHeaders.Add(h1);
                    fromNumber++;
                }
                else if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
                {
                    en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentNumber >= fromNumber && x.AccDocumentHeader.DocumentNumber <= toNumber && x.AccDocumentHeader.Status != StatusEnum.Draft)
                                .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title })
                                .Select(x => new { TLDocumentItemDate = x.Key.Month, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit - y.Credit) > 0 ? x.Sum(y => y.Debit - y.Credit) : 0, Credit = x.Sum(y => y.Debit - y.Credit) < 0 ? x.Sum(y => y.Debit - y.Credit) : 0 }).ToListAsync().ConfigureAwait(false);
                    var h1 = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = TLDocumentDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.Closing };
                    i = 1; ;
                    foreach (var x in en)
                    {
                        h1.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                    }
                    _uow.TLDocumentHeaders.Add(h1);
                    fromNumber++;
                }

            }
            if (tLDocumentTypeEnum.HasFlag(TLDocumentTypeEnum.Opening))
            {
                var result = await _uow.AccDocumentItems.AnyAsync(x => x.AccDocumentHeader.DocumentNumber >= fromNumber && x.AccDocumentHeader.DocumentNumber <= toNumber && x.AccDocumentHeader.TypeDocumentId == 3);
                if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Daily)
                {
                    en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentNumber >= fromNumber && x.AccDocumentHeader.DocumentNumber <= toNumber && x.AccDocumentHeader.Status == StatusEnum.Permanent)
                         .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TL.TLId, x.SL.TL.Title })
                         .Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit - y.Credit) > 0 ? x.Sum(y => y.Debit - y.Credit) : 0, Credit = x.Sum(y => y.Debit - y.Credit) < 0 ? x.Sum(y => y.Debit - y.Credit) : 0 }).ToListAsync().ConfigureAwait(false);
                    var h1 = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = TLDocumentDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.Opening };
                    i = 1; ;
                    foreach (var x in en)
                    {
                        h1.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                    }
                    _uow.TLDocumentHeaders.Add(h1);
                    fromNumber++;
                }
                else if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
                {
                    en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentNumber >= fromNumber && x.AccDocumentHeader.DocumentNumber <= toNumber && x.AccDocumentHeader.Status == StatusEnum.Permanent)
                                .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title })
                                .Select(x => new { TLDocumentItemDate = x.Key.Month, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit - y.Credit) > 0 ? x.Sum(y => y.Debit - y.Credit) : 0, Credit = x.Sum(y => y.Debit - y.Credit) < 0 ? x.Sum(y => y.Debit - y.Credit) : 0 }).ToListAsync().ConfigureAwait(false);
                    var h1 = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = TLDocumentDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.Opening };
                    i = 1; ;
                    foreach (var x in en)
                    {
                        h1.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                    }
                    _uow.TLDocumentHeaders.Add(h1);
                    fromNumber++;
                }

            }
            if (tLDocumentTypeEnum.HasFlag(TLDocumentTypeEnum.Litter))
            {
                var result = await _uow.AccDocumentItems.AnyAsync(x => x.AccDocumentHeader.DocumentNumber >= fromNumber && x.AccDocumentHeader.DocumentNumber <= toNumber && x.AccDocumentHeader.TypeDocumentId == 6);
                if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Daily)
                {
                    en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentNumber >= fromNumber && x.AccDocumentHeader.DocumentNumber <= toNumber && x.AccDocumentHeader.Status == StatusEnum.Permanent)
                         .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TL.TLId, x.SL.TL.Title })
                         .Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit - y.Credit) > 0 ? x.Sum(y => y.Debit - y.Credit) : 0, Credit = x.Sum(y => y.Debit - y.Credit) < 0 ? x.Sum(y => y.Debit - y.Credit) : 0 }).ToListAsync().ConfigureAwait(false);
                    var h1 = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = TLDocumentDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.Litter };
                    i = 1; ;
                    foreach (var x in en)
                    {
                        h1.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                    }
                    _uow.TLDocumentHeaders.Add(h1);
                    fromNumber++;
                }
                else if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
                {
                    en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentNumber >= fromNumber && x.AccDocumentHeader.DocumentNumber <= toNumber && x.AccDocumentHeader.Status == StatusEnum.Permanent)
                                .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title })
                                .Select(x => new { TLDocumentItemDate = x.Key.Month, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit - y.Credit) > 0 ? x.Sum(y => y.Debit - y.Credit) : 0, Credit = x.Sum(y => y.Debit - y.Credit) < 0 ? x.Sum(y => y.Debit - y.Credit) : 0 }).ToListAsync().ConfigureAwait(false);
                    var h1 = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = TLDocumentDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.Litter };
                    i = 1; ;
                    foreach (var x in en)
                    {
                        h1.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                    }
                    _uow.TLDocumentHeaders.Add(h1);
                    fromNumber++;
                }

            }
            if (tLDocumentTypeEnum.HasFlag(TLDocumentTypeEnum.Profit))
            {
                var result = await _uow.AccDocumentItems.AnyAsync(x => x.AccDocumentHeader.DocumentNumber >= fromNumber && x.AccDocumentHeader.DocumentNumber <= toNumber && x.AccDocumentHeader.TypeDocumentId == 1);
                if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Daily)
                {
                    en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentNumber >= fromNumber && x.AccDocumentHeader.DocumentNumber <= toNumber && x.AccDocumentHeader.Status == StatusEnum.Permanent)
                         .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Day, x.SL.TL.TLId, x.SL.TL.Title })
                         .Select(x => new { TLDocumentItemDate = x.Key.Day, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit - y.Credit) > 0 ? x.Sum(y => y.Debit - y.Credit) : 0, Credit = x.Sum(y => y.Debit - y.Credit) < 0 ? x.Sum(y => y.Debit - y.Credit) : 0 }).ToListAsync().ConfigureAwait(false);
                    var h1 = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = TLDocumentDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.Profit };
                    i = 1; ;
                    foreach (var x in en)
                    {
                        h1.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                    }
                    _uow.TLDocumentHeaders.Add(h1);
                    fromNumber++;
                }
                else if (result == true && tLDocumentExportEnum == TLDocumentExportEnum.Monthly)
                {
                    en = await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentNumber >= fromNumber && x.AccDocumentHeader.DocumentNumber <= toNumber && x.AccDocumentHeader.Status == StatusEnum.Permanent)
                                .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title })
                                .Select(x => new { TLDocumentItemDate = x.Key.Month, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit - y.Credit) > 0 ? x.Sum(y => y.Debit - y.Credit) : 0, Credit = x.Sum(y => y.Debit - y.Credit) < 0 ? x.Sum(y => y.Debit - y.Credit) * -1 : 0 }).ToListAsync().ConfigureAwait(false);
                    var h1 = new TLDocumentHeader { TLDocumentExport = tLDocumentExportEnum, TLDocumentHeaderDate = TLDocumentDate, TLDocumentNumber = fromNumber, TLDocumentType = TLDocumentTypeEnum.Profit };
                    i = 1; ;
                    foreach (var x in en)
                    {
                        h1.TLDocumentItems.Add(new TLDocumentItem { TLDocumentItemCode = i++, TLDocumentItemDate = TLDocumentDate, TLId = x.TLId, TLTitle = x.TLTitle, Debit = x.Debit, Credit = x.Credit });
                    }
                    _uow.TLDocumentHeaders.Add(h1);
                    fromNumber++;
                }

            }
            await _uow.SaveChangesAsync().ConfigureAwait(false);

        }


        public Task GetExportDocumentNumber(int toNumber, DateTime fromDate, DateTime toDate, TLDocumentExportEnum tLDocumentExportEnum, TLDocumentTypeEnum tLDocumentTypeEnum)
        {
            throw new NotImplementedException();
        }
        //        public async Task<List<TLDocumentItem>> GetMonthlyDocument(DateTime fromDate, DateTime toDate)
        //        {
        //            return await _uow.AccDocumentItems.Where(x => x.AccDocumentHeader.DocumentDate >= fromDate && x.AccDocumentHeader.DocumentDate <= toDate)
        //                 .GroupBy(x => new { x.AccDocumentHeader.DocumentDate.Month, x.SL.TL.TLId, x.SL.TL.Title })
        //                 .Select(x => new TLDocumentItem { TLDocumentItemDate = x.Key.Month, TLId = x.Key.TLId, TLTitle = x.Key.Title, Debit = x.Sum(y => y.Debit), Credit = x.Sum(y => y.Credit) }).ToListAsync().ConfigureAwait(false);

        //        }

        //        public async Task<List<TLDocumentGroupedDTO>> GetDocumentNumber(int fromNumber, int toNumber)
        //        {
        //            var originalCulture = Thread.CurrentThread.CurrentCulture;
        //            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        //            var query = $@"  SELECT tl.TLId, tl.TLCode,tl.Title TLTitle, SUM(di.Debit) SumDebit,SUM(di.Credit) SumCredit FROM Acc.AccDocumentItems di
        // INNER JOIN Acc.AccDocumentHeaders h ON h.AccDocumentHeaderId = di.AccDocumentHeaderId
        // INNER JOIN Info.SLs sl ON sl.SLId = di.SLId
        // INNER JOIN Info.TLs tl ON tl.TLId = sl.TLId
        // WHERE h.DocumentNumber BETWEEN '{fromNumber}' AND '{toNumber}'
        //GROUP BY tl.TLId, tl.TLCode,tl.Title
        //        ";

        //            Thread.CurrentThread.CurrentCulture = originalCulture;
        //            var result = await _uow.Database.SqlQuery<TLDocumentGroupedDTO>(query).ToListAsync().ConfigureAwait(false);
        //            return result;
        //        }

        //        public async Task<List<TLDocumentGroupedDTO>> GetDocumentDate(DateTime fromDate, DateTime toDate)
        //        {
        //            var originalCulture = Thread.CurrentThread.CurrentCulture;
        //            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        //            var query = $@"  SELECT tl.TLId, tl.TLCode,tl.Title TLTitle, SUM(di.Debit) SumDebit,SUM(di.Credit) SumCredit FROM Acc.AccDocumentItems di
        // INNER JOIN Acc.AccDocumentHeaders h ON h.AccDocumentHeaderId = di.AccDocumentHeaderId
        // INNER JOIN Info.SLs sl ON sl.SLId = di.SLId
        // INNER JOIN Info.TLs tl ON tl.TLId = sl.TLId
        // WHERE h.DocumentDate BETWEEN '{fromDate}' AND '{toDate}'
        //GROUP BY tl.TLId, tl.TLCode,tl.Title
        //        ";

        //            Thread.CurrentThread.CurrentCulture = originalCulture;
        //            var result = await _uow.Database.SqlQuery<TLDocumentGroupedDTO>(query).ToListAsync().ConfigureAwait(false);
        //            return result;
        // }
        #endregion



    }
}
