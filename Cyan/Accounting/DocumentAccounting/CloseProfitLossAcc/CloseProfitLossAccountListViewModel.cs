
using AutoMapper;
using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.WPF.Accounting.DocumentAccounting.DocumentHeader;
using Saina.WPF.Accounting.DocumentAccounting.ItemDocument;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Saina.Common.Toolkit;
using System.Globalization;
using Saina.WPF.Accounting.DocumentAccounting.CurrencyExchangeinfo;
using Saina.ApplicationCore.DTOs;
using Saina.WPF.Behaviors;

namespace Saina.WPF.Accounting.DocumentAccounting.CloseProfitLossAcc
{
    class CloseProfitLossAccountListViewModel : BindableBase
    {
        #region Constructors
        public CloseProfitLossAccountListViewModel(ICloseProfitLossAccountsService closeProfitLossAccountsService, ITLDocumentsService tLDocumentsService,
            IOpeningClosingsService openingClosingsService, IAccDocumentItemsService accDocumentItemsService,
            ICurrencyExchangesService currencyExchangesService, IAccDocumentHeadersService accDocumentHeadersService, 
            IAppContextService appContextService, ISLsService sLsService, ICompanyInformationsService companyInformationsService)
        {
            _tLDocumentsService = tLDocumentsService;
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _accDocumentHeadersService = accDocumentHeadersService;
            _openingClosingsService = openingClosingsService;
            _accDocumentItemsService = accDocumentItemsService;
            _currencyExchangesService = currencyExchangesService;
            _closeProfitLossAccountsService = closeProfitLossAccountsService;
            _sLsService = sLsService;
            _appContextService = appContextService;
            AccDocumentItemListViewModel = SmObjectFactory.Container.GetInstance<AccDocumentItemListViewModel>();
            SLsDropDownOpenedCommand = new RelayCommand(OnSLsDropDownOpened, () => SLs != null && SLs.Any());
            DLs1DropDownOpenedCommand = new RelayCommand<string>(OnDLs1DropDownOpened);
            DLs2DropDownOpenedCommand = new RelayCommand<string>(OnDLs2DropDownOpened);
            //TransferCommand = new RelayCommand<ObservableCollection<object>>(OnTransfer, (o) => AccDocumentItems.Count > 0);
            //AllTransferCommand = new RelayCommand<ObservableCollection<object>>(OnAllTransfer, (o) => AccDocumentItems.Count > 0);
            //AllReturnCommand = new RelayCommand<ObservableCollection<object>>(OnAllReturn, (o) => AccDocumentItems1.Count > 0);
            //ReturnCommand = new RelayCommand<ObservableCollection<object>>(OnReturn, (o) => AccDocumentItems1.Count > 0);
            TransferCommand = new RelayCommand<ObservableCollection<object>>(OnTransfer);
            AllTransferCommand = new RelayCommand<ObservableCollection<object>>(OnAllTransfer);
            AllReturnCommand = new RelayCommand<ObservableCollection<object>>(OnAllReturn);
            ReturnCommand = new RelayCommand<ObservableCollection<object>>(OnReturn);
            ExportCommand = new RelayCommand(OnExport, ()=> !Close);
            ViewSystemDocumentHeaderCommand = new RelayCommand(OnViewSystemDocumentHeader);
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();
            // Close = true;
            //   TypeDocumentsDropDownOpenedCommand = new RelayCommand(OnTypeDocumentsDropDownOpened);


        }

