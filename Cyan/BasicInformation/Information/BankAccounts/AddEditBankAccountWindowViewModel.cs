using AutoMapper;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation.Information;
using Saina.Data.Context;
using Saina.WPF.BasicInformation.Information.Related;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Information.BankAccounts
{
    class AddEditBankAccountWindowViewModel : BindableBase
    {
        #region Constructors

        public AddEditBankAccountWindowViewModel(IBankAccountsService bankAccountsService, RelatedPersonListViewModel relatedPersonListViewModel, IBanksService banksService, ICurrenciesService currenciesService, IAccountTypesService accountTypesService)
        {
            _currenciesService = currenciesService;
            _accountTypesService = accountTypesService;
            _bankAccountsService = bankAccountsService;
            _banksService = banksService;
            _accountTypesService = accountTypesService;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            BanksDropDownOpenedCommand = new RelayCommand(OnBanksDropDownOpened, () => Banks != null && Banks.Any());
            CurrenciesDropDownOpenedCommand = new RelayCommand(OnCurrenciesDropDownOpened, () => Currencies != null && Currencies.Any());
            AccountTypesDropDownOpenedCommand = new RelayCommand(OnAccountTypesDropDownOpened, () => AccountTypes != null && Currencies.Any());
            
            RelatedPersonListViewModel = relatedPersonListViewModel;
            BankAccount = new EditableBankAccount();
        }

        
        #endregion
        #region Fields
        private ICurrenciesService _currenciesService;
        private List<Currency> _allCurrencies;
        private List<AccountType> _allAccountTypes;
        private SainaDbContext _uow;

        private IBankAccountsService _bankAccountsService;
        private IAccountTypesService _accountTypesService;
        private List<Bank> _allBanks;
        private IBanksService _banksService;
      //  private BankAccount _editingBankAccount = null;
        #endregion
        #region Commands
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand BanksDropDownOpenedCommand { get; private set; }
        public RelayCommand AccountTypesDropDownOpenedCommand { get; private set; }
        
        public RelayCommand CurrenciesDropDownOpenedCommand { get; private set; }

        #endregion
        #region Properties
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }
        private RelatedPersonListViewModel _relatedPersonListViewModel;

        public RelatedPersonListViewModel RelatedPersonListViewModel
        {
            get { return _relatedPersonListViewModel; }
            set { SetProperty(ref _relatedPersonListViewModel, value); }
        }
        private EditableBankAccount _BankAccount;
        public EditableBankAccount BankAccount
        {
            get { return _BankAccount; }
            set { SetProperty(ref _BankAccount, value); }
        }

        private IEnumerable<Bank> _banks;
        public IEnumerable<Bank> Banks
        {
            get { return _banks; }
            set { SetProperty(ref _banks, value); }
        }
        private IEnumerable<AccountType> _accountTypes;

        public IEnumerable<AccountType> AccountTypes
        {
            get { return _accountTypes; }
            set { SetProperty(ref _accountTypes, value); }
        }

        private IEnumerable<Currency> _currencies;
        public IEnumerable<Currency> Currencies
        {
            get { return _currencies; }
            set { SetProperty(ref _currencies, value); }
        }
        private DL _DL;
        public DL DL
        {
            get { return _DL; }
            set { SetProperty(ref _DL, value); }
        }

        public event Action<Exception> Failed;
        public event Action Done;
        #endregion
        #region Methode
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
        public  void LoadBanks()
        {
            _uow = new SainaDbContext();
           
            Banks = _uow.Banks.ToList();
            Currencies = _uow.Currencies.ToList();
            AccountTypes = _uow.AccountTypes.ToList();
            OnBanksDropDownOpened();
            OnCurrenciesDropDownOpened();
            OnAccountTypesDropDownOpened();
        }
        public override void UnLoaded()
        {
            Banks = null;
            Currencies = null;
            AccountTypes = null;
        }
        public void SetBankAccount(BankAccount bankAccount)
        {
            if (EditMode == false)
                RelatedPersonListViewModel.RelatedPeople = new ObservableCollection<RelatedPerson>();
            BankAccount = Mapper.Map<BankAccount, EditableBankAccount>(bankAccount);
            BankAccount.ErrorsChanged += RaiseCanExecuteChanged;
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
            var editingBankAccount = Mapper.Map<EditableBankAccount, BankAccount>(BankAccount);
          //  editingBankAccount.RelatedPeople = RelatedPersonListViewModel.RelatedPeople;

            try
            {
                if (EditMode)
                    await _bankAccountsService.UpdateBankAccountAsync(editingBankAccount);
                else
                    await _bankAccountsService.AddBankAccountAsync(editingBankAccount);
                Done?.Invoke();
            }
            catch (Exception ex)
            {
                Failed(ex);
            }
            finally
            {
                BankAccount = null;
            }
        }

        private bool CanSave()
        {
            //return !BankAccount?.HasErrors == true;
            return  true;
        }

        internal void AddBankAccount(BankAccount bankAccount)
        {
            _uow.BankAccounts.Add(bankAccount);
            _uow.SaveChanges();
        }

        #endregion
    }
}
