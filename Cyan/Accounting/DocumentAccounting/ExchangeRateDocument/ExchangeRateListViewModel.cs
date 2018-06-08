using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.WPF.Behaviors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace Saina.WPF.Accounting.DocumentAccounting.ExchangeRateDocument
{
    /// <summary>
    /// لیست نرخ ارز
    /// </summary>
    class ExchangeRateListViewModel : BindableBase
    {
        #region Constructors
        public ExchangeRateListViewModel(IExchangeRatesService exchangeRatesService, ICurrenciesService currenciesService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _currenciesService = currenciesService;
            _exchangeRatesService = exchangeRatesService;
            AddExchangeRateCommand = new RelayCommand(OnAddExchangeRate);
            EditExchangeRateCommand = new RelayCommand<ExchangeRate>(OnEditExchangeRate);
            DeleteCommand = new RelayCommand<ExchangeRate>(OnDeleteExchangeRate);
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();
        }
        #endregion
        #region Fields
        private IExchangeRatesService _exchangeRatesService;
        private List<ExchangeRate> _allExchangeRates;
        private ICurrenciesService _currenciesService;
        private List<Currency> _allCurrencies;
        public CompanyInformationModel CompanyInformationModel { get; set; }
        private ICompanyInformationsService _companyInformationsService;
        #endregion
        #region Commands
        public ICommand AddExchangeRateCommand { get; private set; }
        public ICommand EditExchangeRateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        private AccessUtility _accessUtility;
        #endregion
        #region Properties
        private ObservableCollection<ExchangeRate> _exchangeRates;
        public ObservableCollection<ExchangeRate> ExchangeRates
        {
            get { return _exchangeRates; }
            set { SetProperty(ref _exchangeRates, value); }
        }
        private ObservableCollection<Currency> _currencies;

        public ObservableCollection<Currency> Currencies
        {
            get { return _currencies; }
            set { SetProperty(ref _currencies, value); }
        }

        public event Action<ExchangeRate> AddExchangeRateRequested ;
        public event Action<ExchangeRate> EditExchangeRateRequested ;
        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<Exception> Failed;
        public event Action<string> Error;
        #endregion
        #region Methode
        public async void LoadExchangeRates()
        {
            _allExchangeRates = await _exchangeRatesService.GetExchangeRatesAsync();
            ExchangeRates = new ObservableCollection<ExchangeRate>(_allExchangeRates);
        }
        private async void OnAddExchangeRate()
        {
            if (_accessUtility.HasAccess(31))
            {
                _allCurrencies = await _currenciesService.GetCurrenciesAsync();

                Currencies = new ObservableCollection<Currency>(_allCurrencies);

                if (Currencies?.Any() != true)
                {
                    Error(".ارز ثبت نشده است");

                }
                else
                {
                    AddExchangeRateRequested(new ExchangeRate());
                }
            }
        }
        private void OnEditExchangeRate(ExchangeRate exchangeRate)
        {
            if (_accessUtility.HasAccess(32))
            {
                EditExchangeRateRequested(exchangeRate);
            }
        }

        private async void OnDeleteExchangeRate(ExchangeRate exchangeRate)
        {
            if (_accessUtility.HasAccess(33))
            {
                if (Deleting())
                {
                    try
                    {
                        await _exchangeRatesService.DeleteExchangeRateAsync(exchangeRate.ExchangeRateId);
                        ExchangeRates.Remove(exchangeRate);
                        Deleted();
                    }
                    catch (Exception ex)
                    {
                        Failed(ex);
                    }

                }
            }

        }
        #endregion
    }
}
