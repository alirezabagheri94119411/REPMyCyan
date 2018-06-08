using AutoMapper;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using Saina.Common.Toolkit;
using Saina.WPF.BasicInformation.Accounts.SLStandard;
using Saina.WPF.BasicInformation.Settings.AccountingSetting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saina.ApplicationCore.DTOs;
//using Saina.WPF.BasicInformation.Information.PersonInfo;
using Saina.WPF.BasicInformation.Information.CompanyInfo;
using Saina.WPF.BasicInformation.Information.BankAccounts;

namespace Saina.WPF.BasicInformation.Accounts.SLAccount
{
    /// <summary>
    /// افزودن و ویرایش حساب معین
    /// </summary>
    public class AddEditSLViewModel : BindableBase
    {
        #region Constructors
        public AddEditSLViewModel(ISLsService sLsService, ITLsService tLsService, IDLTypesService dLTypesService, ISystemAccountingSettingsService systemAccountingSettingsService, IPropertiesService propertiesService, IAccountsNaturesService accountsNaturesService)
        {
            _dLTypesService = dLTypesService;
            _sLsService = sLsService;
            _tLsService = tLsService;
            _systemAccountingSettingsService = systemAccountingSettingsService;
            _propertiesService = propertiesService;
            _accountsNaturesService = accountsNaturesService;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            SaveAddCommand = new RelayCommand(OnSaveAdd, CanSaveAdd);
            TLsDropDownOpenedCommand = new RelayCommand(OnTLsDropDownOpened, () => TLs != null && TLs.Any());
            PropertiesDropDownOpenedCommand = new RelayCommand(OnPropertiesDropDownOpened, () => Properties != null && Properties.Any());
            AccountsNaturesDropDownOpenedCommand = new RelayCommand(OnAccountsNaturesDropDownOpened, () => AccountsNatures != null && AccountsNatures.Any());
            SelectedDLTypes1 = new ObservableCollection<DLType>();
            SelectedDLTypes2 = new ObservableCollection<DLType>();
            SLStandardDescriptionListViewModel = SmObjectFactory.Container.GetInstance<SLStandardDescriptionListViewModel>();
        }



        #endregion
        #region Fields
        private ISLsService _sLsService;
        private ISystemAccountingSettingsService _systemAccountingSettingsService;

        private List<TL> _allTLs;
        private List<Property> _allProperties;
        private List<DLType> _allDLTypes;
        private List<AccountsNature> _allAccountsNatures;

        private IDLTypesService _dLTypesService;
        private ITLsService _tLsService;
        private IAccountsNaturesService _accountsNaturesService;
        private IPropertiesService _propertiesService;
        private SL _editingSL = null;
        #endregion
        #region Commands
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand SaveAddCommand { get; }
        public RelayCommand TLsDropDownOpenedCommand { get; private set; }
        public RelayCommand AccountsNaturesDropDownOpenedCommand { get; private set; }
        public RelayCommand PropertiesDropDownOpenedCommand { get; private set; }


        #endregion
        #region Properties
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }
        private EditableSL _SL;
        public EditableSL SL
        {
            get { return _SL ?? new EditableSL(); }
            set { SetProperty(ref _SL, value); }
        }
        private EditableSL _oldSL;
        public EditableSL OldSL
        {
            get { return _SL; }
            set { SetProperty(ref _SL, value); }
        }
        private ObservableCollection<TL> _tLs;
        public ObservableCollection<TL> TLs
        {
            get { return _tLs; }
            set { SetProperty(ref _tLs, value); }
        }
        private ObservableCollection<Property> _properties;

        public ObservableCollection<Property> Properties
        {
            get { return _properties; }
            set { SetProperty(ref _properties, value); }
        }
        private ObservableCollection<AccountsNature> _accountsNatures;

        public ObservableCollection<AccountsNature> AccountsNatures
        {
            get { return _accountsNatures; }
            set { SetProperty(ref _accountsNatures, value); }

        }
        private ObservableCollection<DLType> _dLTypes;

        public ObservableCollection<DLType> DLTypes
        {
            get { return _dLTypes; }
            set { SetProperty(ref _dLTypes, value); }
        }
        private ObservableCollection<DLType> selectedDLTypes1;

        public ObservableCollection<DLType> SelectedDLTypes1
        {
            get { return selectedDLTypes1; }
            set
            {
                SetProperty(ref selectedDLTypes1, value);
            }
        }
        private ObservableCollection<DLType> selectedDLTypes2;

        public ObservableCollection<DLType> SelectedDLTypes2
        {
            get { return selectedDLTypes2; }
            set
            {
                SetProperty(ref selectedDLTypes2, value);
            }
        }
        private SLStandardDescriptionListViewModel _sLStandardDescriptionListViewModel;

        public SLStandardDescriptionListViewModel SLStandardDescriptionListViewModel
        {
            get { return _sLStandardDescriptionListViewModel; }
            set { SetProperty(ref _sLStandardDescriptionListViewModel, value); }
        }
        //private PersonListViewModel _personListViewModel;

