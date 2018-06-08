
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

namespace Saina.WPF.Accounting.DocumentAccounting.CurrencyDocument
{
    /// <summary>
    /// لیست تعریف ارز
    /// </summary>
   public class CurrencyListViewModel : BindableBase
    {
        #region Constructors
        public CurrencyListViewModel(ICurrenciesService currencysService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _currencysService = currencysService;
            AddCurrencyCommand = new RelayCommand(OnAddCurrency);
            EditCurrencyCommand = new RelayCommand<Currency>(OnEditCurrency);
            DeleteCommand = new RelayCommand<Currency>(OnDeleteCurrency);
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();
        }
        #endregion
        #region Fields
        private ICurrenciesService _currencysService;
        private List<Currency> _allCurrencies;
        public CompanyInformationModel CompanyInformationModel { get; set; }
        private ICompanyInformationsService _companyInformationsService;
        #endregion
        #region Commands
        public ICommand AddCurrencyCommand { get; private set; }
        public ICommand EditCurrencyCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        private AccessUtility _accessUtility;
        #endregion
        #region Properties
        private ObservableCollection<Currency> _currencies;

        public ObservableCollection<Currency> Currencies
        {
            get { return _currencies; }
            set { SetProperty(ref _currencies, value); }
        }
        public event Action<Currency> AddCurrencyRequested ;
        public event Action<Currency> EditCurrencyRequested ;
        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<string> Error;
        public event Action<Exception> Failed;
        #endregion
        #region Methode
        private void FilterCurrencies(string searchInput)
        {
            if (string.IsNullOrWhiteSpace(searchInput))
            {
                Currencies = new ObservableCollection<Currency>(_allCurrencies);
                return;
            }
            else
            {
                Currencies = new ObservableCollection<Currency>(_allCurrencies.Where(c => c.CurrencyTitle.ToLower().Contains(searchInput.ToLower())));
            }
        }
        public async void LoadCurrencies()
        {
            _allCurrencies = await _currencysService.GetCurrenciesAsync();
            Currencies = new ObservableCollection<Currency>(_allCurrencies);
        }
        private void OnAddCurrency()
        {
            if (_accessUtility.HasAccess(28))
            {
                AddCurrencyRequested(new Currency());
            }

        }
        private void OnEditCurrency(Currency currency)
        {
            if (_accessUtility.HasAccess(29))
            {
                EditCurrencyRequested(currency);
            }
        }

        private async void OnDeleteCurrency(Currency currency)
        {
            if (_accessUtility.HasAccess(30))
            {
                var x = await _currencysService.GetExchangeRateAsync(currency.CurrencyId);
                if (x == true)
                {
                    Error(".امکان حذف وجود ندارد");


                }
                else
                {
                    if (Deleting())
                    {
                        try
                        {
                            await _currencysService.DeleteCurrencyAsync(currency.CurrencyId);
                            Currencies.Remove(currency);
                            Deleted();
                        }
                        catch (Exception ex)
                        {
                            Failed(ex);
                        }

                    }
                }
            }
        }
        #endregion
    }
}
