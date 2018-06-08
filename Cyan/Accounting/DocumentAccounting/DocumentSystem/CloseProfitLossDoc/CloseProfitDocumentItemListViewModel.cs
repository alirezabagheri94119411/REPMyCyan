using AutoMapper;
using Saina.ApplicationCore.DTOs.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.WPF.Accounting.DocumentAccounting.DocumentHeader;
using Saina.WPF.Accounting.DocumentAccounting.ItemDocument;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentSystem.CloseProfitLossDoc
{
    class CloseProfitDocumentItemListViewModel:BindableBase
    {
        #region Constructors
        public CloseProfitDocumentItemListViewModel(IAccDocumentItemsService accDocumentItemsService, ISLsService sLsService, IExchangeRatesService exchangeRatesService, ICurrenciesService currenciesService, ISLStandardDescriptionsService sLStandardDescriptionsService)
        {
            _accDocumentItemsService = accDocumentItemsService;
            _sLsService = sLsService;
            _exchangeRatesService = exchangeRatesService;
            _currenciesService = currenciesService;
            _sLStandardDescriptionsService = sLStandardDescriptionsService;
            //EditAccDocumentItemCommand = new RelayCommand<AccDocumentItem>(OnEditAccDocumentItem);
            DeleteCommand = new RelayCommand<EditableAccDocumentItem>(OnDeleteAccDocumentItem);
            // CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            CancelCommand = new RelayCommand(OnCancel);
            SLsDropDownOpenedCommand = new RelayCommand(OnSLsDropDownOpened);
            DLsDropDownOpenedCommand = new RelayCommand<string>(OnDLsDropDownOpened);
            SLStandardDescriptionsDropDownOpenedCommand = new RelayCommand<string>(OnSLStandardDescriptionsDropDownOpened);
            CurrenciesDropDownOpenedCommand = new RelayCommand<string>(OnCurrenciesDropDownOpened);
            ExchangeRatesDropDownOpenedCommand = new RelayCommand<string>(OnExchangeRatesDropDownOpened);
            CanAdd = true;
            AccDocumentHeader = new EditableAccDocumentHeader();
        }

        private void OnCancel()
        {
            Done();
        }
        #endregion
        #region Fields
        private IAccDocumentItemsService _accDocumentItemsService;
        private ISLsService _sLsService;
        IExchangeRatesService _exchangeRatesService;
        ICurrenciesService _currenciesService;
        ISLStandardDescriptionsService _sLStandardDescriptionsService;
        private List<DL> _allDLs;
        private List<SL> _allSLs;
        private List<EditableAccDocumentItem> _allAccDocumentItems;

        private List<SLStandardDescription> _allSLStandardDescriptions;
        private List<CurrencyExchangesDTO> _allCurrencies;
        private List<ExchangeRate> _allExchangeRates;
        public event Action Done;

        //    private List<AccDocumentItem> _allAccDocumentItems;
        #endregion
        #region Commands
        public ICommand AddAccDocumentItemCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public ICommand SLsDropDownOpenedCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand DLsDropDownOpenedCommand { get; private set; }
        public ICommand SLStandardDescriptionsDropDownOpenedCommand { get; private set; }
        public ICommand CurrenciesDropDownOpenedCommand { get; private set; }
        public ICommand ExchangeRatesDropDownOpenedCommand { get; private set; }
        // public ICommand EditAccDocumentItemCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public event NotifyCollectionChangedEventHandler AccDocumentItemsCollectionChanged;
        public event PropertyChangedEventHandler EditableAccDocumentItemChanged;
        #endregion
        #region Properties
        private ObservableCollection<EditableAccDocumentItem> _accDocumentItems;
        public ObservableCollection<EditableAccDocumentItem> AccDocumentItems
        {
            get { return _accDocumentItems; }
            set { SetProperty(ref _accDocumentItems, value); }
        }
        private EditableAccDocumentItem _AccDocumentItem;
        public EditableAccDocumentItem AccDocumentItem
        {
            get { return _AccDocumentItem; }
            set { SetProperty(ref _AccDocumentItem, value); }
        }
        private ObservableCollection<SL> _sLs;
        public ObservableCollection<SL> SLs
        {
            get { return _sLs; }
            set { SetProperty(ref _sLs, value); }
        }
        //private ObservableCollection<DL> _dLs;
        //public ObservableCollection<DL> DLs
        //{
        //    get { return _dLs; }
        //    set { SetProperty(ref _dLs, value); }
        //}
        private ObservableCollection<ExchangeRate> _exchangeRates;

        public ObservableCollection<ExchangeRate> ExchangeRates
        {
            get { return _exchangeRates; }
            set { SetProperty(ref _exchangeRates, value); }
        }
        private ObservableCollection<CurrencyExchangesDTO> _currencies;

        public ObservableCollection<CurrencyExchangesDTO> Currencies
        {
            get { return _currencies; }
            set { SetProperty(ref _currencies, value); }
        }
        ////private ObservableCollection<SLStandardDescription> _sLStandardDescriptions;

        ////public ObservableCollection<SLStandardDescription> SLStandardDescriptions
        ////{
        ////    get { return _sLStandardDescriptions; }
        ////    set { SetProperty(ref _sLStandardDescriptions, value); }

        ////}
        ////private SLStandardDescription _selectedSLStandardDescription;

        ////public SLStandardDescription SelectedSLStandardDescription
        ////{
        ////    get { return _selectedSLStandardDescription; }
        ////    set { SetProperty(ref _selectedSLStandardDescription, value); }
        ////}


        private int _accRow;

        public int AccRow
        {
            get { return _accRow; }
            set { SetProperty(ref _accRow, value); }
        }
        private bool _resultColumn;
        public bool ResultColumn
        {
            get { return _resultColumn; }
            set { SetProperty(ref _resultColumn, value); }
        }

        // public event Action<AccDocumentItem> EditAccDocumentItemRequested;
        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<Exception> Failed;
        public EditableAccDocumentHeader AccDocumentHeader { get; set; }
        private bool _canAdd;
        public bool CanAdd
        {
            get { return _canAdd; }
            set { SetProperty(ref _canAdd, value); }
        }

        #endregion
        #region Methode
        private async void OnSLStandardDescriptionsDropDownOpened(string sLStandardDescription)
        {

            //if (AccDocumentItem.SL != null)
            //{
            //    _allSLStandardDescriptions = await _sLsService.GetSLStandardDescriptionsAsync(AccDocumentItem.SL.SLId);
            //    // _allSLStandardDescriptions = AccDocumentItem.SLCode.SLStandardDescriptions.ToList();
            //}
        }
        private async void OnCurrenciesDropDownOpened(string currencies)
        {
            Currencies = new ObservableCollection<CurrencyExchangesDTO>(await _currenciesService.GetCurrencies2Async());
        }
        private async void OnExchangeRatesDropDownOpened(string exchangeRate)
        {

            if (AccDocumentItem.SL != null)
            {
                _allExchangeRates = await _sLsService.GetExchangeRatesAsync(AccDocumentItem.SL.SLId, exchangeRate);
                ExchangeRates = new ObservableCollection<ExchangeRate>(_allExchangeRates);
            }
        }
        private async void OnSLsDropDownOpened()
        {
            _allSLs = await _sLsService.GetSLsActiveAsync();
            SLs = new ObservableCollection<SL>(_allSLs);
        }
        private async void OnDLsDropDownOpened(string dlType)
        {

            if (AccDocumentItem.SL != null)
            {
                _allDLs = await _sLsService.GetDLsAsync(AccDocumentItem.SL.SLId, dlType);
                if (dlType == "DLType1")
                    AccDocumentItem.DLs1 = new ObservableCollection<DL>(_allDLs);
                else
                    AccDocumentItem.DLs2 = new ObservableCollection<DL>(_allDLs);
            }
        }
        public async void LoadAccDocumentItems()
        {
           // AccRow = _accDocumentItemsService.GetLastRow();
           // string AccRowCode = AccRow.ToString();
            //    AccDocumentItem.AccRow = int.Parse($"{AccRowCode}");
            //if (AccDocumentItems == null)
            //{
            var temp1 = await _accDocumentItemsService.GetAccDocumentItemsAsync(AccDocumentHeader.AccDocumentHeaderId);
            _allAccDocumentItems = Mapper.Map<List<AccDocumentItem>, List<EditableAccDocumentItem>>(temp1);

            AccDocumentItems = new ObservableCollection<EditableAccDocumentItem>(_allAccDocumentItems ?? new List<EditableAccDocumentItem>());
            if (AccDocumentHeader != null)
            {
                AccDocumentHeader.SumDebit = _allAccDocumentItems.Sum(x => x.Debit);
                AccDocumentHeader.SumCredit = _allAccDocumentItems.Sum(x => x.Credit);
                AccDocumentHeader.Difference = Math.Abs(AccDocumentHeader.SumDebit - AccDocumentHeader.SumCredit);
            }
            foreach (var x in AccDocumentItems)
            {
                x.PropertyChanging += (sender, eventArg) =>
                {
                    var editableAccDocumentItem = sender as EditableAccDocumentItem;

                    if (eventArg.PropertyName == "Debit")
                    {
                        AccDocumentHeader.SumDebit -= editableAccDocumentItem.Debit;
                    }
                    if (eventArg.PropertyName == "Credit")
                    {
                        AccDocumentHeader.SumCredit -= editableAccDocumentItem.Credit;
                    }
                };

                x.PropertyChanged += (sender, eventArg) =>
                {
                    var editableAccDocumentItem = sender as EditableAccDocumentItem;
                    EditableAccDocumentItemChanged?.Invoke(sender, eventArg);
                    if (eventArg.PropertyName == "Debit")
                    {
                        AccDocumentHeader.SumDebit += editableAccDocumentItem.Debit;
                        var temp = Mapper.Map<EditableAccDocumentItem, AccDocumentItem>(editableAccDocumentItem);
                        _accDocumentItemsService.AddAccDocumentItemAsync(temp);
                    }
                    if (eventArg.PropertyName == "Credit")
                    {
                        AccDocumentHeader.SumCredit += editableAccDocumentItem.Credit;
                        var temp = Mapper.Map<EditableAccDocumentItem, AccDocumentItem>(editableAccDocumentItem);
                        _accDocumentItemsService.AddAccDocumentItemAsync(temp);
                    }
                };
            }
            AccDocumentItems.CollectionChanged += (s, ea) =>
            {
                AccDocumentItemsCollectionChanged?.Invoke(s, ea);
                if (ea.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                {
                    var oldItems = ea.OldItems.Cast<EditableAccDocumentItem>();
                    //var ids = oldItems.Select(y => y.AccDocumentHeaderId).ToList();
                    //var delete = _accDocumentItemsService.DeleteItemsAccDocumentItemAsync(ids);
                    AccDocumentHeader.SumDebit -= oldItems.Sum(x => x.Debit);
                    AccDocumentHeader.SumCredit -= oldItems.Sum(x => x.Credit);
                    return;
                }
                var c = ea.NewItems.Cast<EditableAccDocumentItem>();
                c.First().AccRow = AccDocumentItems.Max(x => x.AccRow) + 1;
                foreach (var x in c)
                {
                    x.PropertyChanging += (sender, eventArg) =>
                    {
                        var editableAccDocumentItem = sender as EditableAccDocumentItem;

                        if (eventArg.PropertyName == "Debit")
                        {
                            AccDocumentHeader.SumDebit -= editableAccDocumentItem.Debit;
                        }
                        if (eventArg.PropertyName == "Credit")
                        {
                            AccDocumentHeader.SumCredit -= editableAccDocumentItem.Credit;
                        }
                    };

                    x.PropertyChanged += (sender, eventArg) =>
                    {
                        var editableAccDocumentItem = sender as EditableAccDocumentItem;
                        EditableAccDocumentItemChanged?.Invoke(sender, eventArg);
                        if (eventArg.PropertyName == "Debit")
                        {
                            if (ea.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                            {
                                AccDocumentHeader.SumDebit += editableAccDocumentItem.Debit;
                                var temp = Mapper.Map<EditableAccDocumentItem, AccDocumentItem>(editableAccDocumentItem);
                                _accDocumentItemsService.AddAccDocumentItemAsync(temp);
                            }

                        }
                        if (eventArg.PropertyName == "Credit")
                        {
                            if (ea.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                            {
                                AccDocumentHeader.SumCredit += editableAccDocumentItem.Credit;
                                var temp = Mapper.Map<EditableAccDocumentItem, AccDocumentItem>(editableAccDocumentItem);
                                _accDocumentItemsService.AddAccDocumentItemAsync(temp);
                            }

                        }
                        if (eventArg.PropertyName == "SelectedCurrency")
                        {
                            if (ea.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                            {
                                if (AccDocumentItem?.SelectedCurrency != null)
                                    AccDocumentItem.ExchangeRate = AccDocumentItem.SelectedCurrency.ParityRate;
                            }
                        }
                    };
                }
            };
            //    AccDocumentItems = new ObservableCollection<AccDocumentItem>();
            //}
            if (SLs == null)
            {
                _allSLs = await _sLsService.GetSLsActiveAsync();
                SLs = new ObservableCollection<SL>(_allSLs);
            }
        }



        //private void OnEditAccDocumentItem(AccDocumentItem accDocumentItem)
        //{
        //    accDocumentItem.SLId = SLId;
        //    EditAccDocumentItemRequested(accDocumentItem);
        //}

        private async void OnDeleteAccDocumentItem(EditableAccDocumentItem accDocumentItem)
        {
            if (Deleting?.Invoke() == true)
            {
                try
                {
                    await _accDocumentItemsService.DeleteAccDocumentItemAsync(accDocumentItem.AccDocumentItemId);
                    AccDocumentItems.Remove(accDocumentItem);
                    Deleted();
                }
                catch (Exception ex)
                {
                    Failed(ex);
                }

            }

        }
        public void SetAccDocumentItem(AccDocumentItem accDocumentItem)
        {
            AccDocumentItem = Mapper.Map<AccDocumentItem, EditableAccDocumentItem>(accDocumentItem);
            //  AccDocumentItem.ErrorsChanged += RaiseCanExecuteChanged;
        }
        private  void OnSave()
        {
            var editingAccDocumentItem = Mapper.Map<EditableAccDocumentItem, AccDocumentItem>(AccDocumentItem);
            //   editingAccDocumentItem.TrackingDate = DateTime.Now;
             _accDocumentItemsService.AddAccDocumentItemAsync(editingAccDocumentItem);

        }
        internal async Task<bool> IsReadOnly(string columnName)
        {
            //    bool result = false;
            switch (columnName)
            {
                case "Currency":
                case "ExchangeRate":
                case "CurrencyAmount":
                    ResultColumn = await _accDocumentItemsService.HasCurrency(AccDocumentItem.SL.SLId);
                    break;
                case "TrackingNumber":
                case "TrackingDate":
                    ResultColumn = await _accDocumentItemsService.HasTracking(AccDocumentItem.SL.SLId);
                    break;
                default:
                    break;
            }
            return ResultColumn;
        }
        private bool CanSave()
        {
            return true;//!AccDocumentItem.HasErrors;
        }
        #endregion
    }
}