        //public PersonListViewModel PersonListViewModel
        //{
        //    get { return _personListViewModel; }
        //    set { SetProperty(ref _personListViewModel, value); }
        //}
        private CompanyListViewModel _companyListViewModel;

        public CompanyListViewModel CompanyListViewModel
        {
            get { return _companyListViewModel; }
            set { SetProperty(ref _companyListViewModel, value); }
        }
        private BankAccountListViewModel _bankAccountListViewModel;

        public BankAccountListViewModel BankAccountListViewModel
        {
            get { return _bankAccountListViewModel; }
            set { SetProperty(ref _bankAccountListViewModel, value); }
        }
        private TL _selectedTL;

        public TL SelectedTL
        {
            get { return _selectedTL; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref _selectedTL, value);
                    var SystemAccountingSettingModel = AutoMapper.Mapper.Map<SystemAccountingSettingModel, EditableSystemAccountingSettingModel>(_systemAccountingSettingsService.GetSystemAccountingSettingModel());
                    var SystemAccountingSettingModelSLLength = int.Parse(SystemAccountingSettingModel.SLLength);
                    var SystemAccountingSettingModelTLLength = int.Parse(SystemAccountingSettingModel.TLLength);
                    var len = SystemAccountingSettingModelSLLength - SystemAccountingSettingModelTLLength;
                    long x = 1;
                    long r = 0;
                    for (int i = 1; i <= len; i++)
                    {
                        r += x * 9;
                        x *= 10;
                    }
                    r += (SelectedTL.TLCode) * x;
                    string s = "0," + (SystemAccountingSettingModelSLLength - SystemAccountingSettingModelTLLength).ToString();
                    var lastTLCode = _sLsService.GetLastSLCode(SelectedTL.TLId);
                    if (lastTLCode == SelectedTL.TLId)
                    {
                        var stringLastTLCode = "1";
                        var lastSLCode = stringLastTLCode.ToString().PadLeft(SystemAccountingSettingModelSLLength - SystemAccountingSettingModelTLLength, '0');
                        SL.SLCode = int.Parse($"{SelectedTL.TLCode}{lastSLCode}");

                        SL.Regex = $"^{SelectedTL.TLCode}[0-9]{{{s}}}$";
                        SL.SLCodeEmptyValue = SelectedTL.TLCode;
                    }
                    else
                    {

                        lastTLCode++;
                        //var lastTLLenght = (lastGLCode.ToString()).Length;
                        if (lastTLCode < r)
                        {


                            var stringLastTLCode = lastTLCode.ToString();
                            var lastSLCode = stringLastTLCode.ToString().PadLeft(SystemAccountingSettingModelSLLength - SystemAccountingSettingModelTLLength, '0');
                            SL.SLCode = int.Parse($"{lastTLCode}");

                            SL.Regex = $"^{SelectedTL.TLCode}[0-9]{{{s}}}$";
                            SL.SLCodeEmptyValue = SelectedTL.TLCode;
                        }
                        else
                        {
                            Error?.Invoke("شماره گذاری این حساب  به پایان رسیده است");
                        }

                    }

                }
            }
        }
        public event Action Done;
        public event Action<Exception> Failed;
        public event Action<string> Error;
        public event Action<SL> AddSLRequested;

        #endregion
        #region Methode
        private async void OnTLsDropDownOpened()
        {
            _allTLs = await _tLsService.GetTLsActiveAsync();
            TLs = new ObservableCollection<TL>(_allTLs);
        }
        private async void OnPropertiesDropDownOpened()
        {
            _allProperties = await _propertiesService.GetPropertiesAsync();
            Properties = new ObservableCollection<Property>(_allProperties);
        }
        private async void OnAccountsNaturesDropDownOpened()
        {
            _allAccountsNatures = await _accountsNaturesService.GetAccountsNaturesAsync();
            AccountsNatures = new ObservableCollection<AccountsNature>(_allAccountsNatures);
        }
        public async void LoadTLs()
        {

            if (TLs == null)
            {
                _allTLs = await _tLsService.GetTLsActiveAsync();
                TLs = new ObservableCollection<TL>(_allTLs);
            }
            if (Properties == null)
            {
                _allProperties = await _propertiesService.GetPropertiesAsync();
                Properties = new ObservableCollection<Property>(_allProperties);
            }
            if (AccountsNatures == null)
            {
                _allAccountsNatures = await _accountsNaturesService.GetAccountsNaturesAsync();
                AccountsNatures = new ObservableCollection<AccountsNature>(_allAccountsNatures);
            }

            SL.PropertyChanged += (sender, earg) =>
            {
                //var sl = sender as EditableSL;
                //if (earg.PropertyName == "DLType1" || earg.PropertyName == "DLType2")
                //    if (sl.DLType1.HasFlag(DLTypeEnum.People) || sl.DLType2.HasFlag(DLTypeEnum.People))
                //        PersonListViewModel = SmObjectFactory.Container.GetInstance<PersonListViewModel>();
                //if (sl.DLType1.HasFlag(DLTypeEnum.Company) || sl.DLType2.HasFlag(DLTypeEnum.Company))
                //    CompanyListViewModel = SmObjectFactory.Container.GetInstance<CompanyListViewModel>();
                //if (sl.DLType1.HasFlag(DLTypeEnum.BankAccount) || sl.DLType2.HasFlag(DLTypeEnum.BankAccount))
                //    BankAccountListViewModel = SmObjectFactory.Container.GetInstance<BankAccountListViewModel>();
            };
        }
        public override void UnLoaded()
        {
            TLs = null;
            Properties = null;
            AccountsNatures = null;
           
        }

        public async void SetSL(SL sL)
        {
            if (EditMode == false)
                SLStandardDescriptionListViewModel.SLStandardDescriptions = new ObservableCollection<SLStandardDescription>();
            SL = Mapper.Map<SL, EditableSL>(sL);
            OldSL = Mapper.Map<SL, EditableSL>(sL);
            SL.ValidationDelegate += SL_ValidationDelegate;
            SL.ErrorsChanged += RaiseCanExecuteChanged;
            var selectedDLTypes1 = EnumHelper.GetFlags(sL.DLType1).ToList();
            var selectedDLTypes2 = EnumHelper.GetFlags(sL.DLType2).ToList();
            DLTypes = new ObservableCollection<DLType>(await _dLTypesService.GetDLTypesAsync());
            SL.TLId = null;
            SelectedTL = null;
            //foreach (var item in selectedDLTypes1)
            //{
            //    var dLTypeId = Convert.ToInt32(item);
            //    SelectedDLTypes1.Add(new DLType
            //    {
            //        DLTypeId = dLTypeId,
            //        DLTypeTitle = (await _dLTypesService.GetDLTypeIdAsync(dLTypeId)).DLTypeTitle
            //    });
            //}
            //foreach (var item in selectedDLTypes2)
            //{
            //    var dLTypeId = Convert.ToInt32(item);
            //    SelectedDLTypes2.Add(new DLType
            //    {
            //        DLTypeId = dLTypeId,
            //        DLTypeTitle = (await _dLTypesService.GetDLTypeIdAsync(dLTypeId)).DLTypeTitle
            //    });
            //}
        }
        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
            SaveAddCommand.RaiseCanExecuteChanged();
        }
        private void OnCancel()
        {
            SL = new EditableSL();
            UnLoaded();
            Done?.Invoke();
        }
        private async void OnSave()
        {
            var editingSL = Mapper.Map<EditableSL, SL>(SL);
            foreach (var sLStandardDescriptions in SLStandardDescriptionListViewModel.SLStandardDescriptions)
            {
                sLStandardDescriptions.SLId = SL.SLId;
            }
            editingSL.SLStandardDescriptions = SLStandardDescriptionListViewModel.SLStandardDescriptions;
            try
            {
                if (EditMode)
                    await _sLsService.UpdateSLAsync(editingSL);
                else
                    await _sLsService.AddSLAsync(editingSL);
               Done?.Invoke();
            }
            catch (Exception ex)
            {
                Failed?.Invoke(ex);
            }
            finally
            {
                  SL = null;
                //SL = new EditableSL();
            }
        }

        private bool CanSave()
        {
            return !SL.HasErrors;
           // return true;
        }
        private bool CanSaveAdd()
        {
            //return !SL.HasErrors;
            return true;
        }

        private async void OnSaveAdd()
        {
            var editingSL = Mapper.Map<EditableSL, SL>(SL);
            foreach (var sLStandardDescriptions in SLStandardDescriptionListViewModel.SLStandardDescriptions)
            {
                sLStandardDescriptions.SLId = SL.SLId;
            }
            editingSL.SLStandardDescriptions = SLStandardDescriptionListViewModel.SLStandardDescriptions;
            try
            {
                if (EditMode)
                    await _sLsService.UpdateSLAsync(editingSL);
                else
                    await _sLsService.AddSLAsync(editingSL);
                Done?.Invoke();
            }
            catch (Exception ex)
            {
                Failed?.Invoke(ex);
            }
            finally
            {
                AddSLRequested(new SL());
            }
        }



        private async Task<List<string>> SL_ValidationDelegate(object sender, string propertyName)
        {
            var sl = (EditableSL)sender;
            List<string> errors = new List<string>();
            switch (propertyName)
            {
                case nameof(SL.Title):

                    //if (!(EditMode == true || OldSL.SLCode == sl.SLCode) && await _sLsService.HasTitle(sl.Title))
                    //    errors.Add("عنوان نباید تکراری باشد");
                    //return errors;
                case nameof(SL.SLCode):
                    //if (!(EditMode == true || OldSL.Title == sl.Title) && await _sLsService.HasDuplicate(sl.SLCode))
                    //    errors.Add("کد معین نباید تکراری باشد");
                    //return errors;
                default:
                    return null;
            }
        }

    }
    #endregion
}
