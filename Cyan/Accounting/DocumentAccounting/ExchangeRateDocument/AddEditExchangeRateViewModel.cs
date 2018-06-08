using AutoMapper;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.ExchangeRateDocument
{
    /// <summary>
    /// افزودن و ویرایش نرخ ارز
    /// </summary>
    class AddEditExchangeRateViewModel:BindableBase
    {
        #region Constructors
        public AddEditExchangeRateViewModel(IExchangeRatesService exchangeRatesService, ICurrenciesService currenciesService)
        {
            _exchangeRatesService = exchangeRatesService;
            _currenciesService = currenciesService;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            CurrenciesDropDownOpenedCommand = new RelayCommand(OnCurrenciesDropDownOpened, () => Currencies != null && Currencies.Any());
        }
        #endregion
        #region Fields
        private IExchangeRatesService _exchangeRatesService;
        private List<Currency> _allCurrencies;
        private ICurrenciesService _currenciesService;
        private ExchangeRate _editingExchangeRate = null;
        #endregion
        #region Commands
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand CurrenciesDropDownOpenedCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        #endregion
        #region Properties
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }
        private EditableExchangeRate _ExchangeRate;
        public EditableExchangeRate ExchangeRate
        {
            get { return _ExchangeRate; }
            set { SetProperty(ref _ExchangeRate, value); }
        }
        private ObservableCollection<Currency> _currencies;
        public ObservableCollection<Currency> Currencies
        {
            get { return _currencies; }
            set { SetProperty(ref _currencies, value); }

        }
        public event Action<Exception> Failed;

        public event Action Done;
        #endregion
        #region Methode
        private async void OnCurrenciesDropDownOpened()
        {
            _allCurrencies = await _currenciesService.GetCurrenciesAsync();
            Currencies = new ObservableCollection<Currency>(_allCurrencies);
        }
        public async void LoadCurrencies()
        {
            if (Currencies == null)
            {
                _allCurrencies = await _currenciesService.GetCurrenciesAsync();
                Currencies = new ObservableCollection<Currency>(_allCurrencies);
            }
        }
        public override void UnLoaded()
        {
            Currencies = null;
          
        }
        public void SetExchangeRate(ExchangeRate exchangeRate)
        {
            ExchangeRate = Mapper.Map<ExchangeRate, EditableExchangeRate>(exchangeRate);
            ExchangeRate.ErrorsChanged += RaiseCanExecuteChanged;
        }
        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }
        private void OnCancel()
        {
            Done?.Invoke();
        }
        private async void OnSave()
        {
            var editingExchangeRate = Mapper.Map<EditableExchangeRate, ExchangeRate>(ExchangeRate);
            try
            {
                if (EditMode)
                await _exchangeRatesService.UpdateExchangeRateAsync(editingExchangeRate);
            else
                await _exchangeRatesService.AddExchangeRateAsync(editingExchangeRate);
            Done?.Invoke();
        }
            catch (Exception ex)
            {
                Failed(ex);
    }
            finally
            {
                ExchangeRate = null;
            }
        }
      
        private bool CanSave()
        {
            return !ExchangeRate.HasErrors;
        }
       
        #endregion

    }
}
