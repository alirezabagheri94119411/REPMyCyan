
using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation.Information;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace Saina.WPF.BasicInformation.Information.BankAccounts
{

    /// <summary>
    /// لیست حساب های بانکی
    /// </summary>
    public class BankAccountListViewModel : BindableBase
    {
        #region Constructors
        public BankAccountListViewModel(IBankAccountsService bankAccountsService,
            IBanksService banksService, ICurrenciesService currenciesService, IAccountTypesService accountTypesService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _banksService = banksService;
            _currenciesService = currenciesService;
            _accountTypesService = accountTypesService;
            _bankAccountsService = bankAccountsService;
            AddBankAccountCommand = new RelayCommand(OnAddBankAccount);
            EditBankAccountCommand = new RelayCommand<BankAccount>(OnEditBankAccount);
            DeleteCommand = new RelayCommand<BankAccount>(OnDeleteBankAccount);
            AddRelatedPersonCommand = new RelayCommand<BankAccount>(OnAddRelatedPerson);
            BankAccount = new BankAccount();
        }
        #endregion
        #region Fields
        private IBankAccountsService _bankAccountsService;
        private List<BankAccount> _allBankAccounts;
        private IBanksService _banksService;
        private List<Bank> _allBanks;
        private ICurrenciesService _currenciesService;
        private List<Currency> _allCurrencies;
        private List<AccountType> _allAccountTypes;
        private SainaDbContext _uow;
        public CompanyInformationModel CompanyInformationModel { get; set; }
        private ICompanyInformationsService _companyInformationsService;
        private IAccountTypesService _accountTypesService;

        #endregion
        #region Commands
        public ICommand AddBankAccountCommand { get; private set; }
        public ICommand EditBankAccountCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddRelatedPersonCommand { get; private set; }

        #endregion
        #region Properties

        private ObservableCollection<RelatedPerson> _relatedPeople;
        public ObservableCollection<RelatedPerson> RelatedPeople
        {
            get { return _relatedPeople; }
            set { SetProperty(ref _relatedPeople, value); }
        }
        private ObservableCollection<BankAccount> _bankAccounts;
        public ObservableCollection<BankAccount> BankAccounts
        {
            get { return _bankAccounts; }
            set { SetProperty(ref _bankAccounts, value); }
        }
        private BankAccount _BankAccount;
        public BankAccount BankAccount
        {
            get { return _BankAccount; }
            set
            {
                SetProperty(ref _BankAccount, value);
                if (value == null)
                {
                    return;
                }
            }
        }
        private long _accountNumber;

        public long AccountNumber
        {
            get { return _accountNumber; }
            set { SetProperty(ref _accountNumber, value); }

        }


        private ObservableCollection<AccountType> _accountTypes;

        public ObservableCollection<AccountType> AccountTypes
        {
            get { return _accountTypes; }
            set { SetProperty(ref _accountTypes, value); }
        }

        private ObservableCollection<Currency> _currencies;
        public ObservableCollection<Currency> Currencies
        {
            get { return _currencies; }
            set { SetProperty(ref _currencies, value); }
        }
        private ObservableCollection<Bank> _banks;
        public ObservableCollection<Bank> Banks
        {
            get { return _banks; }
            set { SetProperty(ref _banks, value); }
        }
        //private Bank _Bank;

        //public Bank Bank
        //{
        //    get { return _Bank; }
        //    set { _Bank = value; }
        //}
        public event Action<BankAccount> AddBankAccountRequested;
        public event Action<BankAccount> EditBankAccountRequested;
        public event Action<BankAccount> AddRelatedPersonRequested;
        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<Exception> Failed;
        public event Action<string> Error;
        #endregion
        #region Methode
        private void OnAddRelatedPerson(BankAccount relatedPerson)
        {
            AddRelatedPersonRequested(relatedPerson);
        }
        private async void OnAddBankAccount()
        {
            _allBanks = await _banksService.GetBanksAsync();

            Banks = new ObservableCollection<Bank>(_allBanks);
            BankAccount = new BankAccount();
            if (Banks?.Any() != true)
            {
                Error(".بانک ثبت نشده است");

            }
            else
            {
                AddBankAccountRequested(new BankAccount());
            }
        }
        private void OnEditBankAccount(BankAccount bankAccount)
        {
            EditBankAccountRequested(bankAccount);
        }
        private async void OnDeleteBankAccount(BankAccount bankAccount)
        {
            if (Deleting())
            {
                try
                {
                    await _bankAccountsService.DeleteBankAccountAsync(bankAccount.BankAccountId);
                    BankAccounts.Remove(bankAccount);
                    Deleted();
                }
                catch (Exception ex)
                {
                    Failed(ex);
                }

            }

        }
        //public bool HasDL(int id)
        //{
        //    return _uow.DLs.Any(x => x.DLType == id);

        //}
        public void Add()
        {
            _uow.BankAccounts.Add(BankAccount);
        }
        public async void LoadBankAccounts()
        {
            _allBankAccounts = await _bankAccountsService.GetBankAccountsAsync();
            BankAccounts = new ObservableCollection<BankAccount>(_allBankAccounts);
        }
        private async void OnBanksDropDownOpened()
        {
            _allBanks = await _banksService.GetBanksAsync();
            Banks = new ObservableCollection<Bank>(_allBanks);
        }
        private async void OnCurrenciesDropDownOpened()
        {
            _allCurrencies = await _currenciesService.GetCurrenciesAsync();
            Currencies = new ObservableCollection<Currency>(_allCurrencies);
        }
        private async void OnAccountTypesDropDownOpened()
        {
            _allAccountTypes = await _accountTypesService.GetAccountTypesAsync();
            AccountTypes = new ObservableCollection<AccountType>(_allAccountTypes);
        }
        public void LoadBanks()
        {
            _uow = new SainaDbContext();
            if (Banks?.Any() != true)
            {
                Banks = new ObservableCollection<Bank>(_uow.Banks.ToList());
                Currencies = new ObservableCollection<Currency>(_uow.Currencies.ToList());
                AccountTypes = new ObservableCollection<AccountType>(_uow.AccountTypes.ToList());
                OnBanksDropDownOpened();
                OnCurrenciesDropDownOpened();
                OnAccountTypesDropDownOpened();

            }
        }
        public void Save()
        {

            if (BankAccount != null)
            {
                //stop Entity Framework from trying to save/insert child objects?
                var items = BankAccounts.ToList();
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Bank != null)
                        _uow.Entry(items[i].Bank).State = EntityState.Detached;
                    if (items[i].AccountType != null)
                        _uow.Entry(items[i].AccountType).State = EntityState.Detached;
                    if (items[i].CurrencyType != null)
                        _uow.Entry(items[i].CurrencyType).State = EntityState.Detached;
                }
            }



            var x = _uow.SaveChanges();
        }
        #endregion

    }
}
