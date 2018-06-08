using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using Saina.Data.Context;
using Saina.WPF.BasicInformation.Settings.AccountingSetting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Data;
using System.Collections;
using System.Data.Entity;
using Saina.ApplicationCore.IServices.BasicInformation.Information;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Telerik.Windows.Controls;
using Saina.ApplicationCore.IServices.BasicInformation;
using static Saina.WPF.BasicInformation.Accounts.DLAccount.EditBankAccountWindow;
using Saina.WPF.Accounting.DocumentAccounting.ReviewAcc;

namespace Saina.WPF.BasicInformation.Accounts.DLAccount
{
    /// <summary>
    /// لیست حساب تفصیل
    /// </summary>
    public class DLListViewModel : BindableBase
    {
        #region Constructors
        public DLListViewModel(ISystemAccountingSettingsService systemAccountingSettingsService
           , IBankAccountsService bankAccountsService, IBanksService banksService, ICurrenciesService currenciesService, IAccountTypesService accountTypesService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _systemAccountingSettingsService = systemAccountingSettingsService;

            _banksService = banksService;
            _currenciesService = currenciesService;
            _accountTypesService = accountTypesService;
            _bankAccountsService = bankAccountsService;
            BanksDropDownOpenedCommand = new RelayCommand(OnBanksDropDownOpened, () => Banks != null && Banks.Any());
            CurrenciesDropDownOpenedCommand = new RelayCommand(OnCurrenciesDropDownOpened, () => Currencies != null && Currencies.Any());
            AddDLCommand = new RelayCommand(OnAddDL);
            EditDLCommand = new RelayCommand<DL>(OnEditDL);
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();
        }
        public RelayCommand BanksDropDownOpenedCommand { get; private set; }

        public RelayCommand CurrenciesDropDownOpenedCommand { get; private set; }
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
        #endregion
        #region Fields
        ISystemAccountingSettingsService _systemAccountingSettingsService;
        #endregion
        #region Commands
        public ICommand AddDLCommand { get; private set; }
        public ICommand EditDLCommand { get; private set; }

        private AccessUtility _accessUtility;

        public ICommand DeleteCommand { get; private set; }
        #endregion
        #region Properties
        private ObservableCollection<DL> _dLs;
        public ObservableCollection<DL> DLs
        {
            get { return _dLs; }
            set { SetProperty(ref _dLs, value); }
        }
        private bool _TitleEnable;

        public bool TitleEnable
        {
            get { return _TitleEnable; }
            set { SetProperty(ref _TitleEnable, value); }
        }

        public event Action<DL> AddDLRequested;
        public event Action<DL> EditDLRequested;
        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<Exception> Failed;
        public event Action<string> Error;
        public event Action<DLTypeEnum, long?> DLTypeRequested;
        private DLTypeEnum _DLTypeEnum;
        public DLTypeEnum DLTypeEnum
        {
            get { return _DLTypeEnum; }
            set { SetProperty(ref _DLTypeEnum, value); }
        }


        internal void RaiseTestRequested(DLTypeEnum dLTypeEnum, long? dcode)
        {
            DLTypeRequested?.Invoke(dLTypeEnum, dcode);
        }
        #endregion
        #region Methode
        private void OnAddDL()
        {
            var SystemAccountingSettingModel = AutoMapper.Mapper.Map<SystemAccountingSettingModel, EditableSystemAccountingSettingModel>(_systemAccountingSettingsService.GetSystemAccountingSettingModel());
            var SystemAccountingSettingModelDLLength = int.Parse(SystemAccountingSettingModel.DLLength ?? "0");
            long lastDLCode = 0;
            if (_uow.DLs.Any())
            {
                lastDLCode = _uow.DLs.Select(x => x.DLCode).Max() + 1;
            }
            else
            {
                lastDLCode = 0;
            }
            var lastDLLenght = (lastDLCode.ToString()).Length;
            if (SystemAccountingSettingModelDLLength > 0 && lastDLLenght == SystemAccountingSettingModelDLLength)
            {
                AddDLRequested(new DL());
            }
            else if (SystemAccountingSettingModelDLLength > 0 && lastDLLenght != SystemAccountingSettingModelDLLength)
            {
                Error?.Invoke("کد حساب تفضیل به پایان رسیده است");
            }
            else
            {
                Error?.Invoke("تنظیمات حسابداری انجام نشده است");
            }

        }
        private void OnEditDL(DL dL)
        {
            EditDLRequested(dL);
        }
        //private async void OnDeleteDL(DL dL)
        //{
        //    if (Deleting())
        //    {
        //        try
        //        {
        //            await _dLsService.DeleteDLAsync(dL.DLId);
        //            DLs.Remove(dL);
        //            Deleted();
        //        }
        //        catch (Exception ex)
        //        {
        //            Failed(ex);
        //        }

