using AutoMapper;
using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
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
using System.Windows.Input;

namespace Saina.WPF.Accounting.DocumentAccounting.CurrencyExchangeinfo
{
    /// <summary>
    /// لیست تسعیر ارز
    /// </summary>
    class CurrencyExchangeListViewModel : BindableBase
    {
        #region Constructors
        public CurrencyExchangeListViewModel(ICurrencyExchangesService currencyExchangesService, ITLDocumentsService tLDocumentsService,
            IOpeningClosingsService openingClosingsService, IAccDocumentItemsService accDocumentItemsService,
            IAccDocumentHeadersService accDocumentHeadersService, IAppContextService appContextService, ISLsService sLsService, ICompanyInformationsService companyInformationsService)
        {
            _tLDocumentsService = tLDocumentsService;
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _accDocumentHeadersService = accDocumentHeadersService;
            _openingClosingsService = openingClosingsService;
            _currencyExchangesService = currencyExchangesService;
            _accDocumentItemsService = accDocumentItemsService;
            _sLsService = sLsService;
            _appContextService = appContextService;

            AccDocumentItemListViewModel = SmObjectFactory.Container.GetInstance<AccDocumentItemListViewModel>();
            SLsDropDownOpenedCommand = new RelayCommand(OnSLsDropDownOpened, () => SLs != null && SLs.Any());
            DLs1DropDownOpenedCommand = new RelayCommand<string>(OnDLs1DropDownOpened);
            DLs2DropDownOpenedCommand = new RelayCommand<string>(OnDLs2DropDownOpened);
            ViewCommand = new RelayCommand(OnView, CanView);
            ExportCommand = new RelayCommand(OnExport, CanExport);
            ViewSystemDocumentHeaderCommand = new RelayCommand(OnViewSystemDocumentHeader);
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();
        }

        private bool CanExport()
        {
            return !Close;
        }

        private bool CanView()
        {
            return !Close;

        }






        #endregion
        #region Fields
        private IAccDocumentHeadersService _accDocumentHeadersService;
        private IOpeningClosingsService _openingClosingsService;
        private ICurrencyExchangesService _currencyExchangesService;
        private IAccDocumentItemsService _accDocumentItemsService;

        //    private List<TypeDocument> _allTypeDocuments;
        private IAppContextService _appContextService;
        private List<SL> _allSLs;
        private List<DL> _allDLs;
        private ISLsService _sLsService;
        public CompanyInformationModel CompanyInformationModel { get; set; }

        private ITLDocumentsService _tLDocumentsService;
        private ICompanyInformationsService _companyInformationsService;

        //   private AccDocumentHeader _editingAccDocumentHeader = null;
        #endregion
        #region Commands
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand CurrenciesDropDownOpenedCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public ICommand SLsDropDownOpenedCommand { get; private set; }
        public ICommand ViewSystemDocumentHeaderCommand { get; private set; }

        private AccessUtility _accessUtility;

        public ICommand DLs1DropDownOpenedCommand { get; private set; }
        public ICommand DLs2DropDownOpenedCommand { get; private set; }
        public ICommand ViewCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }
        //  public ICommand TypeDocumentsDropDownOpenedCommand { get; private set; }