        private void AccDocumentItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            (TransferCommand).RaiseCanExecuteChanged();
            (AllTransferCommand).RaiseCanExecuteChanged();
            (ReturnCommand).RaiseCanExecuteChanged();
            (AllReturnCommand).RaiseCanExecuteChanged();
        }
        private void AccDocumentItems1_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            (TransferCommand).RaiseCanExecuteChanged();
            (AllTransferCommand).RaiseCanExecuteChanged();
            (ReturnCommand).RaiseCanExecuteChanged();
            (AllReturnCommand).RaiseCanExecuteChanged();
        }



        #endregion
        #region Fields
        private IAccDocumentHeadersService _accDocumentHeadersService;
        private IOpeningClosingsService _openingClosingsService;
        private IAccDocumentItemsService _accDocumentItemsService;
        private ICurrencyExchangesService _currencyExchangesService;
        private ICloseProfitLossAccountsService _closeProfitLossAccountsService;
        public CompanyInformationModel CompanyInformationModel { get; set; }

        private ITLDocumentsService _tLDocumentsService;
        private ICompanyInformationsService _companyInformationsService;
        //    private List<TypeDocument> _allTypeDocuments;
        private IAppContextService _appContextService;
        private List<SL> _allSLs;
        private List<DL> _allDLs;
        private ISLsService _sLsService;
        //   private AccDocumentHeader _editingAccDocumentHeader = null;
        #endregion
        #region Commands
        public RelayCommand CurrenciesDropDownOpenedCommand { get; private set; }
        public ICommand SLsDropDownOpenedCommand { get; private set; }
        public ICommand DLs1DropDownOpenedCommand { get; private set; }
        public ICommand DLs2DropDownOpenedCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }
        public RelayCommand<ObservableCollection<object>> ReturnCommand { get; private set; }
        public RelayCommand<ObservableCollection<object>> AllReturnCommand { get; private set; }
        public ICommand ViewSystemDocumentHeaderCommand { get; private set; }

        private AccessUtility _accessUtility;

        public RelayCommand<ObservableCollection<object>> TransferCommand { get; private set; }
        public RelayCommand<ObservableCollection<object>> AllTransferCommand { get; private set; }

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

        private SL _sL;
        public SL SL
        {
            get { return _sL; }
            set { SetProperty(ref _sL, value); }
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
        private ObservableCollection<AccDocumentItemGroupedDTO> _SelectedAccDocumentItems;
        public ObservableCollection<AccDocumentItemGroupedDTO> SelectedAccDocumentItems
        {
            get { return _SelectedAccDocumentItems; }
            set { SetProperty(ref _SelectedAccDocumentItems, value); }
        }

        private DateTime _endFinancialYear;
        public DateTime EndFinancialYear
        {
            get { return _endFinancialYear; }
            set { SetProperty(ref _endFinancialYear, value); }
        }
        private AccDocumentItem _accDocumentItem;
        public AccDocumentItem AccDocumentItem
        {
            get { return _accDocumentItem; }
            set { SetProperty(ref _accDocumentItem, value); }
        }
        private DateTime _startFinancialYear;
        public DateTime StartFinancialYear
        {
            get { return _startFinancialYear; }
            set { SetProperty(ref _startFinancialYear, value); }
        }

        private ObservableCollection<AccDocumentItemGroupedDTO> _accDocumentItems;
        public ObservableCollection<AccDocumentItemGroupedDTO> AccDocumentItems
        {
            get { return _accDocumentItems; }
            set
            {
                SetProperty(ref _accDocumentItems, value);
                //if (AccDocumentItems!=null)
                //{
                //    AccDocumentItems.CollectionChanged += AccDocumentItems_CollectionChanged;

                //}
                
            }
        }
        private ObservableCollection<AccDocumentItem> _AccDocumentItemsProfit;

        public ObservableCollection<AccDocumentItem> AccDocumentItemsProfit
        {
            get { return _AccDocumentItemsProfit; }
            set { _AccDocumentItemsProfit = value; }
        }

        private ObservableCollection<AccDocumentItemGroupedDTO> _accDocumentItems1;
        public ObservableCollection<AccDocumentItemGroupedDTO> AccDocumentItems1
        {
            get { return _accDocumentItems1; }
            set { SetProperty(ref _accDocumentItems1, value);
                //if (AccDocumentItems1 != null)
                //{
                //    AccDocumentItems1.CollectionChanged += AccDocumentItems1_CollectionChanged;

                //}
            }
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
        private DateTime? _documentDate;
        public DateTime? DocumentDate
        {
            get { return _documentDate; }
            set { SetProperty(ref _documentDate, value); }
        }
        private bool _close;
        public bool Close
        {
            get { return _close; }
            set { SetProperty(ref _close, value); }
        }
        private bool _isEnable;
        public bool IsEnable
        {
            get { return _isEnable; }
            set { SetProperty(ref _isEnable, value); }
        }
        public long EndNumber { get; set; }
        public event Action<Exception> Failed;
        public event Action<string> Error;
        public event Action<string> Information;
        public event Action ViewSystemDocumentHeaderRequested;

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
        private bool CanTransfer(ObservableCollection<object> arg)
        {
            return AccDocumentItems.Count > 0;


        }

        private bool CanAllTransfer(ObservableCollection<object> arg)
        {
            return AccDocumentItems.Count>0;

        }

        private bool CanAllReturn(ObservableCollection<object> arg)
        {
            return AccDocumentItems1.Count > 0;
            
        }

        private bool CanReturn(ObservableCollection<object> arg)
        {
            return AccDocumentItems1.Count > 0;
            
        }

        private bool CanExport()
        {
            return !Close;
        }
        private void OnAllTransfer(ObservableCollection<object> selectedAccDocumentItems)
        {
            AccDocumentItems1 = AccDocumentItems;
            AccDocumentItems = new ObservableCollection<AccDocumentItemGroupedDTO>();
        }

        private void OnAllReturn(ObservableCollection<object> selectedAccDocumentItems)
        {
            AccDocumentItems = AccDocumentItems1;
            AccDocumentItems1 = new ObservableCollection<AccDocumentItemGroupedDTO>();
        }

        private void OnReturn(ObservableCollection<object> selectedAccDocumentItems)
        {

            var count = selectedAccDocumentItems.Count;
            if (count != 0)
                for (int i = 0; i < count; i++)
                {
                    //if (!AccDocumentItems1.Any(x => SelectedAccDocumentItems[i].PublicInstancePropertiesEqual(x)))
                    AccDocumentItems.Add(selectedAccDocumentItems[i] as AccDocumentItemGroupedDTO);
                    //AccDocumentItems.Remove(selectedAccDocumentItems[i] as AccDocumentItemGroupedDTO);

                }
            //AccDocumentItems1.ToList().AddRange(selectedAccDocumentItems.Cast<AccDocumentItemGroupedDTO>());

            AccDocumentItems1 = new ObservableCollection<AccDocumentItemGroupedDTO>(AccDocumentItems1.Except(selectedAccDocumentItems.Cast<AccDocumentItemGroupedDTO>()));
        }

        private void OnTransfer(ObservableCollection<object> selectedAccDocumentItems)
        {
            // انتقال

            var count = selectedAccDocumentItems.Count;
            if (count != 0)
                for (int i = 0; i < count; i++)
                {
                    //if (!AccDocumentItems1.Any(x => SelectedAccDocumentItems[i].PublicInstancePropertiesEqual(x)))
                    AccDocumentItems1.Add(selectedAccDocumentItems[i] as AccDocumentItemGroupedDTO);
                    //AccDocumentItems.Remove(selectedAccDocumentItems[i] as AccDocumentItemGroupedDTO);

                }
            //AccDocumentItems1.ToList().AddRange(selectedAccDocumentItems.Cast<AccDocumentItemGroupedDTO>());
            AccDocumentItems = new ObservableCollection<AccDocumentItemGroupedDTO>(AccDocumentItems.Except(selectedAccDocumentItems.Cast<AccDocumentItemGroupedDTO>()));
        }
        private async void OnSLsDropDownOpened()
        {
            _allSLs = await _sLsService.GetSLsActiveAsync();
            SLs = new ObservableCollection<SL>(_allSLs);
        }
        private async void OnDLs1DropDownOpened(string dlType)
        {
            if (SL != null)
            {
                _allDLs = await _sLsService.GetDLsAsync(SL.SLId, dlType);
                DLs1 = new ObservableCollection<DL>(_allDLs);
            }
        }
        private async void OnDLs2DropDownOpened(string dlType)
        {
            if (SL != null)
            {
                _allDLs = await _sLsService.GetDLsAsync(SL.SLId, dlType);
                DLs2 = new ObservableCollection<DL>(_allDLs);
            }
        }

        public async void LoadCloseProfitLosss()
        {
            IsBusy = true;
            AccDocumentItem = new AccDocumentItem();
            var dateDocument = _appContextService.CurrentFinancialYear;
            Close = await _openingClosingsService.GetCloseAccAsync(dateDocument);

            EndFinancialYear = await _currencyExchangesService.GetEndFinancialYear(dateDocument);
            PersianCalendar persianCalendar = new PersianCalendar();

            var accDate = persianCalendar.GetYear(DateTime.Now);
            StartFinancialYear = await _currencyExchangesService.GetStartFinancialYear(dateDocument);
            if (Close == false)
            {
                IsEnable = true;

                if (dateDocument == accDate)
                {
                    AccHeaderDate = DateTime.Now;
                }
                else
                {
                    AccHeaderDate = EndFinancialYear;
                }


                if (SLs == null)
                {

                    _allSLs = await _sLsService.GetSLsActiveAsync();
                    SLs = new ObservableCollection<SL>(_allSLs);
                }



               AccDocumentItems = new ObservableCollection<AccDocumentItemGroupedDTO>( _closeProfitLossAccountsService.GetGroupedAccDocumentItems(dateDocument));
                IsBusy = false;
            }
            else
            {
                IsEnable = false;
            }
            IsBusy = false;
            _appContextService.PropertyChanged += async (sender, eventArgs) =>
            {
                var appContextService = sender as IAppContextService;
                if (eventArgs.PropertyName == "CurrentFinancialYear")
                    dateDocument = _appContextService.CurrentFinancialYear;
                if (Close == false)
                {

                    IsEnable = true;
                    IsBusy = true;
                    StartFinancialYear =  _currencyExchangesService.GetStartFinancialYear1(dateDocument);

                    EndFinancialYear =  _currencyExchangesService.GetEndFinancialYear1(dateDocument);
                    AccDocumentItems = new ObservableCollection<AccDocumentItemGroupedDTO>( _closeProfitLossAccountsService.GetGroupedAccDocumentItems(_appContextService.CurrentFinancialYear));
                    IsBusy = false;
                }
                else
                {
                    IsEnable = false;
                }
            };
            SelectedAccDocumentItems = new ObservableCollection<AccDocumentItemGroupedDTO>();
            AccDocumentItems1 = new ObservableCollection<AccDocumentItemGroupedDTO>();

        }
        // public long lastAccDocumentHeaderCode { get; set; }
        private async void OnExport()
        {
            if (_accessUtility.HasAccess(68))
            {
                var errorMessage = "";
                var getDocumentNumbering = await _accDocumentHeadersService.GetDocumentNumberingAsync();
                var dateDocument = _appContextService.CurrentFinancialYear;
                EndNumber = getDocumentNumbering.EndNumber;
                var lastAccDocumentHeaderCode = _accDocumentHeadersService.GetLastAccDocumentHeaderCode(_appContextService.CurrentFinancialYear);
                if (lastAccDocumentHeaderCode > EndNumber)
                {
                    Error("شماره گذاری اسناد به پایان رسیده،لطفا شماره گذاری اسناد را بررسی نمایید");
                }
                else
                {
                    if (AccDocumentItems1.Count == 0)
                    {
                        Error("سندی برای سود زیانی وجود ندارد");
                    }
                    else
                    {
                        var startYear = _currencyExchangesService.GetStartFinancialYear(dateDocument);

                        PersianCalendar persianCalendar = new PersianCalendar();
                        if (AccHeaderDate == null)
                        {
                            Error("تاریخ سند خالی می باشد");
                        }
                        else
                        {
                            var x = persianCalendar.GetYear(AccHeaderDate.Value);

                            if (x != dateDocument)
                            {
                                Error("تاریخ را درست وارد نمایید");
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

                                    var startNumber = getDocumentNumbering.StartNumber;

                                    if (lastAccDocumentHeaderCode == 0)
                                    {
                                        lastAccDocumentHeaderCode = startNumber;
                                    }
                                    else
                                    {
                                        lastAccDocumentHeaderCode++;
                                    }
                                    AccDocumentHeader = new AccDocumentHeader();
                                    var lastDailyNumberCode = await _accDocumentHeadersService.GetLastDailyNumberCode();
                                    var stringLastAccDocumentHeaderCode = lastAccDocumentHeaderCode.ToString();
                                    var stringlastDailyNumberCode = lastDailyNumberCode.ToString();
                                    var lastAccDocumentHeadersCode = stringLastAccDocumentHeaderCode;
                                    var lastDayAccDocumentHeadersCode = stringLastAccDocumentHeaderCode;
                                    AccDocumentHeader.DailyNumber = int.Parse($"{stringlastDailyNumberCode}");
                                    AccDocumentHeader.DocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                                    AccDocumentHeader.ManualDocumentNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                                    AccDocumentHeader.SystemFixNumber = int.Parse($"{lastAccDocumentHeadersCode}");
                                    AccDocumentHeader.HeaderDescription = "هدر سند سود و زیانی";
                                    AccDocumentHeader.Status = StatusEnum.Temporary;
                                    AccDocumentHeader.DocumentDate = AccHeaderDate.Value;
                                    AccDocumentHeader.Exporter = _appContextService.CurrentUser.FriendlyName;
                                    AccDocumentHeader.SystemName = System.Environment.MachineName;
                                    AccDocumentHeader.TypeDocumentId = 1;
                                    AccRow = 1;
                                    //  string AccRowCode = AccRow.ToString();
                                    //  var editingAccDocumentHeader = Mapper.Map<EditableAccDocumentHeader, AccDocumentHeader>(AccDocumentHeader);
                                    // Mapper.Map( AccDocumentItemListViewModel.AccDocumentItems.ToList(), editingAccDocumentHeader.AccDocumentItems);
                                    await _accDocumentHeadersService.AddAccDocumentHeaderAsync(AccDocumentHeader);
                                    if (DocumentDate == null)
                                    {
                                        DocumentDate = await _currencyExchangesService.GetEndFinancialYear(dateDocument);
                                    }
                                    var accItems = new ObservableCollection<AccDocumentItemGroupedDTO>(_closeProfitLossAccountsService.GetGroupedAccDocumentItems(_appContextService.CurrentFinancialYear));
                                    var accList = AccDocumentItems1.Select((xd, i) => new AccDocumentItem { AccDocumentHeaderId = AccDocumentHeader.AccDocumentHeaderId, SLId = (xd.SLId), DL1Id = (xd.DL1Id), DL2Id = (xd.DL2Id), Description1 = Description1, Description2 = Description2, Credit = (xd.SumDebit - xd.SumCredit) < 0 ? Math.Abs(xd.SumDebit - xd.SumCredit) : 0, Debit = (xd.SumDebit - xd.SumCredit) > 0 ? Math.Abs(xd.SumDebit - xd.SumCredit) : 0 });
                                    _accDocumentItemsService.AddAccDocumentItemsAsync(accList);
                                    var remainSum = accList.Sum(di => di.Debit - di.Credit);
                                    if (remainSum > 0)
                                    {
                                        Credit = remainSum;
                                        Debit = 0;
                                    }
                                    else
                                    {
                                        Debit = Math.Abs(remainSum);
                                        Credit = 0;

                                    }
                                    if (remainSum != 0)
                                    {
                                        var accItem = new AccDocumentItem { AccDocumentHeaderId = AccDocumentHeader.AccDocumentHeaderId, SLId = (SL.SLId), DL1Id = AccDocumentItem.DL1Id, DL2Id = AccDocumentItem.DL2Id, Description1 = Description1, Description2 = Description2, Credit = Credit, Debit = Debit };
                                        _accDocumentItemsService.AddAccDocumentItemAsync(accItem);
                                    }
                                    EndFinancialYear = await _currencyExchangesService.GetEndFinancialYear(dateDocument);

                                    AccDocumentItems = new ObservableCollection<AccDocumentItemGroupedDTO>(_closeProfitLossAccountsService.GetGroupedAccDocumentItems(_appContextService.CurrentFinancialYear));
                                    AccDocumentItems1 = null;
                                    AccDocumentItems1 = new ObservableCollection<AccDocumentItemGroupedDTO>();
                                    //  AccDocumentItems1 = new ObservableCollection<AccDocumentItemGroupedDTO>(AccDocumentItems1.Except(selectedAccDocumentItems.Cast<AccDocumentItemGroupedDTO>()));

                                    Information("سند با موفقیت ثبت شد");
                                }
                            }
                        }
                    }
                }
            }
        }
            #endregion
        }
    }
