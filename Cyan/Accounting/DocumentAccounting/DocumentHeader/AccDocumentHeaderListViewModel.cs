using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.WPF.Accounting.DocumentAccounting.ItemDocument;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Saina.Data.Context;
using Telerik.Windows.Data;
using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using Telerik.Windows.Persistence;
using static Saina.WPF.Accounting.DocumentAccounting.DocumentHeader.AccDocumentHeaderListView;
using Saina.ApplicationCore.DTOs;
using System.Threading;
using System.Globalization;
using Saina.WPF.Accounting.DocumentAccounting.CurrencyExchangeinfo;
using Saina.WPF.Behaviors;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentHeader
{
    /// <summary>
    /// لیست هدر سند حسابداری
    /// </summary>
    public class AccDocumentHeaderListViewModel : BindableBase
    {
        #region Constructors
        public AccDocumentHeaderListViewModel(IAppContextService appContextService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _appContextService = appContextService;
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();

        }
        #endregion
        #region Fields

        private SainaDbContext _uow;
        private ICollectionView _AccDocumentHeaders;
        public CompanyInformationModel CompanyInformationModel { get; set; }
        private ICompanyInformationsService _companyInformationsService;

        #endregion
        #region Commands
        private IAppContextService _appContextService;
        private AccessUtility _accessUtility;
        #endregion
        #region Properties
        public ICollectionView AccDocumentHeaders
        {
            get { return _AccDocumentHeaders; }
            set
            {
                SetProperty(ref _AccDocumentHeaders, value);
                if (_AccDocumentHeaders != null)
                {

                    foreach (var item in _AccDocumentHeaders)
                    {
                        (item as AccDocumentHeader).PropertyChanged += AccDocumentHeaderListViewModel_PropertyChanged;
                    }
                }
            }
        }

        private void AccDocumentHeaderListViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DocumentDate")
            {
                var date = ((AccDocumentHeader)AccDocumentHeaders.CurrentItem);
                var daily = _uow.Database.SqlQuery<long?>($"select MAX(DailyNumber)+1 from Acc.AccDocumentHeaders where YEAR(DocumentDate)={ date.DocumentDate.Year} and MONTH(DocumentDate)={ date.DocumentDate.Month} and DAY(DocumentDate)={ date.DocumentDate.Day}").FirstOrDefault();
                if (daily == null)
                {
                    daily = 1;
                }
                date.DailyNumber = daily.Value;
                //long lastDailyNumberCode = _uow.AccDocumentHeaders.Where(x =>
                //x.DocumentDate.Day == date.DocumentDate.Day
                // && x.DocumentDate.Month == date.DocumentDate.Month
                // && x.DocumentDate.Year == date.DocumentDate.Year).Select(x => x.DailyNumber).DefaultIfEmpty(1).Max();
            }
        }
        private int _DateFlow;

        public int DateFlow
        {
            get { return _DateFlow; }
            set { SetProperty(ref _DateFlow, value); }
        }
        private bool _HasFlow;

        public bool HasFlow
        {
            get { return _HasFlow; }
            set { SetProperty(ref _HasFlow, value); }
        }

        private AccDocumentItem _AccDocumentItem;
        public AccDocumentItem AccDocumentItem
        {
            get { return _AccDocumentItem; }
            set
            {
                SetProperty(ref _AccDocumentItem, value);
                if (_AccDocumentItem != null)
                {
                    _AccDocumentItem.PropertyChanged -= _AccDocumentItem_PropertyChanged;
                    _AccDocumentItem.PropertyChanged += _AccDocumentItem_PropertyChanged;
                }
            }
        }
        private DateTime? _documentDate;
        public DateTime? DocumentDate
        {
            get { return _documentDate; }
            set { SetProperty(ref _documentDate, value); }
        }
        private void _AccDocumentItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var SLQuery = AccountsNatureEnum.None;
            if (e.PropertyName == "SLId")
            {
                var slid = AccDocumentItem?.SLId;
                if (slid != null)
                {
                    var Query = _uow.SLs.Where(x => x.SLId == slid).FirstOrDefault();
                    SLQuery = Query.AccountsNatureEnum;
                }
                else
                {
                    SLQuery = AccountsNatureEnum.Debt;
                }

            }
            if (e.PropertyName == "CurrencyId")
            {
                if (AccDocumentItem?.CurrencyId != null)
                {
                    var exchangeRate = _uow.ExchangeRates.OrderBy(x => x.EffectiveDate).Select(x => new { x.CurrencyId, x.ParityRate }).FirstOrDefault(x => x.CurrencyId == AccDocumentItem.CurrencyId);
                    AccDocumentItem.ExchangeRate = exchangeRate != null ? exchangeRate.ParityRate : 1;
                    if (AccDocumentItem.Debit > 0 && AccDocumentItem.ExchangeRate != 0)
                    {
                        AccDocumentItem.CurrencyAmount = (AccDocumentItem.Debit / AccDocumentItem.ExchangeRate);
                    }
                    else if (AccDocumentItem.Credit > 0 && AccDocumentItem.ExchangeRate != 0)
                    {
                        AccDocumentItem.CurrencyAmount = (AccDocumentItem.Credit / AccDocumentItem.ExchangeRate);
                    }
                    else if (AccDocumentItem.Debit == 0 && AccDocumentItem.Credit == 0 && AccDocumentItem.ExchangeRate != 0)
                    {
                        AccDocumentItem.CurrencyAmount = 1;
                        if (SLQuery == AccountsNatureEnum.Debt)
                        {
                            AccDocumentItem.Debit = (AccDocumentItem.ExchangeRate * AccDocumentItem.CurrencyAmount);

                        }
                        else if (SLQuery == AccountsNatureEnum.Cred)
                        {
                            AccDocumentItem.Credit = (AccDocumentItem.ExchangeRate * AccDocumentItem.CurrencyAmount);

                        }
                        else if (SLQuery == AccountsNatureEnum.None)
                        {
                            AccDocumentItem.Debit = (AccDocumentItem.ExchangeRate * AccDocumentItem.CurrencyAmount);

                        }

                    }

                }
            }
            else if (e.PropertyName == "ExchangeRate")
            {
                if (AccDocumentItem?.CurrencyId != null)
                {

                    if (AccDocumentItem.Debit > 0)
                    {
                        AccDocumentItem.CurrencyAmount = (AccDocumentItem.Debit / AccDocumentItem.ExchangeRate);
                    }
                    else if (AccDocumentItem.Credit > 0)
                    {
                        AccDocumentItem.CurrencyAmount = (AccDocumentItem.Credit / AccDocumentItem.ExchangeRate);
                    }


                }
            }
            else if (e.PropertyName == "CurrencyAmount")
            {
                if (AccDocumentItem?.CurrencyId != null)
                {

                    if (AccDocumentItem.Debit > 0 && AccDocumentItem.ExchangeRate != 0)
                    {
                        AccDocumentItem.ExchangeRate = (AccDocumentItem.Debit / AccDocumentItem.CurrencyAmount);
                    }
                    else if (AccDocumentItem.Credit > 0 && AccDocumentItem.ExchangeRate != 0)
                    {
                        AccDocumentItem.ExchangeRate = (AccDocumentItem.Credit / AccDocumentItem.CurrencyAmount);
                    }
                    else if (AccDocumentItem.Debit > 0 && AccDocumentItem.ExchangeRate == 0)
                    {
                        AccDocumentItem.ExchangeRate = 1;
                        AccDocumentItem.CurrencyAmount = (AccDocumentItem.Debit / AccDocumentItem.ExchangeRate);
                    }
                    else if (AccDocumentItem.Credit > 0 && AccDocumentItem.ExchangeRate == 0)
                    {
                        AccDocumentItem.ExchangeRate = 1;
                        AccDocumentItem.CurrencyAmount = (AccDocumentItem.Credit / AccDocumentItem.ExchangeRate);
                    }
                    else if (AccDocumentItem.Credit == 0 && AccDocumentItem.Debit == 0 && AccDocumentItem.ExchangeRate == 0)
                    {
                        AccDocumentItem.CurrencyAmount = 0;
                    }


                }
            }
            else if (e.PropertyName == "Crdit")
            {
                if (AccDocumentItem?.CurrencyId != null)
                {
                    if (AccDocumentItem.Credit > 0 && (AccDocumentItem.CurrencyAmount != 0) && AccDocumentItem.ExchangeRate == 0)
                    {
                        AccDocumentItem.ExchangeRate = (AccDocumentItem.Credit / AccDocumentItem.CurrencyAmount);
                    }
                   else if (AccDocumentItem.Credit > 0 && (AccDocumentItem.CurrencyAmount== 0) && AccDocumentItem.ExchangeRate != 0)
                    {
                        AccDocumentItem.CurrencyAmount = (AccDocumentItem.Credit / AccDocumentItem.ExchangeRate);
                    }
                    else if (AccDocumentItem.Credit > 0 && (AccDocumentItem.CurrencyAmount != 0) && AccDocumentItem.ExchangeRate != 0)
                    {
                        AccDocumentItem.CurrencyAmount = (AccDocumentItem.Credit / AccDocumentItem.ExchangeRate);
                    }


                }
            }
            else if (e.PropertyName == "Debit")
            {
                if (AccDocumentItem?.CurrencyId != null)
                {
                    if (AccDocumentItem.Debit > 0 && (AccDocumentItem.CurrencyAmount != 0) && AccDocumentItem.ExchangeRate == 0)
                    {
                        AccDocumentItem.ExchangeRate = (AccDocumentItem.Credit / AccDocumentItem.CurrencyAmount);
                    }
                    else if (AccDocumentItem.Debit > 0 && (AccDocumentItem.CurrencyAmount == 0) && AccDocumentItem.ExchangeRate != 0)
                    {
                        AccDocumentItem.CurrencyAmount = (AccDocumentItem.Credit / AccDocumentItem.ExchangeRate);
                    }
                    else if (AccDocumentItem.Debit > 0 && (AccDocumentItem.CurrencyAmount != 0) && AccDocumentItem.ExchangeRate != 0)
                    {
                        AccDocumentItem.CurrencyAmount = (AccDocumentItem.Credit / AccDocumentItem.ExchangeRate);
                    }
                }
            }
        }
        private IEnumerable<SL> _sLs;
        public IEnumerable<SL> SLs
        {
            get { return _sLs; }
            set { SetProperty(ref _sLs, value); }
        }
        private IEnumerable<DL> _DLs;
        public IEnumerable<DL> DLs
        {
            get { return _DLs; }
            set { SetProperty(ref _DLs, value); }
        }
        private IEnumerable<Currency> _Currencies;
        public IEnumerable<Currency> Currencies
        {
            get { return _Currencies; }
            set { SetProperty(ref _Currencies, value); }
        }
        private IEnumerable<TypeDocument> _typeDocuments;
        public IEnumerable<TypeDocument> TypeDocuments
        {
            get { return _typeDocuments; }
            set { SetProperty(ref _typeDocuments, value); }
        }
        private List<SLStandardDescription> _SLStandardDescriptions;
        public List<SLStandardDescription> SLStandardDescriptions
        {
            get { return _SLStandardDescriptions; }
            set { SetProperty(ref _SLStandardDescriptions, value); }
        }
        private int _HeaderId;
        public int HeaderId
        {
            get { return _HeaderId; }
            set { SetProperty(ref _HeaderId, value); }
        }
        public event Action<SLDLEnum> SLRequested;
        public bool ContextHasChanges => _uow.ContextHasChanges;
       

        //type
        #endregion
        #region Methods
        public void DocumentNumbering()
        {
            var order = _uow.AccDocumentHeaders.OrderBy(x => x.DocumentDate).ThenBy(x => x.DailyNumber).ToList();
            var getDocumentNumbering = _uow.Set<DocumentNumbering>().AsNoTracking().FirstOrDefault(x => x.AccountDocumentId == 1);
            var startNumber = getDocumentNumbering.StartNumber;
            for (long i = 1; i < order.Count(); i++)
            {
                int ii = Convert.ToInt32(i);
                order[ii].DocumentNumber = startNumber;
                startNumber++;
            }
            _uow.SaveChanges();
            var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter);

            var alert = new RadDesktopAlert
            {
                FlowDirection = FlowDirection.RightToLeft,
                Header = "اطلاعات",
                Content = ".شماره گذاری با موفقیت انجام شد",
                ShowDuration = 2000,
            };
            manager.ShowAlert(alert);
        }
        public void RaiseSLRequested(SLDLEnum sLDLEnum)
        {
            SLRequested?.Invoke(sLDLEnum);
        }
        private int _TypeEnum;

        public int TypeEnum
        {
            get { return _TypeEnum; }
            set { _TypeEnum = value; }
        }

        public void Loaded()
        {
            _uow = new SainaDbContext();
            var ids = new List<int> { 1, 3, 4, 5, 6 };
            TypeDocuments = _uow.TypeDocuments.Where(x => !ids.Contains(x.TypeDocumentId)).ToList();
            SLs = _uow.SLs.Where(x => x.Status == true).ToList();
            DLs = _uow.DLs.ToList();
            SLStandardDescriptions = _uow.SLStandardDescriptions.ToList();
            Currencies = _uow.Currencies.ToList();
            
            AccDocumentHeaders = new QueryableCollectionView(_uow.AccDocumentHeaders.Include(x => x.AccDocumentItems.Select(s => s.SL)).Where(x => _uow.ShamsiToMiladi(x.DocumentDate, "Saal") == _appContextService.CurrentFinancialYear.ToString()
             && (HeaderId == 0 || x.AccDocumentHeaderId == HeaderId)
                     && (TypeEnum == 0 || x.TypeDocumentId == TypeEnum)).OrderBy(x=>x.DocumentNumber).ToList());
           
            
            _appContextService.PropertyChanged += (sender, eventArgs) =>
           {
               try
               {
                   if (eventArgs.PropertyName == "CurrentFinancialYear")
                   {
                       var dateDocument = _appContextService.CurrentFinancialYear;
                       ids = new List<int> { 1, 3, 4, 5, 6 };
                       TypeDocuments = _uow.TypeDocuments.Where(x => !ids.Contains(x.TypeDocumentId)).ToList();
                       SLs = _uow.SLs.Where(x => x.Status == true).ToList();
                       DLs = _uow.DLs.ToList();
                       SLStandardDescriptions = _uow.SLStandardDescriptions.ToList();
                       Currencies = _uow.Currencies.ToList();
                       // AccDocumentHeaders = new QueryableCollectionView(_uow.AccDocumentHeaders.Include(x => x.AccDocumentItems).Where(x => _uow.ShamsiToMiladi(x.DocumentDate, "Saal") == _appContextService.CurrentFinancialYear.ToString()).ToList());
                       AccDocumentHeaders = new QueryableCollectionView(_uow.AccDocumentHeaders.Include(x => x.AccDocumentItems.Select(s => s.SL)).Where(x => _uow.ShamsiToMiladi(x.DocumentDate, "Saal") == _appContextService.CurrentFinancialYear.ToString()
               && (HeaderId == 0 || x.AccDocumentHeaderId == HeaderId)
              && (TypeEnum == 0 || x.TypeDocumentId == TypeEnum)).OrderBy(x=>x.DocumentNumber).ToList());
                   }
               }
               catch (Exception)
               { }
           };
            AccDocumentHeaders.CollectionChanged += AccDocumentHeaders_CollectionChanged;


            _uow.SaveChanges();
        }
        public void LoadedFlow()
        {
            _uow = new SainaDbContext();
            var ids = new List<int> { 1, 3, 4, 5, 6 };
            TypeDocuments = _uow.TypeDocuments.Where(x => !ids.Contains(x.TypeDocumentId)).ToList();
            SLs = _uow.SLs.Where(x => x.Status == true).ToList();
            DLs = _uow.DLs.ToList();
            SLStandardDescriptions = _uow.SLStandardDescriptions.ToList();
            Currencies = _uow.Currencies.ToList();
            AccDocumentHeaders = new QueryableCollectionView(_uow.AccDocumentHeaders.Include(x => x.AccDocumentItems.Select(s => s.SL)).Where(x => _uow.ShamsiToMiladi(x.DocumentDate, "Saal") == DateFlow.ToString()
             && (HeaderId == 0 || x.AccDocumentHeaderId == HeaderId)
                     && (TypeEnum == 0 || x.TypeDocumentId == TypeEnum)).ToList());



            AccDocumentHeaders.CollectionChanged += AccDocumentHeaders_CollectionChanged;


            _uow.SaveChanges();
        }
        public void LoadRefresh()
        {
            _uow = new SainaDbContext();
            var ids = new List<int> { 1, 3, 4, 5, 6 };
            TypeDocuments = _uow.TypeDocuments.Where(x => !ids.Contains(x.TypeDocumentId)).ToList();
            SLs = _uow.SLs.Where(x => x.Status == true).ToList();
            DLs = _uow.DLs.ToList();
            SLStandardDescriptions = _uow.SLStandardDescriptions.ToList();
            Currencies = _uow.Currencies.ToList();
            AccDocumentHeaders = new QueryableCollectionView(_uow.AccDocumentHeaders.Include(x => x.AccDocumentItems.Select(s => s.SL)).Where(x => _uow.ShamsiToMiladi(x.DocumentDate, "Saal") == _appContextService.CurrentFinancialYear.ToString()
            ).ToList());
            AccDocumentHeaders.CollectionChanged += AccDocumentHeaders_CollectionChanged;
            _uow.SaveChanges();
        }
        private void AccDocumentHeaders_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                ((AccDocumentHeader)e.NewItems[0]).PropertyChanged += AccDocumentHeaderListViewModel_PropertyChanged;
            }
        }

        public override void UnLoaded()
        {
            ////this.stream.Position = 0L;
            ////PersistenceManager manager = new PersistenceManager();
            ////manager.Load(this.reviewRadGridView, this.stream);
            ////this.EnsureLoadState();
            _uow.Dispose();
            AccDocumentHeaders = null;
            HeaderId = 0;
        }
        public void AddAccDocumentHeader(AccDocumentHeader accDocumentHeader)
        {
            _uow.AccDocumentHeaders.Add(accDocumentHeader);
        }
        public int HasAcc(DateTime dateTime, TypeDocument typeDocument, StatusEnum statusEnum)
        {
            int hasAccResult = 0;
            //var cc = _uow.GLs.FirstOrDefault(x => x.GLId == id)?.TLs;
            //return _uow.GLs.FirstOrDefault(x => x.GLId == id)?.TLs?.Any() == true;
            if (statusEnum == StatusEnum.End || statusEnum == StatusEnum.Permanent)
            {
                hasAccResult = 2;
                return hasAccResult;
            }
            else
            {

                if (typeDocument.TypeDocumentId != 2)
                {
                    return hasAccResult;
                }

                else
                {
                    var result = _uow.TLDocumentHeaders.Any(x => x.TLDocumentHeaderDate == dateTime);
                    if (result == false)
                    {
                        result = _uow.AccDocumentHeaders.Any(x => x.TypeDocumentId != 2 && x.DocumentDate == dateTime);
                        if (result == true)
                        {
                            var type = _uow.AccDocumentHeaders.Where(x => x.TypeDocumentId != 2 && x.DocumentDate == dateTime).Select(x => x.TypeDocumentId).FirstOrDefault();
                            if (type == 1)
                            {
                                hasAccResult = 1;
                            }
                            else if (type == 3)
                            {
                                hasAccResult = 3;
                            }
                            else if (type == 4)
                            {
                                hasAccResult = 4;
                            }
                            else if (type == 6)
                            {
                                hasAccResult = 6;
                            }
                        }
                    }
                    else
                    {
                        hasAccResult = 5;
                    }
                    return hasAccResult;
                }
            }
            //بررسی کن اگه سندی که هیچ آیتم ندارد را حذف کند
        }
        public int HasExport(AccDocumentHeader accDocumentHeader )
        {
            int hasAccResult = 0;
            //var cc = _uow.GLs.FirstOrDefault(x => x.GLId == id)?.TLs;
            //return _uow.GLs.FirstOrDefault(x => x.GLId == id)?.TLs?.Any() == true;
          

                
                   
                      var  result = _uow.AccDocumentHeaders.Any(x => x.AccDocumentHeaderId == accDocumentHeader.AccDocumentHeaderId && x.TypeDocumentId != 2);
                            var type = _uow.AccDocumentHeaders.Where(x => x.TypeDocumentId != 2 && x.AccDocumentHeaderId == accDocumentHeader.AccDocumentHeaderId).Select(x => x.TypeDocumentId).FirstOrDefault();
                        if (type!=0)
                        {
                            if (type == 1)
                            {
                                hasAccResult = 1;
                            }
                            else if (type == 3)
                            {
                                hasAccResult = 3;
                            }
                            else if (type == 4)
                            {
                                hasAccResult = 4;
                            }
                            else if (type == 6)
                            {
                                hasAccResult = 6;
                            }
                        
                    }
                 
                    return hasAccResult;
               
           
            //بررسی کن اگه سندی که هیچ آیتم ندارد را حذف کند
        }

        public void Save()
        {
            var accDocumentHeader = AccDocumentHeaders.CurrentItem as AccDocumentHeader;
            var headers = _uow.AccDocumentHeaders.Where(x => x.AccDocumentItems.Count == 0).ToList();


            headers.RemoveAll(x => x.AccDocumentItems.Count == 0);

            if (accDocumentHeader != null)
            {
                var items = accDocumentHeader.AccDocumentItems.ToList();
                if (items.Count != 0)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i].SL != null)
                        {
                            var s = _uow.Entry(items[i].SL).State;
                            if (items[i].SL != null)
                                _uow.Entry(items[i].SL).State = EntityState.Detached;
                            if (items[i].DL1 != null)
                                _uow.Entry(items[i].DL1).State = EntityState.Detached;
                            if (items[i].DL2 != null)
                                _uow.Entry(items[i].DL2).State = EntityState.Detached;
                            if (items[i].Currency != null)
                                _uow.Entry(items[i].Currency).State = EntityState.Detached;
                        }
                    }
                }
                var accitems = _uow.AccDocumentItems.Where(x => x.AccDocumentHeaderId == accDocumentHeader.AccDocumentHeaderId).ToList();

                var xx = _uow.ChangeTracker
              .Entries()
              .Where(x => x.Entity is AccDocumentItem
               accDocumentItem && accDocumentItem.AccDocumentHeader == null).ToList();
                //var s = (xx).Cast<AccDocumentItem>();
                var count = 0;
                try
                {

                    foreach (var item in accitems)
                    {
                        count = 0;
                        foreach (var i in items)
                        {
                            if (item.AccDocumentItemId == i.AccDocumentItemId)
                            {
                                count++;
                            }
                        }
                        if (count == 0)
                        {

                            _uow.AccDocumentItems.Remove(item);
                        }

                    }


                }
                catch (Exception)
                {


                }
                var r = _uow.SaveChanges();
            }
        }
        public int DeleteItem(AccDocumentItem accDocumentItem)
        {
            //  _uow.RejectChanges();

            var q = $"delete Acc.AccDocumentItems where AccDocumentItemId={AccDocumentItem.AccDocumentItemId} ";
            return _uow.Database.ExecuteSqlCommand(q);
        }
        public bool HasHeaderItem(AccDocumentHeader accDocumentHeader)
        {
            if (_uow.AccDocumentItems.Any(x => x.AccDocumentHeaderId == accDocumentHeader.AccDocumentHeaderId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Load(AccDocumentHeader accDocumentHeader)
        {
            var items = _uow.AccDocumentItems.Where(x => x.AccDocumentHeaderId == accDocumentHeader.AccDocumentHeaderId).ToIList();


        }
        public DateTime? LastExchange()
        {
            var date = _appContextService.CurrentFinancialYear;
            var year = (_uow.AccDocumentHeaders.Select(x => new { x.DocumentDate, x.TypeDocumentId })
                .Where(x => x.TypeDocumentId == 6 && _uow.ShamsiToMiladi(x.DocumentDate, "Saal") == date.ToString()));
            if (year != null && year.Any())
            {
                return year.Max(x => x.DocumentDate);
            }
            else
            {
                return null;
            }
        }
        public DateTime? LastPermanent()
        {
            var dateDocument = _appContextService.CurrentFinancialYear;

            //var originalCulture = Thread.CurrentThread.CurrentCulture;
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //var q2 = $"SELECT Max(DocumentDate) FROM Acc.AccDocumentHeaders  WHERE dbo.ShamsiToMiladi(DocumentDate,'Saal')='{dateDocument}' and   Status={(int)StatusEnum.Permanent}";
            //var enddate =  _uow.Database.ExecuteSqlCommand(q2);
            //if (enddate != 0)
            //{
            //    var cmd = $@"UPDATE Acc.AccDocumentHeaders SET Status={(int)status} WHERE DocumentDate>='{toDate.ToShortDateString()}' and DocumentDate>='{enddate.Value.ToShortDateString()}' and Status={(int)convert} ";

            //    Thread.CurrentThread.CurrentCulture = originalCulture;
            //    return  _uow.Database.ExecuteSqlCommandAsync(cmd).ConfigureAwait(false);
            //}
            //var dateDocument = _appContextService.CurrentFinancialYear;
            var originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var endDate = _uow.AccDocumentHeaders.Where(x => (_uow.ShamsiToMiladi(x.DocumentDate, "Saal") == dateDocument.ToString() && x.Status == StatusEnum.Permanent));
            Thread.CurrentThread.CurrentCulture = originalCulture;
            if (endDate.Any())
            {
                var d = endDate.Max(x => x.DocumentDate);

                return d;
            }
            return null;
        }
        public int DeleteDocumentHeader(AccDocumentHeader accDocumentHeader)
        {

            _uow.RejectChanges();
            var q = $"delete Acc.AccDocumentItems where AccDocumentHeaderId={accDocumentHeader.AccDocumentHeaderId} delete Acc.AccDocumentHeaders where AccDocumentHeaderId={accDocumentHeader.AccDocumentHeaderId}";
            return _uow.Database.ExecuteSqlCommand(q);

        }

        public void Reject()
        {
            _uow.RejectChanges();
        }
        public void GetDaily()
        {
            long lastDailyNumberCode = _uow.AccDocumentHeaders.Where(x =>
                   x.DocumentDate.Day == DateTime.Now.Day
                    && x.DocumentDate.Month == DateTime.Now.Month
                    && x.DocumentDate.Year == DateTime.Now.Year).Select(x => x.DailyNumber).DefaultIfEmpty(1).Max();
        }
        public SL GetSLById(int sLId)
        {
            return _uow.SLs.Find(sLId);
        }

        public DL GetDLById(int dLId)
        {
            return _uow.DLs.Find(dLId);
        }
        public Currency GetCurrById(int CurrencyId)
        {
            return _uow.Currencies.Find(CurrencyId);
        }
        #endregion
    }
}