        //    }

        //}
        #endregion
        private SainaDbContext _uow;
        private ICompanyInformationsService _companyInformationsService;

        public CompanyInformationModel CompanyInformationModel { get; set; }
        private ICollectionView _dLss;
        public ICollectionView DLss
        {
            get { return _dLss; }
            set { SetProperty(ref _dLss, value); }
        }
        private int _SystemAccountingSettingModelDLLength;

        public int SystemAccountingSettingModelDLLength1
        {
            get { return _SystemAccountingSettingModelDLLength; }
            set { SetProperty(ref _SystemAccountingSettingModelDLLength, value); }
        }
        private bool _enableTab;

        public bool EnableTab
        {
            get { return _enableTab; }
            set { SetProperty(ref _enableTab, value); }
        }

        public void Loaded()
        {
                     SystemAccountingSettingModel = AutoMapper.Mapper.Map<SystemAccountingSettingModel, EditableSystemAccountingSettingModel>(_systemAccountingSettingsService.GetSystemAccountingSettingModel());

            int.TryParse(SystemAccountingSettingModel.DLLength, out int SystemAccountingSettingModelDLLength);
            SystemAccountingSettingModelDLLength1 = SystemAccountingSettingModelDLLength;
            if (SystemAccountingSettingModelDLLength == 0)
            {
                DialogParameters parameters = new DialogParameters();
                parameters.OkButtonContent = "بستن";
                parameters.Header = "!اخطار";
                parameters.Content = "تنظیمات حسابداری انجام نشده است";
                RadWindow.Alert(parameters);
                 EnableTab = false;
            }
            else if (SystemAccountingSettingModelDLLength > 0)
            {
                
                EnableTab = true;
            }
            else
            {
               
                EnableTab = true;

            }
            _uow = new SainaDbContext();
            DLss = new QueryableCollectionView(_uow.DLs.ToList());
        }
        public void AddDL(DL dL)
        {
            _uow.DLs.Add(dL);
        }
        private EditableSystemAccountingSettingModel _SystemAccountingSettingModel;
        public EditableSystemAccountingSettingModel SystemAccountingSettingModel
        {
            get { return _SystemAccountingSettingModel; }
            set { SetProperty(ref _SystemAccountingSettingModel, value); }
        }
        public int GetLastDLCode(DLTypeEnum dLType)
        {
          
            int res = 0;
            var bankAccountString = 2;
            var peopleString = 3;
            var companyString = 4;
            var costString = 5;
            var projectString = 6;
            var otherString = 7;
            var bankAccount = bankAccountString.ToString().PadRight(SystemAccountingSettingModelDLLength1, '0');
            var people = peopleString.ToString().PadRight(SystemAccountingSettingModelDLLength1, '0');
            var company = companyString.ToString().PadRight(SystemAccountingSettingModelDLLength1, '0');
            var cost = costString.ToString().PadRight(SystemAccountingSettingModelDLLength1, '0');
            var project = projectString.ToString().PadRight(SystemAccountingSettingModelDLLength1, '0');
            var other = otherString.ToString().PadRight(SystemAccountingSettingModelDLLength1, '0');

            var bankAccountString1 = 1;
            var peopleString1 = 2;
            var companyString1 = 3;
            var costString1 = 4;
            var projectString1 = 5;
            var otherString1 = 6;
            var bankAccount1 = bankAccountString1.ToString().PadRight(SystemAccountingSettingModelDLLength1, '0');
            var people1 = peopleString1.ToString().PadRight(SystemAccountingSettingModelDLLength1, '0');
            var company1 = companyString1.ToString().PadRight(SystemAccountingSettingModelDLLength1, '0');
            var cost1 = costString1.ToString().PadRight(SystemAccountingSettingModelDLLength1, '0');
            var project1 = projectString1.ToString().PadRight(SystemAccountingSettingModelDLLength1, '0');
            var other1 = otherString1.ToString().PadRight(SystemAccountingSettingModelDLLength1, '0');
            if (_uow.DLs.Any(x => x.DLType == dLType))
            {
                res = _uow.DLs.Where(x => x.DLType == dLType).Max(x => x.DLCode) + 1;
                switch (dLType)
                {
                    case DLTypeEnum.BankAccount:
                        res = res >= Convert.ToInt32(bankAccount) ? 0 : res;
                        break;
                    case DLTypeEnum.People:
                        res = res >= Convert.ToInt32(people) ? 0 : res;
                        break;
                    case DLTypeEnum.Company:
                        res = res >= Convert.ToInt32(company) ? 0 : res;
                        break;
                    case DLTypeEnum.Cost:
                        res = res >= Convert.ToInt32(cost) ? 0 : res;
                        break;
                    case DLTypeEnum.Project:
                        res = res >= Convert.ToInt32(project) ? 0 : res;
                        break;
                    case DLTypeEnum.Others:
                        res = res >= Convert.ToInt32(other) ? 0 : res;
                        break;
                    default:
                        break;
                }

            }
            else
            {
                switch (dLType)
                {
                    case DLTypeEnum.BankAccount:
                        res = Convert.ToInt32(bankAccount1);
                        break;
                    case DLTypeEnum.People:
                        res = Convert.ToInt32(people1);
                        break;
                    case DLTypeEnum.Company:
                        res = Convert.ToInt32(company1);
                        break;
                    case DLTypeEnum.Cost:
                        res = Convert.ToInt32(cost1);
                        break;
                    case DLTypeEnum.Project:
                        res = Convert.ToInt32(project1);
                        break;
                    case DLTypeEnum.Others:
                        res = Convert.ToInt32(other1);
                        break;
                    default:
                        break;
                }
            }
            return res;
        }
        public bool HasItem(int id)
        {
            return _uow.AccDocumentItems.Any(x => x.DL1Id == id || x.DL2Id==id) ;

        }
        public int DeleteDL(DL dL)
        {
            if (HasItem(dL.DLId) == false)
            {

                _uow.DLs.Attach(dL);
                _uow.DLs.Remove(dL);
                return _uow.SaveChanges();
            }
            else
            {

                return 0;
            }

        }
        //public int DeleteDL(DL dL)
        //{
        //    _uow.DLs.Remove(dL);
        //    return _uow.SaveChanges();
        //}
        private Person _SelectedPerson;
        public Person SelectedPerson
        {
            get { return _SelectedPerson; }
            set { SetProperty(ref _SelectedPerson, value); }
        }

