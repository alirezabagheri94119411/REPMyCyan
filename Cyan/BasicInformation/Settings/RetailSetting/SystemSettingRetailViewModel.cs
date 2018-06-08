using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.Entities.Commerce;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Information;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using Saina.ApplicationCore.IServices.Commerce;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saina.WPF.BasicInformation.Settings.RetailSetting
{
    /// <summary>
    /// تنظیمات سیستم خرده فروشی
    /// </summary>
    class SystemSettingRetailViewModel : BindableBase
    {
        #region Fields
        private ISystemSettingRetailsService _systemSettingRetailsService;
        private IAppContextService _appContextService;
        private List<BankAccountSetting> _allBankAccounts;
        private IBankAccountsService _bankAccountsService;
        private List<StockSetting> _allStocks;
        private IStocksService _stocksService;
        
        #endregion
        #region Constructors

        public SystemSettingRetailViewModel(IAppContextService appContextService, ISystemSettingRetailsService systemSettingRetailsService, IBankAccountsService bankAccountsService, IStocksService stocksService)
        {
            _bankAccountsService = bankAccountsService;
            _stocksService = stocksService;
             _appContextService = appContextService;
            _systemSettingRetailsService = systemSettingRetailsService;
            SystemSettingRetailModel = _systemSettingRetailsService.GetSystemSettingRetailModel();

            SaveCommand = new RelayCommand(onSave);
        }

        #endregion
        #region Commands
        public ICommand SaveCommand { get; set; }
        #endregion
        #region Properties
        public SystemSettingRetailModel SystemSettingRetailModel { get; set; }
       

        private ObservableCollection<BankAccountSetting> _bankAccounts;
        public ObservableCollection<BankAccountSetting> BankAccounts
        {
            get { return _bankAccounts; }
            set { SetProperty(ref _bankAccounts, value); }
        }
        private ObservableCollection<StockSetting> _stocks;
        public ObservableCollection<StockSetting> Stocks
        {
            get { return _stocks; }
            set { SetProperty(ref _stocks, value); }
        }
        private ObservableCollection<Invoice> _invoice;
        public ObservableCollection<Invoice> Invoice
        {
            get { return _invoice; }
            set { SetProperty(ref _invoice, value); }
        }
        private ObservableCollection<DefaultMouseLocation> _defaultMouseLocation;
        public ObservableCollection<DefaultMouseLocation> DefaultMouseLocation
        {
            get { return _defaultMouseLocation; }
            set { SetProperty(ref _defaultMouseLocation, value); }
        }

        public event Action Done;
        #endregion
        #region Methods
        public async void LoadBankAccounts()
        {
            if (BankAccounts==null)
            {
                _allBankAccounts = (await _bankAccountsService.GetBankAccountsAsync()).Select(x => new BankAccountSetting { BankAccountId = x.BankAccountId.ToString(), PoseId = x.PoseId.ToString() }).ToList();
            BankAccounts = new ObservableCollection<BankAccountSetting>(_allBankAccounts);
            }
            if (Stocks == null)
            {
                _allStocks = (await _stocksService.GetStocksAsync()).Select(x => new StockSetting { StockId = x.StockId.ToString(), StockTitle1 = x.StockTitle1 }).ToList();
                Stocks = new ObservableCollection<StockSetting>(_allStocks);
            }
            if (Invoice == null)
            {
                Invoice = new ObservableCollection<Invoice>
            {
                new Invoice { InvoiceId = "1", InvoiceTitle = "A3" },
                new Invoice { InvoiceId = "2", InvoiceTitle = "A4" },
                new Invoice { InvoiceId = "3", InvoiceTitle = "A5" },
               
            };
            }
            if (DefaultMouseLocation == null)
            {
                DefaultMouseLocation = new ObservableCollection<DefaultMouseLocation>
            {
                new DefaultMouseLocation { DefaultMouseLocationId = "1", DefaultMouseLocationTitle = "کد کالا" },
                new DefaultMouseLocation { DefaultMouseLocationId = "2", DefaultMouseLocationTitle = "بارکد کالا" },
              

            };
            }

        }
        private void onSave()
        {
            _systemSettingRetailsService.SaveSystemSettingRetailModel(SystemSettingRetailModel);
        }


        #endregion
    }
}
