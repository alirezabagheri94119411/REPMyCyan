using AutoMapper;
using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.WPF.Accounting.DocumentAccounting.CurrencyExchangeinfo;
using Saina.WPF.Accounting.DocumentAccounting.DocumentHeader;
using Saina.WPF.Accounting.DocumentAccounting.ItemDocument;
using Saina.WPF.Behaviors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Saina.WPF.Accounting.DocumentAccounting.OpenClose
{
    class OpeningClosingListViewModel : BindableBase
    {
        #region Constructors
        public OpeningClosingListViewModel(IOpeningClosingsService openingClosingsService, ITLDocumentsService tLDocumentsService, IAppContextService appContextService, IAccDocumentItemsService accDocumentItemsService, ICurrencyExchangesService currencyExchangesService, IAccDocumentHeadersService accDocumentHeadersService)
        {
            _tLDocumentsService = tLDocumentsService;
            _accDocumentItemsService = accDocumentItemsService;
            _accDocumentHeadersService = accDocumentHeadersService;
            _currencyExchangesService = currencyExchangesService;
            _openingClosingsService = openingClosingsService;
            _appContextService = appContextService;
            AddAccDocumentCommand = new RelayCommand(OnAddAccDocument);
            DocumentDate = DateTime.Now;
            ViewSystemDocumentHeaderCommand = new RelayCommand(OnViewSystemDocumentHeader);
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();

        }
        #endregion
        #region Fields
        private IOpeningClosingsService _openingClosingsService;
        private IAppContextService _appContextService;
        private ITLDocumentsService _tLDocumentsService;
        private IAccDocumentItemsService _accDocumentItemsService;
        private IAccDocumentHeadersService _accDocumentHeadersService;
        private ICurrencyExchangesService _currencyExchangesService;
        private List<AccDocumentItem> _allAccDocumentItems;

        //   private ReviewAccount _editingReviewAccount = null;
        #endregion
        #region Commands
        public ICommand ViewSystemDocumentHeaderCommand { get; private set; }
        public AccessUtility _accessUtility { get; }
        public ICommand AddAccDocumentCommand { get; private set; }
        //  public event Action<AccDocumentItem> AddAccDocumentItemRequested;
        public event Action Done;
        public event Action<string> Error; 
        public event Action<string> Information; 
        public event Action ViewSystemDocumentHeaderRequested;

        #endregion
        #region Properties

        private AccDocumentItemListViewModel _accDocumentItemListViewModel;

        public AccDocumentItemListViewModel AccDocumentItemListViewModel
        {
            get { return _accDocumentItemListViewModel; }
            set { SetProperty(ref _accDocumentItemListViewModel, value); }
        }
        private ObservableCollection<AccDocumentItemOpenCloseDTO> _accDocumentItems;
        public ObservableCollection<AccDocumentItemOpenCloseDTO> AccDocumentItems
        {
            get { return _accDocumentItems; }
            set { SetProperty(ref _accDocumentItems, value); }
        }
        private bool _closing;
        public bool Closing
        {
            get { return _closing; }
            set { SetProperty(ref _closing, value); }
        }
        private bool _opening;
        public bool Opening
        {
            get { return _opening; }
            set { SetProperty(ref _opening, value); }
        }

        private DateTime _documentDate;
        public DateTime DocumentDate
        {
            get { return _documentDate; }
            set { SetProperty(ref _documentDate, value); }
        }

        private AccDocumentHeader _accDocumentHeader;
        public AccDocumentHeader AccDocumentHeader
        {
            get { return _accDocumentHeader; }
            set { SetProperty(ref _accDocumentHeader, value); }
        }
        private string _checkType;
        public string CheckType
        {
            get { return _checkType; }
            set { SetProperty(ref _checkType, value); }
        }
        private bool _enable;
        public bool Enable
        {
            get { return _enable; }
            set { SetProperty(ref _enable, value); }
        }
        private int _getAcc;
        public int GetAcc
        {
            get { return _getAcc; }
            set { SetProperty(ref _getAcc, value); }
        }
        private int _typeHeader;
        public int TypeHeader
        {
            get { return _typeHeader; }
            set { SetProperty(ref _typeHeader, value); }
        }

        private int _accRow;
        public int AccRow
        {
            get { return _accRow; }
            set { SetProperty(ref _accRow, value); }
        }

        public long EndNumber { get; set; }
        private DateTime _startYear;
        public DateTime StartYear
        {
            get { return _startYear; }
            set { SetProperty(ref _startYear, value); }
        }
        private DateTime _endYear;
        //private object persianCalendar;

        public DateTime EndYear
        {
            get { return _endYear; }
            set { SetProperty(ref _endYear, value); }
        }

        #endregion
        #region Methode
        //step1
        public event Action<TypeEnum> AccRequested;
        private TypeEnum _TypeEnum;
        public TypeEnum TypeEnum
        {
            get { return _TypeEnum; }
            set { SetProperty(ref _TypeEnum, value); }
        }
        internal void RaiseTestRequested(TypeEnum typeEnum)
        {
            AccRequested?.Invoke(typeEnum);
        }
        private void OnViewSystemDocumentHeader()
        {
            ViewSystemDocumentHeaderRequested();
        }
        public async void LoadOpeningClosing()
        {
            var dateDocument = _appContextService.CurrentFinancialYear;

            var getAcc = await _openingClosingsService.GetAccDocumentItemsAsync(dateDocument);
            GetAcc = getAcc.Count;
            Opening = await _openingClosingsService.GetCloseAccAsync(dateDocument);
            var newOpening = await _openingClosingsService.GetOpenAccAsync(dateDocument + 1);
            Closing = await _openingClosingsService.GetCloseProfitLossAccountAsync(dateDocument);
            if (GetAcc > 0 && Closing == true && Opening == false)
            {
                CheckType = "اختتامیه";
                Enable = true;
                TypeHeader = 4;
            }

            else if (GetAcc > 0 && Opening == true && newOpening == false)
            {
                CheckType = "افتتاحیه";
                Enable = true;
                TypeHeader = 3;
            }

            else if (GetAcc > 0 && Opening == true && newOpening == true)
            {
                CheckType = "سند افتتاحیه و اختتامیه صادر شده است";
                Enable = false;

            }
            else if (GetAcc > 0 && Closing == false)
            {
                CheckType = "سندهای سود و زیانی بسته نشده است";
                Enable = false;
            }
            else if (GetAcc == 0)
            {
                CheckType = "هیچ سندی صادر نشده است.";
                Enable = false;
            }

            _appContextService.PropertyChanged += async (sender, eventArgs) =>
             {
                 var appContextService = sender as IAppContextService;
                 if (eventArgs.PropertyName == "CurrentFinancialYear")
                     dateDocument = _appContextService.CurrentFinancialYear;

                 getAcc =  _openingClosingsService.GetAccDocumentItemsAsync(dateDocument).Result;
                 GetAcc = getAcc.Count;
                 Opening =  _openingClosingsService.GetCloseAccAsync(dateDocument).Result;
                 newOpening =  _openingClosingsService.GetOpenAccAsync(dateDocument + 1).GetAwaiter().GetResult();
                 Closing =  _openingClosingsService.GetCloseProfitLossAccountAsync(dateDocument).Result;
                 if (GetAcc > 0 && Closing == true && Opening == false)
                 {
                     CheckType = "اختتامیه";
                     Enable = true;
                 }

                 else if (GetAcc > 0 && Opening == true && newOpening == false)
                 {
                     CheckType = "افتتاحیه";
                     Enable = true;

                 }

                 else if (GetAcc > 0 && Opening == true && newOpening == true)
                 {
                     CheckType = "سند افتتاحیه و اختتامیه صادر شده است";
                     Enable = false;

                 }
                 else if (GetAcc > 0 && Closing == false)
                 {
                     CheckType = "سندهای سود و زیانی بسته نشده است";
                     Enable = false;
                 }
                 else if (GetAcc == 0)
                 {
                     CheckType = "هیچ سندی صادر نشده است.";
                     Enable = false;
                 }
             };
        }
        private async void OnAddAccDocument()
        {
            AccDocumentHeader = new AccDocumentHeader();

            var getDocumentNumbering = await _accDocumentHeadersService.GetDocumentNumberingAsync();
            var dateDocument = _appContextService.CurrentFinancialYear;
            StartYear = await _currencyExchangesService.GetStartFinancialYear(dateDocument);
            EndYear = await _currencyExchangesService.GetEndFinancialYear(dateDocument);
            EndNumber = getDocumentNumbering.EndNumber;
            var lastAccDocumentHeaderCode = _accDocumentHeadersService.GetLastAccDocumentHeaderCode(_appContextService.CurrentFinancialYear);
            if (lastAccDocumentHeaderCode > EndNumber)
            {
                Error("شماره گذاری اسناد به پایان رسیده،لطفا شماره گذاری اسناد را بررسی نمایید");
            }
            else
            {
                if (DocumentDate == null)
                {
                    Error(".تاریخ سند خالی می باشد");

                }
                else if (DocumentDate != null)
                {
                    PersianCalendar persianCalendar = new PersianCalendar();

                    var accHeaderDate = persianCalendar.GetYear(DocumentDate.Date);

                    if (accHeaderDate != dateDocument)
                    {
                        Error("تاریخ سند را درست وارد نمایید");
                    }


                    else
                    {
                        var getHeader = _tLDocumentsService.GetAccDocumentHeaders(StartYear.Date, DocumentDate.Date);
                        if (getHeader == true)
                        {
                            Information?.Invoke("در بین اسناد وضعیت پیش نویس وجود دارد");

                        }
                        else
                        {

                            var startNumber = getDocumentNumbering.StartNumber;

                            if (lastAccDocumentHeaderCode == 0)
                            {
                                lastAccDocumentHeaderCode = startNumber;
                            }
                            else
                            {
                                lastAccDocumentHeaderCode++;
                            }

                            var accountDocumentId = getDocumentNumbering.AccountDocumentId;
                            var style = getDocumentNumbering.StyleId;
                            var countingWayId = getDocumentNumbering.CountingWayId;

                            //  var lastAccDocumentHeaderCode = _accDocumentHeadersService.GetLastAccDocumentHeaderCode(_appContextService.CurrentFinancialYear);
                            var lastDailyNumberCode = await _accDocumentHeadersService.GetLastDailyNumberCode();
                            if (lastAccDocumentHeaderCode == 0)
                            {
                                lastAccDocumentHeaderCode = startNumber;
                            }
                            else
                            {
                                lastAccDocumentHeaderCode++;
                            }
                            var stringLastAccDocumentHeaderCode = lastAccDocumentHeaderCode.ToString();
                            var stringlastDailyNumberCode = lastDailyNumberCode.ToString();
                            var lastAccDocumentHeadersCode = stringLastAccDocumentHeaderCode;
                            var lastDayAccDocumentHeadersCode = stringLastAccDocumentHeaderCode;
                            if (lastAccDocumentHeaderCode <= EndNumber)
                            {
                                //  var accDocumentHeader = Mapper.Map<EditableAccDocumentHeader, AccDocumentHeader>(AccDocumentHeader);
                                AccDocumentHeader.DailyNumber = int.Parse($"{stringlastDailyNumberCode}");
                                AccDocumentHeader.DocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                                AccDocumentHeader.ManualDocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                                AccDocumentHeader.SystemFixNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                                AccDocumentHeader.SystemName = Environment.MachineName;
                                AccDocumentHeader.Exporter = _appContextService.CurrentUser.FriendlyName;
                                AccDocumentHeader.Status = StatusEnum.Temporary;
                                AccDocumentHeader.DocumentDate = DocumentDate;
                                AccRow = 1;
                                string AccRowCode = AccRow.ToString();
                                if (TypeHeader == 4)
                                {
                                    AccDocumentItems = new ObservableCollection<AccDocumentItemOpenCloseDTO>(await _openingClosingsService.AddCloseAccAsync(StartYear.Date, EndYear.Date));

                                    AccDocumentHeader.HeaderDescription = "بابت سند اختتامیه";
                                    AccDocumentHeader.TypeDocumentId = 4;
                                    await _accDocumentHeadersService.AddAccDocumentHeaderAsync(AccDocumentHeader);

                                    var accItems = new ObservableCollection<AccDocumentItemOpenCloseDTO>(await _openingClosingsService.AddCloseAccAsync(StartYear.Date, EndYear.Date));
                                    var accList = AccDocumentItems.Select((xd, i) => new AccDocumentItem
                                    {
                                        AccDocumentHeaderId = AccDocumentHeader.AccDocumentHeaderId,
                                        SLId = (xd.SLId),
                                        DL1Id = (xd.DL1Id),
                                        DL2Id = (xd.DL2Id),
                                        Description1 = "بابت سند اختتامیه",
                                        Description2 = "بابت سند اختتامیه",
                                        Credit = (xd.SumDebit - xd.SumCredit) < 0 ? Math.Abs(xd.SumDebit - xd.SumCredit) : 0,
                                        Debit = (xd.SumDebit - xd.SumCredit) > 0 ? Math.Abs(xd.SumDebit - xd.SumCredit) : 0,
                                        CurrencyId = xd.CurrencyId,
                                        CurrencyAmount = xd.SumCurrencyAmount
                                    });
                                    _accDocumentItemsService.AddAccDocumentItemsAsync(accList);
                                }
                                else if (TypeHeader == 3)
                                {
                                    AccDocumentItems = new ObservableCollection<AccDocumentItemOpenCloseDTO>(await _openingClosingsService.AddOpenAccAsync(StartYear.Date, EndYear.Date));
                                    AccDocumentHeader.DocumentDate = StartYear.AddYears(1);
                                    AccDocumentHeader.HeaderDescription = "بابت سند افتتاحیه";
                                    AccDocumentHeader.TypeDocumentId = 3;

                                    await _accDocumentHeadersService.AddAccDocumentHeaderAsync(AccDocumentHeader);

                                    var accItems = new ObservableCollection<AccDocumentItemOpenCloseDTO>(await _openingClosingsService.AddOpenAccAsync(StartYear.Date, EndYear.Date));
                                    var accList = AccDocumentItems.Select((xd, i) => new AccDocumentItem
                                    {
                                        AccDocumentHeaderId = AccDocumentHeader.AccDocumentHeaderId,
                                        SLId = (xd.SLId),
                                        DL1Id = (xd.DL1Id),
                                        DL2Id = (xd.DL2Id),
                                        Description1 = "بابت سند افتتاحیه",
                                        Description2 = "بابت سند افتتاحیه",
                                        Credit = (xd.SumDebit - xd.SumCredit) < 0 ? Math.Abs(xd.SumDebit - xd.SumCredit) : 0,
                                        Debit = (xd.SumDebit - xd.SumCredit) > 0 ? Math.Abs(xd.SumDebit - xd.SumCredit) : 0,
                                        CurrencyId = xd.CurrencyId,
                                        CurrencyAmount = xd.SumCurrencyAmount
                                    });
                                    _accDocumentItemsService.AddAccDocumentItemsAsync(accList);
                                }
                                // await _accDocumentHeadersService.AddAccDocumentHeaderAsync(AccDocumentHeader);


                                dateDocument = _appContextService.CurrentFinancialYear;

                                var getAcc = await _openingClosingsService.GetAccDocumentItemsAsync(dateDocument);
                                GetAcc = getAcc.Count;
                                Opening = await _openingClosingsService.GetCloseAccAsync(dateDocument);
                                var newOpening = await _openingClosingsService.GetOpenAccAsync(dateDocument + 1);
                                Closing = await _openingClosingsService.GetCloseProfitLossAccountAsync(dateDocument);
                                if (GetAcc > 0 && Closing == true && Opening == false)
                                {
                                    CheckType = "اختتامیه";
                                    Enable = true;
                                }

                                else if (GetAcc > 0 && Opening == true && newOpening == false)
                                {
                                    CheckType = "افتتاحیه";
                                    Enable = true;

                                }

                                else if (GetAcc > 0 && Opening == true && newOpening == true)
                                {
                                    CheckType = "سند افتتاحیه و اختتامیه صادر شده است";
                                    Enable = false;

                                }
                                else if (GetAcc > 0 && Closing == false)
                                {
                                    CheckType = "سندهای سود و زیانی بسته نشده است";
                                    Enable = false;
                                }
                                else if (GetAcc == 0)
                                {
                                    CheckType = "هیچ سندی صادر نشده است.";
                                    Enable = false;
                                }
                                Information("سند با موفقیت ثبت شد");

                            }

                        }
                    }
                }
            }
        }

        // AddAccDocumentItemRequested(accDocumentItem);
    }
}



#endregion