        #endregion
        #region Properties
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }
        private AccDocumentItemListViewModel _accDocumentItemListViewModel;

        public AccDocumentItemListViewModel AccDocumentItemListViewModel
        {
            get { return _accDocumentItemListViewModel; }
            set { SetProperty(ref _accDocumentItemListViewModel, value); }
        }

        private AccDocumentHeader _accDocumentHeader;
        public AccDocumentHeader AccDocumentHeader
        {
            get { return _accDocumentHeader; }
            set { SetProperty(ref _accDocumentHeader, value); }
        }
        private ObservableCollection<SL> _sLs;
        public ObservableCollection<SL> SLs
        {
            get { return _sLs; }
            set { SetProperty(ref _sLs, value); }
        }
        private string _description1;
        public string Description1
        {
            get { return _description1; }
            set { SetProperty(ref _description1, value); }
        }
        private string _description2;
        public string Description2
        {
            get { return _description2; }
            set { SetProperty(ref _description2, value); }
        }


        private SL _sL;
        public SL SL
        {
            get { return _sL; }
            set { SetProperty(ref _sL, value); AccDocumentItem.DL1Id = null; AccDocumentItem.DL2Id = null; }
        }

        private ObservableCollection<DL> _dLs1;
        public ObservableCollection<DL> DLs1
        {
            get { return _dLs1; }
            set { SetProperty(ref _dLs1, value); }
        }
        private ObservableCollection<DL> _dLs2;
        public ObservableCollection<DL> DLs2
        {
            get { return _dLs2; }
            set { SetProperty(ref _dLs2, value); }
        }
        private DateTime? _documentDate;
        public DateTime? DocumentDate
        {
            get { return _documentDate; }
            set { SetProperty(ref _documentDate, value); }
        }
        private DateTime? _lastDate;
        public DateTime? LastDate
        {
            get { return _lastDate; }
            set { SetProperty(ref _lastDate, value); }
        }
        private ObservableCollection<AccDocumentItemGroupedDTO> _accDocumentItems;
        public ObservableCollection<AccDocumentItemGroupedDTO> AccDocumentItems
        {
            get { return _accDocumentItems; }
            set { SetProperty(ref _accDocumentItems, value); }
        }
        private AccDocumentItem _accDocumentItem;
        public AccDocumentItem AccDocumentItem
        {
            get { return _accDocumentItem; }
            set { SetProperty(ref _accDocumentItem, value); }
        }

        private DateTime _endFinancialYear;

        public bool Enabled { get; private set; }

        public DateTime EndFinancialYear
        {
            get { return _endFinancialYear; }
            set { SetProperty(ref _endFinancialYear, value); }
        }
        private DateTime _StartFinancialYear1;
        public DateTime StartFinancialYear1
        {
            get { return _StartFinancialYear1; }
            set { SetProperty(ref _StartFinancialYear1, value); }
        }

        private DateTime _startFinancialYear;
        public DateTime StartFinancialYear
        {
            get { return _startFinancialYear; }
            set { SetProperty(ref _startFinancialYear, value); }
        }

        private DateTime? _accHeaderDate;
        public DateTime? AccHeaderDate
        {
            get { return _accHeaderDate; }
            set { SetProperty(ref _accHeaderDate, value); }
        }

        private int _accRow;

        public int AccRow
        {
            get { return _accRow; }
            set { SetProperty(ref _accRow, value); }
        }
        private double _debit;
        public double Debit
        {
            get { return _debit; }
            set { SetProperty(ref _debit, value); }
        }
        private double _credit;
        public double Credit
        {
            get { return _credit; }
            set { SetProperty(ref _credit, value); }
        }

        private bool _isEnable;
        public bool IsEnable
        {
            get { return _isEnable; }
            set { SetProperty(ref _isEnable, value); }
        }

        public long EndNumber { get; set; }
        public bool Close { get; private set; }


        // public event Action<Exception> Failed;
        public event Action<string> Error;
        public event Action<string> Information;
        public event Action ViewSystemDocumentHeaderRequested;
        // public long LastAccDocumentHeaderCode { get; set; }
        public event Action Done;
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
        private void OnSLsDropDownOpened()
        {
            _allSLs = _sLsService.GetSLsActiveAsync().Result;
            SLs = new ObservableCollection<SL>(_allSLs);
        }
        private void OnDLs1DropDownOpened(string dlType)
        {
            if (SL != null)
            {
                _allDLs = _sLsService.GetDLsAsync(SL.SLId, dlType).Result;
                DLs1 = new ObservableCollection<DL>(_allDLs);
            }
        }
        private void OnDLs2DropDownOpened(string dlType)
        {
            if (SL != null)
            {
                _allDLs = _sLsService.GetDLsAsync(SL.SLId, dlType).Result;
                DLs2 = new ObservableCollection<DL>(_allDLs);
            }
        }

        private void OnExport()
        {
            if (_accessUtility.HasAccess(61))
            {
                var errorMessage = "";
            var getDocumentNumbering = _accDocumentHeadersService.GetDocumentNumbering();
            var dateDocument = _appContextService.CurrentFinancialYear;
            EndNumber = getDocumentNumbering.EndNumber;
            var lastAccDocumentHeaderCode = _accDocumentHeadersService.GetLastAccDocumentHeaderCode(_appContextService.CurrentFinancialYear);
                if (lastAccDocumentHeaderCode > EndNumber)
                {
                    Error("شماره گذاری اسناد به پایان رسیده،لطفا شماره گذاری اسناد را بررسی نمایید");
                }
                else
                {
                    var startYear = _currencyExchangesService.GetStartFinancialYear(dateDocument);
                    PersianCalendar persianCalendar = new PersianCalendar();
                    if (DocumentDate == null)
                    {
                        Error("تاریخ تسعیر خالی می باشد");
                    }
                    else if (DocumentDate != null)
                    {
                        var DocDate = persianCalendar.GetYear(DocumentDate.Value);

                        if (DocDate != dateDocument)
                        {
                            Error("تاریخ تسعیر با سال مالی تطابق ندارد");
                        }

                        else
                        {
                            var GetEnd = GetEndExchange(dateDocument);
                            AccDocumentItems = new ObservableCollection<AccDocumentItemGroupedDTO>(_currencyExchangesService.GetGroupedAccDocumentItems(GetEnd.Value.Date, DocumentDate.Value.Date));
                            var c = AccDocumentItems.Count;
                            //   AccDocumentItems = new ObservableCollection<AccDocumentItemGroupedDTO>(await _currencyExchangesService.GetGroupedAccDocumentItemsAsync(StartFinancialYear.Date, DocumentDate.Value.Date));
                            if (!AccDocumentItems.Any())
                            {
                                Error("سندی تا این تاریخ برای تسعیر وجود ندارد");
                            }
                            else
                            {
                                if (AccHeaderDate == null)
                                {
                                    Error("تاریخ سند خالی می باشد");
                                }

                                else if (AccHeaderDate != null)
                                {
                                    var accHeaderDate = persianCalendar.GetYear(AccHeaderDate.Value);

                                    if (accHeaderDate > dateDocument)
                                    {
                                        Error(" تاریخ سند با سال مالی تطابق ندارد");
                                    }

                                    else
                                    {
                                        if (SL == null)
                                        {
                                            errorMessage += $"لطفا معین خود را انتخاب کنید {Environment.NewLine}";

                                            // Error("معین خود را انتخاب کنید.");
                                        }
                                        else if (SL != null)
                                        {
                                            if (SL.DLType1 != 0 && DLs1 == null)
                                                errorMessage += $"تفصیل اول نمی تواند خالی باشد {Environment.NewLine}";
                                            if (SL.DLType2 != 0 && DLs2 == null)
                                                errorMessage += $" تفصیل دوم نمی تواند خالی باشد {Environment.NewLine}";
                                            if (Description1 == "" || Description1 == null)
                                                errorMessage += $" شرح آرتیکل 1  نباید خالی باشد {Environment.NewLine}";


                                        }
                                        if (errorMessage.Length > 0)
                                        {
                                            Error(errorMessage);
                                        }
                                        else
                                        {
                                            var getHeader = _tLDocumentsService.GetAccDocumentHeaders(GetEnd.Value.Date, DocumentDate.Value.Date);
                                            if (getHeader == true)
                                            {
                                                Information?.Invoke("در بین اسناد وضعیت پیش نویس وجود دارد");

                                            }
                                            else
                                            {


                                                IsBusy = true;

                                                AccDocumentHeader = new AccDocumentHeader();
                                                var startNumber = getDocumentNumbering.StartNumber;

                                                if (lastAccDocumentHeaderCode == 0)
                                                {
                                                    lastAccDocumentHeaderCode = startNumber;
                                                }
                                                else
                                                {
                                                    lastAccDocumentHeaderCode++;
                                                }
                                                Enabled = false;
                                                IsEnable = false;
                                                //lastAccDocumentHeaderCode = _accDocumentHeadersService.GetLastAccDocumentHeaderCode(dateDocument);
                                                var lastDailyNumberCode = _accDocumentHeadersService.GetLastDailyNumberCode1();
                                                var stringLastAccDocumentHeaderCode = lastAccDocumentHeaderCode.ToString();
                                                var stringlastDailyNumberCode = lastDailyNumberCode.ToString();
                                                var lastAccDocumentHeadersCode = stringLastAccDocumentHeaderCode;
                                                var lastDayAccDocumentHeadersCode = stringLastAccDocumentHeaderCode;
                                                AccDocumentHeader.DailyNumber = int.Parse($"{stringlastDailyNumberCode}");
                                                AccDocumentHeader.DocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                                                AccDocumentHeader.ManualDocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                                                AccDocumentHeader.SystemFixNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                                                AccDocumentHeader.HeaderDescription = "هدر سند تسعیر";
                                                AccDocumentHeader.DocumentDate = AccHeaderDate.Value;
                                                AccDocumentHeader.Exporter = _appContextService.CurrentUser.FriendlyName;
                                                AccDocumentHeader.SystemName = System.Environment.MachineName;
                                                AccDocumentHeader.Status = StatusEnum.Temporary;

                                                AccDocumentHeader.TypeDocumentId = 6;
                                                AccRow = 1;
                                                string AccRowCode = AccRow.ToString();
                                                //  var editingAccDocumentHeader = Mapper.Map<EditableAccDocumentHeader, AccDocumentHeader>(AccDocumentHeader);
                                                // Mapper.Map( AccDocumentItemListViewModel.AccDocumentItems.ToList(), editingAccDocumentHeader.AccDocumentItems);
                                                if (DocumentDate == null)
                                                {
                                                    DocumentDate = _currencyExchangesService.GetEndFinancialYear1(dateDocument);
                                                }
                                                var accItems = new ObservableCollection<AccDocumentItemGroupedDTO>(_currencyExchangesService.GetGroupedAccDocumentItems(GetEnd.Value.Date, DocumentDate.Value.Date));
                                                var accList = AccDocumentItems.Select((xd, i) => new AccDocumentItem
                                                {
                                                    AccDocumentHeaderId = AccDocumentHeader.AccDocumentHeaderId,
                                                    CurrencyId = xd.CurrencyId,
                                                    SLId = (xd.SLId),
                                                    DL1Id = (xd.DL1Id),
                                                    DL2Id = (xd.DL2Id),
                                                    Description1 = Description1,
                                                    Description2 = Description2,
                                                    Credit = (xd.SumDebit - xd.SumCredit) < 0 ? Math.Abs(xd.SumDebit - xd.SumCredit) : 0,
                                                    Debit = (xd.SumDebit - xd.SumCredit) > 0 ? Math.Abs(xd.SumDebit - xd.SumCredit) : 0,
                                                    //  HasExchange= xd.HasExchange = true

                                                });
                                                using (var _uow = new Saina.Data.Context.SainaDbContext())
                                                {
                                                    foreach (var item in AccDocumentItems)
                                                    {

                                                        var cmd = $@"UPDATE Acc.AccDocumentItems SET HasExchange=1 WHERE AccDocumentItemId={item.AccDocumentItemId} ";

                                                        _uow.Database.ExecuteSqlCommand(cmd);

                                                    }
                                                }
                                                AccDocumentHeader.AccDocumentItems = new ObservableCollection<AccDocumentItem>(accList.ToList());
                                                _accDocumentHeadersService.AddAccDocumentHeader(AccDocumentHeader);
                                                // _accDocumentItemsService.AddAccDocumentItemsAsync(accList);
                                                var remainRial = accList.Sum(di => di.Debit - di.Credit);

                                                // var remainCurrencies = sumDebitCurrencies - sumCreditCurrencies;
                                                var remainCurrencies = _currencyExchangesService.GetRemainExchanges(GetEnd.Value.Date, DocumentDate.Value.Date);
                                                if (remainCurrencies == null)
                                                {
                                                    remainCurrencies = 0;
                                                }
                                                var lastCurrency = _currencyExchangesService.GetLastCurrencyDocAsync();

                                                var remain = (remainCurrencies) - remainRial;
                                                if (remain > 0)
                                                {
                                                    Debit = Math.Abs(remain.Value);
                                                    Credit = 0;
                                                }
                                                else
                                                {
                                                    Credit = Math.Abs(remain.Value);
                                                    Debit = 0;
                                                }
                                                if (remain != 0)
                                                {
                                                    var accItem = new AccDocumentItem { AccDocumentHeaderId = AccDocumentHeader.AccDocumentHeaderId, SLId = (SL.SLId), DL1Id = AccDocumentItem.DL1Id, DL2Id = AccDocumentItem.DL2Id, Description1 = Description1, Description2 = Description2, Credit = Credit, Debit = Debit };
                                                    _accDocumentItemsService.AddAccDocumentItemAsync(accItem);
                                                }


                                                EndFinancialYear = _currencyExchangesService.GetEndFinancialYear1(_appContextService.CurrentFinancialYear);
                                                GetEnd = GetEndExchange(dateDocument);

                                                //  AccDocumentItems = new ObservableCollection<AccDocumentItemGroupedDTO>(await _currencyExchangesService.GetGroupedAccDocumentItemsAsync(GetEnd.Value.Date, EndFinancialYear.Date));
                                                AccDocumentItems = new ObservableCollection<AccDocumentItemGroupedDTO>(_currencyExchangesService.GetGroupedAccDocumentItems(GetEnd.Value.Date, EndFinancialYear.Date));
                                                Information("سند با موفقیت ثبت شد");

                                                Enabled = true;
                                                IsEnable = true;
                                                IsBusy = false;
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
        //}
        private async void OnView()
        {
            if (_accessUtility.HasAccess(63))
            {
                var dateDocument = _appContextService.CurrentFinancialYear;



                if (DocumentDate == null)
                {
                    Error("تاریخ را وارد نمایید");


                }
                else
                {
                    PersianCalendar persianCalendar = new PersianCalendar();
                    var x = persianCalendar.GetYear(DocumentDate.Value);

                    if (x > dateDocument)
                    {
                        Error("تاریخ تسعیر را درست وارد نمایید");
                    }
                    else
                    {
                        var GetEnd = GetEndExchange(dateDocument);
                        AccDocumentItems = new ObservableCollection<AccDocumentItemGroupedDTO>(_currencyExchangesService.GetGroupedAccDocumentItems(GetEnd.Value.Date, DocumentDate.Value.Date));
                    }
                }
            }
        }
        //}
        public async void LoadCurrencyExchanges()
        {
            //باید سندهایی که از آخرین تسعیر تا الان هستند را نشان دهند
            // برگشتی حتما کاملش کن

            AccDocumentItem = new AccDocumentItem();
            var dateDocument = _appContextService.CurrentFinancialYear;
            Close = _openingClosingsService.GetCloseAcc(dateDocument);
            EndFinancialYear = _currencyExchangesService.GetEndFinancialYear1(dateDocument);
            PersianCalendar persianCalendar = new PersianCalendar();

            var accDate = persianCalendar.GetYear(DateTime.Now);
            //  var endFinancialYear = await _currencyExchangesService.GetEndFinancialYear(dateDocument);
            StartFinancialYear = _currencyExchangesService.GetStartFinancialYear1(dateDocument);
            var GetEnd = GetEndExchange(dateDocument);

            if (Close == false)
            {
                //  IsBusy = true;
                IsEnable = true;
                if (dateDocument == accDate)
                {
                    AccHeaderDate = DateTime.Now;
                    DocumentDate = DateTime.Now;
                }
                else
                {
                    AccHeaderDate = EndFinancialYear;
                    DocumentDate = EndFinancialYear;

                }
                if (SLs == null)
                {

                    _allSLs = _sLsService.GetSLsActiveAsync().Result;
                    SLs = new ObservableCollection<SL>(_allSLs);
                }


                AccDocumentItems = new ObservableCollection<AccDocumentItemGroupedDTO>(_currencyExchangesService.GetGroupedAccDocumentItems(GetEnd.Value.Date

                    , EndFinancialYear.Date));
                //  IsBusy = false;
            }
            else
            {
                IsEnable = false;
            }
            //IsBusy = false;

            _appContextService.PropertyChanged += async (sender, eventArgs) =>
            {
                var appContextService = sender as IAppContextService;
                if (eventArgs.PropertyName == "CurrentFinancialYear")
                    dateDocument = _appContextService.CurrentFinancialYear;
                EndFinancialYear = _currencyExchangesService.GetEndFinancialYear1(dateDocument);
                StartFinancialYear = _currencyExchangesService.GetStartFinancialYear1(dateDocument);
                GetEnd = GetEndExchange(dateDocument);
                Close = _openingClosingsService.GetCloseAcc(dateDocument);
                if (Close == false)
                {
                    IsBusy = true;
                    IsEnable = true;
                    if (dateDocument == accDate)
                    {
                        AccHeaderDate = DateTime.Now;
                    }
                    else
                    {
                        AccHeaderDate = EndFinancialYear;
                    }
                    //    LastDate = await _currencyExchangesService.GetLastDateDocAsync(dateDocument);
                    AccDocumentItems = new ObservableCollection<AccDocumentItemGroupedDTO>(_currencyExchangesService.GetGroupedAccDocumentItems(GetEnd.Value.Date, EndFinancialYear.Date));
                    IsBusy = false;
                }
                else
                {
                    IsEnable = false;
                }
            };

        }

        private DateTime? GetEndExchange(int dateDocument)
        {
            StartFinancialYear1 = _currencyExchangesService.GetStartFinancialYear1(dateDocument);
            var GetEnd = (_currencyExchangesService.GetEndExchangeDoc(dateDocument));
            ///*  var GetEndEx =*/ GetEnd;
            if (GetEnd == null)
            {
                var getAcc = _accDocumentHeadersService.GetFirstAcc(dateDocument);
                if (getAcc != null)
                {
                    GetEnd = getAcc;
                }
                else
                {
                    GetEnd = StartFinancialYear1;
                }
            }

            return GetEnd;
        }


        #endregion
    }
}
