using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Data;
using System.Data.Entity;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentSystem.CurrencyExchangeRateDoc
{
    class CurrencyExchangeRateDocHeaderListViewModel:BindableBase
    {
        //#region Constructors
        //public CurrencyExchangeRateDocHeaderListViewModel(IAccDocumentHeadersService accDocumentHeadersService, IAppContextService appContextService)
        //{

        //    _appContextService = appContextService;
        //    _accDocumentHeadersService = accDocumentHeadersService;
        //    SLsDropDownOpenedCommand = new RelayCommand(OnSLsDropDownOpened);
        //    DLsDropDownOpenedCommand = new RelayCommand<string>(OnDLsDropDownOpened);
        //    SLStandardDescriptionsDropDownOpenedCommand = new RelayCommand<AccDocumentItem>(OnSLStandardDescriptionsDropDownOpened);
        //    CurrenciesDropDownOpenedCommand = new RelayCommand(OnCurrenciesDropDownOpened);
        //}
        //#endregion
        //#region Fields
        //private SainaDbContext _uow;
        //private IEditableCollectionViewAddNewItem _AccDocumentHeaders;
        //#endregion
        //#region Commands
        //public ICommand SLsDropDownOpenedCommand { get; private set; }
        //public ICommand DLsDropDownOpenedCommand { get; private set; }
        //public ICommand SLStandardDescriptionsDropDownOpenedCommand { get; private set; }
        //public ICommand CurrenciesDropDownOpenedCommand { get; private set; }
        //public ICommand ExchangeRatesDropDownOpenedCommand { get; private set; }
        //#endregion
        //#region Properties
        //private List<AccDocumentHeader> _allAccDocumentHeaders;
        //private IAppContextService _appContextService;
        //private IAccDocumentHeadersService _accDocumentHeadersService;

        //public IEditableCollectionViewAddNewItem AccDocumentHeaders
        //{
        //    get { return _AccDocumentHeaders; }
        //    set { SetProperty(ref _AccDocumentHeaders, value); }
        //}
        //private AccDocumentItem _AccDocumentItem;
        //public AccDocumentItem AccDocumentItem
        //{
        //    get { return _AccDocumentItem; }
        //    set
        //    {
        //        SetProperty(ref _AccDocumentItem, value);
        //        if (_AccDocumentItem != null)
        //        {
        //            _AccDocumentItem.PropertyChanged -= _AccDocumentItem_PropertyChanged;
        //            _AccDocumentItem.PropertyChanged += _AccDocumentItem_PropertyChanged;
        //        }
        //    }
        //}

        //private void _AccDocumentItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == "CurrencyId")
        //    {
        //        if (AccDocumentItem?.CurrencyId != null)
        //        {
        //            AccDocumentItem.ExchangeRate = _uow.ExchangeRates.OrderBy(x => x.EffectiveDate).Select(x => new { x.CurrencyId, x.ParityRate }).FirstOrDefault(x => x.CurrencyId == AccDocumentItem.CurrencyId).ParityRate;

        //        }
        //    }
        //    else if (e.PropertyName == "CurrencyAmount")
        //    {
        //        if (AccDocumentItem?.CurrencyId != null)
        //        {
        //            if (AccDocumentItem.Debit > 0)
        //            {
        //                AccDocumentItem.Debit = (AccDocumentItem.ExchangeRate ?? 0 * AccDocumentItem.CurrencyAmount) / AccDocumentItem.Currency.ExchangeUnit;
        //            }
        //            else
        //            {
        //                AccDocumentItem.Credit = (AccDocumentItem.ExchangeRate ?? 0 * AccDocumentItem.CurrencyAmount) / AccDocumentItem.Currency.ExchangeUnit;
        //            }
        //        }
        //    }
        //}

        //private IEnumerable<SL> _sLs;
        //public IEnumerable<SL> SLs
        //{
        //    get { return _sLs; }
        //    set { SetProperty(ref _sLs, value); }
        //}
        //private IEnumerable<Currency> _Currencies;
        //public IEnumerable<Currency> Currencies
        //{
        //    get { return _Currencies; }
        //    set { SetProperty(ref _Currencies, value); }
        //}
        //private IEnumerable<TypeDocument> _typeDocuments;
        //public IEnumerable<TypeDocument> TypeDocuments
        //{
        //    get { return _typeDocuments; }
        //    set { SetProperty(ref _typeDocuments, value); }
        //}
        //#endregion
        //#region Methods
        //public void Loaded()
        //{
        //    _uow = new SainaDbContext();
        //    //var ids = new List<int> { 1, 3, 4, 5, 6 };
        //    //TypeDocuments = _uow.TypeDocuments.Where(x => !ids.Contains(x.TypeDocumentId)).ToList();
        //    TypeDocuments = _uow.TypeDocuments.ToList();
        //    OnSLsDropDownOpened();
        //    OnCurrenciesDropDownOpened();
        //    AccDocumentHeaders = new QueryableCollectionView(_uow.AccDocumentHeaders.Include(x => x.AccDocumentItems).ToList());
        //}
        //public override void UnLoaded()
        //{
        //    _uow.Dispose();
        //}
        //public void AddAccDocumentHeader(AccDocumentHeader accDocumentHeader)
        //{
        //    _uow.AccDocumentHeaders.Add(accDocumentHeader);
        //}
        //public void Save()
        //{
        //    _uow.SaveChanges();
        //}
        //public int DeleteDocumentHeader(AccDocumentHeader accDocumentHeader)
        //{
        //    _uow.AccDocumentHeaders.Remove(accDocumentHeader);
        //    return _uow.SaveChanges();
        //}
        //private async void OnSLStandardDescriptionsDropDownOpened(AccDocumentItem accDocument)
        //{
        //    if (accDocument?.SL != null)
        //    {
        //        accDocument.SLStandardDescriptions = _uow.SLStandardDescriptions.Where(x => x.SLId == accDocument.SLId).Select(x => x.SLStandardDescriptionTitle).ToList();
        //    }
        //}
        //private void OnCurrenciesDropDownOpened()
        //{
        //    Currencies = _uow.Currencies.ToList();
        //}
        ////private ObservableCollection<AccDocumentHeader> _accDocumentHeaders;
        ////public ObservableCollection<AccDocumentHeader> AccDocumentHeaders
        ////{
        ////    get { return _accDocumentHeaders; }
        ////    set { SetProperty(ref _accDocumentHeaders, value); }
        ////}
        //public async void LoadAccDocumentHeaders()
        //{
        //    IsBusy = true;

        //    var dateDocument = _appContextService.CurrentFinancialYear;
        //    if (AccDocumentHeaders == null /*|| !AccDocumentHeaders.Any()*/)
        //    {
        //        _allAccDocumentHeaders = await _accDocumentHeadersService.GetAccSystemDocumentHeadersAsync(dateDocument, 6);
        //        AccDocumentHeaders = new QueryableCollectionView(_uow.AccDocumentHeaders.Include(x => x.AccDocumentItems).ToList());

        //        //  AccDocumentHeaders = new (_allAccDocumentHeaders);
        //    }
        //    _appContextService.PropertyChanged += async (sender, eventArgs) =>
        //    {
        //        var appContextService = sender as IAppContextService;
        //        if (eventArgs.PropertyName == "CurrentFinancialYear")
        //            dateDocument = _appContextService.CurrentFinancialYear;
        //        _allAccDocumentHeaders = await _accDocumentHeadersService.GetAccSystemDocumentHeadersAsync(dateDocument, 6);
        //        AccDocumentHeaders = new QueryableCollectionView(_uow.AccDocumentHeaders.Include(x => x.AccDocumentItems).ToList());

        //        // AccDocumentHeaders = new ObservableCollection<AccDocumentHeader>(_allAccDocumentHeaders);
        //    };
        //    IsBusy = false;
        //}
        //private void OnSLsDropDownOpened()
        //{
        //    //_allSLs = await _sLsService.GetSLsActiveAsync();
        //    //SLs = new ObservableCollection<SL>(_allSLs);
        //    //if (SLs?.Any() != true)
        //    SLs = _uow.SLs.Where(x => x.Status == true).ToList();
        //}
        //IDictionary<int, IEnumerable<DL>> DlsDictionary = new Dictionary<int, IEnumerable<DL>>();
        //private async void OnDLsDropDownOpened(string dlType)
        //{

        //    if (AccDocumentItem?.SLId != null)
        //    {
        //        AccDocumentItem.DLs1 = new List<DL>();
        //        AccDocumentItem.DLs2 = new List<DL>();
        //        if (!DlsDictionary.ContainsKey(AccDocumentItem.SLId))
        //        {
        //            var q = $@"SELECT dls.* FROM Info.SLs sls 
        //            JOIN Info.DLs dls ON(sls.{dlType} & dls.DLType) > 0 WHERE sls.SLId ={AccDocumentItem.SLId}";
        //            if (dlType == "DLType1")
        //            {
        //                AccDocumentItem.DLs1 = await _uow.Database.SqlQuery<DL>(q).ToListAsync();
        //                DlsDictionary.Add(AccDocumentItem.SLId, AccDocumentItem.DLs1);
        //            }
        //            else
        //            {
        //                AccDocumentItem.DLs2 = await _uow.Database.SqlQuery<DL>(q).ToListAsync();
        //                DlsDictionary.Add(AccDocumentItem.SLId, AccDocumentItem.DLs2);
        //            }

        //        }
        //        else
        //        {
        //            if (dlType == "DLType1")
        //                AccDocumentItem.DLs1 = DlsDictionary[AccDocumentItem.SLId];
        //            else
        //                AccDocumentItem.DLs2 = DlsDictionary[AccDocumentItem.SLId];

        //        }

        //        //if (dlType == "DLType1")
        //        //    AccDocumentItem.DLs1 = new ObservableCollection<DL>(_allDLs);
        //        //else
        //        //    AccDocumentItem.DLs2 = new ObservableCollection<DL>(_allDLs);
        //    }

        //}
        //#endregion
        #region Constructors
        public CurrencyExchangeRateDocHeaderListViewModel(IAccDocumentHeadersService accDocumentHeadersService, IAppContextService appContextService, IOpeningClosingsService openingClosingsService)
        {
            _accDocumentHeadersService = accDocumentHeadersService;
            _appContextService = appContextService;
            _openingClosingsService = openingClosingsService;

            EditAccDocumentHeaderCommand = new RelayCommand<AccDocumentHeader>(OnEditAccDocumentHeader);
            ReturnCommand = new RelayCommand(OnReturnd);
            DeleteCommand = new RelayCommand<AccDocumentHeader>(OnDeleteAccDocumentHeader);
        }


        #endregion
        #region Fields
        private IOpeningClosingsService _openingClosingsService;

        private IAccDocumentHeadersService _accDocumentHeadersService;
        private List<AccDocumentHeader> _allAccDocumentHeaders;
        private IAppContextService _appContextService;

        #endregion
        #region Commands
        // public ICommand AddAccDocumentItemCommand { get; private set; }
        public ICommand ReturnCommand { get; private set; }

        public ICommand AddAccDocumentHeaderCommand { get; private set; }
        public ICommand EditAccDocumentHeaderCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        #endregion
        #region Properties
        private ObservableCollection<AccDocumentHeader> _accDocumentHeaders;
        public ObservableCollection<AccDocumentHeader> AccDocumentHeaders
        {
            get { return _accDocumentHeaders; }
            set { SetProperty(ref _accDocumentHeaders, value); }
        }
        private ObservableCollection<AccDocumentItem> _accDocumentItems;
        public ObservableCollection<AccDocumentItem> AccDocumentItems
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


        //  public event Action<AccDocumentHeader> AddAccDocumentHeaderRequested;
        public event Action<AccDocumentHeader> EditAccDocumentHeaderRequested;
        // public event Action<AccDocumentHeader> AddAccDocumentItemRequested;
        public event Action ReturnRequested;

        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<string> Error;

        public event Action<Exception> Failed;
        #endregion
        #region Methode
        private void OnReturnd()
        {
            //   typeDocumentId = TypeDocumentId;
            ReturnRequested();
        }
        public async void LoadAccDocumentHeaders()
        {
            IsBusy = true;

            var dateDocument = _appContextService.CurrentFinancialYear;
            if (AccDocumentHeaders == null || !AccDocumentHeaders.Any())
            {
                _allAccDocumentHeaders = await _accDocumentHeadersService.GetAccSystemDocumentHeadersAsync(dateDocument, 6);
                AccDocumentHeaders = new ObservableCollection<AccDocumentHeader>(_allAccDocumentHeaders);
            }
            _appContextService.PropertyChanged += async (sender, eventArgs) =>
            {
                var appContextService = sender as IAppContextService;
                if (eventArgs.PropertyName == "CurrentFinancialYear")
                    dateDocument = _appContextService.CurrentFinancialYear;
                _allAccDocumentHeaders = await _accDocumentHeadersService.GetAccSystemDocumentHeadersAsync(dateDocument, 6);
                AccDocumentHeaders = new ObservableCollection<AccDocumentHeader>(_allAccDocumentHeaders);
            };
            IsBusy = false;
        }

        private void OnEditAccDocumentHeader(AccDocumentHeader accDocumentHeader)
        {
            EditAccDocumentHeaderRequested(accDocumentHeader);
        }
        private async void OnDeleteAccDocumentHeader(AccDocumentHeader accDocumentHeader)
        {
            if (Deleting())
            {
                try
                {
                    await _accDocumentHeadersService.DeleteAccDocumentHeaderAsync(accDocumentHeader.AccDocumentHeaderId);
                    AccDocumentHeaders.Remove(accDocumentHeader);
                    Deleted();
                }
                catch (Exception ex)
                {
                    Failed(ex);
                }

            }

        }

        #endregion
    }
}