        public IEnumerable GetPeople()
        {
            return _uow.People.ToList();
            //return new List<Person> {
            //                new Person {NationalCode=1234567890,FatherName="Ali" },
            //                new Person { NationalCode = 1234567891,FatherName="Omid" },
            //                new Person { NationalCode = 1234567892,FatherName="Mehran" } };
        }

        public IEnumerable GetCompanies()
        {
            return _uow.Companies.ToList();
            //return new List<Company> {
            //                new Company{RegistrationNumber="222222222",EconomicalNumber="7878"},
            //                new Company{RegistrationNumber="333333333",EconomicalNumber="9090"},
            //                new Company{RegistrationNumber="444444444",EconomicalNumber="0000"},
            //            };
        }

        public IEnumerable GetBanks()
        {
            return _uow.BankAccounts.Include(x => x.Bank).ToList();
            //return new List<Ba9nkAccount> {
            //                new BankAccount{Bank=new Bank{BankName="Bank1" }, AccountNumber=444444 },
            //                new BankAccount{Bank=new Bank{BankName="Bank2" }, AccountNumber=555555 },
            //                new BankAccount{Bank=new Bank{BankName="Bank3" }, AccountNumber=666666 },
            //            };

        }
        public void InsertPerson(long dlCode, string Title)
        {
         //   var c = ($" INSERT INTO dbo.People(Name,Family,Dcode, NationalCode, CertificateNumber, BirthDate, FatherName, NumberOfChildren, PostalCode, Sexuality, BirthPlace, CertificatePlace, CertificateSeries, Address, Address2, WebSite, FirstAccountBalance, Fax, Logo)" +
          //      $"VALUES(N'{Title}',N'',{dlCode}, 0, 0, GETDATE(), N'', 0, 0, N'', N'', N'', N'', N'', N'', N'', 0.0, 0, N'')");
            //  $"INSERT INTO dbo.people(DCode, Name) VALUES ({dlCode}, '{Title}')");
         //   var x = _uow.Database.ExecuteSqlCommand(c);
        }
        public void InsertCompany(long dlCode, string Title)
        {
           // var c = ($" INSERT INTO		dbo.Companies(CompanyName,Dcode, DateRegistration, RegistrationNumber, EconomicalNumber, City, Province, Town, PostalCode, PhoneManager, ManagerName, Phone1, Phone2, Phone3, Address, LatinName, FirstAccountBalance, Fax, Logo, WebSite)VALUES" +
               // $"(N'{Title}',{dlCode}, GETDATE(), N'', N'', N'', N'', N'', 0, N'', N'', N'', N'', N'', N'', N'', 0, 0, N'', N'')");
            //  $"INSERT INTO dbo.people(DCode, Name) VALUES ({dlCode}, '{Title}')");
          //  var x = _uow.Database.ExecuteSqlCommand(c);
        }
        public void InsertBankAccount(long dlCode, string Title)
        {
            // var AccountNumber = Convert.ToInt64(Title);
            // var c = ($" INSERT INTO dbo.BankAccounts(Dcode, BankId, AccountNumber, Branch, AccountTypeId, ShabaNumber, Addrress, PostalCode, CardNumber, PoseId, WebSite, OpeningDate, FirstReservePeriod, InventoryReservation, CurrencyId, ExchangeRate, Status)VALUES" +
            //  $"({dlCode}, 0, {AccountNumber}, N'', 0, N'', N'', 0, 0, N'', N'', GETDATE(), 0.0, 0.0, 0, NULL, NULL)");
            //  $"INSERT INTO dbo.people(DCode, Name) VALUES ({dlCode}, '{Title}')");
            //  var x = _uow.Database.ExecuteSqlCommand(c);
        }
        public bool HasPerson(long dlCode, string Title)
        {
            return _uow.People.Any(x => x.Name == Title && x.Dcode == dlCode);
        }
        public bool HasCompany(long dlCode, string Title)
        {
            return _uow.Companies.Any(x => x.CompanyName == Title && x.Dcode == dlCode);
        }
        private BankCurrencyEnum _BankCurrencyEnum;

        public BankCurrencyEnum BankCurrencyEnum
        {
            get { return _BankCurrencyEnum; }
            set
            {
                SetProperty(ref _BankCurrencyEnum, value);
            }
        }

        public event Action<BankCurrencyEnum> BankCurrencyRequested;

        public void RaiseRequested(BankCurrencyEnum bankCurrencyEnum)
        {
            BankCurrencyRequested?.Invoke(bankCurrencyEnum);
        }
        #region BankAccount
        //حساب بانکی
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
        private IAccountTypesService _accountTypesService;

        private IBankAccountsService _bankAccountsService;
        private List<BankAccount> _allBankAccounts;
        private IBanksService _banksService;
        private List<Bank> _allBanks;
        private ICurrenciesService _currenciesService;
        private List<Currency> _allCurrencies;
        private List<AccountType> _allAccountTypes;
        public async void LoadBankAccounts()
        {
            _allBankAccounts = await _bankAccountsService.GetBankAccountsAsync();
            BankAccounts = new ObservableCollection<BankAccount>(_allBankAccounts);
        }
       
        private async void OnAccountTypesDropDownOpened()
        {
            _allAccountTypes = await _accountTypesService.GetAccountTypesAsync();
            AccountTypes = new ObservableCollection<AccountType>(_allAccountTypes);
        }
        public void LoadBanks()
        {
            //_uow = new SainaDbContext();
            //if (Banks?.Any() != true)
            //{
                Banks = new ObservableCollection<Bank>(_uow.Banks.ToList());
                Currencies = new ObservableCollection<Currency>(_uow.Currencies.ToList());
                AccountTypes = new ObservableCollection<AccountType>(_uow.AccountTypes.ToList());
                OnBanksDropDownOpened();
                OnCurrenciesDropDownOpened();
                OnAccountTypesDropDownOpened();

           // }
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
        public void SendTitle()
        {
        }
        #endregion
        #region Person
        private DL _DL;
        public DL DL
        {
            get { return _DL; }
            set { SetProperty(ref _DL, value); }
        }
        private bool _active;

        public bool Active
        {
            get { return _active; }
            set { SetProperty(ref _active, value); }
        }

        //#endregion
        //#region Company
        //private Company _Company;
        //public Company Company
        //{
        //    get { return _Company; }
        //    set { SetProperty(ref _Company, value); }
        //}
        #endregion
    }
}
