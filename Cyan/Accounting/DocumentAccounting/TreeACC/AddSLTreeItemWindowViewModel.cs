using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using Saina.Common.Toolkit;
using Saina.WPF.BasicInformation.Information.BankAccounts;
using Saina.WPF.BasicInformation.Information.CompanyInfo;
//using Saina.WPF.BasicInformation.Information.PersonInfo;
using Saina.WPF.BasicInformation.Settings.AccountingSetting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Telerik.Windows.Controls;

namespace Saina.WPF.Accounting.DocumentAccounting.TreeACC
{
    public class AddSLTreeItemWindowViewModel : BindableBase
    {
        #region Constructors
        public AddSLTreeItemWindowViewModel(ISLsService sLsService, ITLsService tLsService, IDLTypesService dLTypesService, ISystemAccountingSettingsService systemAccountingSettingsService, IPropertiesService propertiesService, IAccountsNaturesService accountsNaturesService)
        {
            _dLTypesService = dLTypesService;
            _sLsService = sLsService;
            _tLsService = tLsService;
            _systemAccountingSettingsService = systemAccountingSettingsService;
            _propertiesService = propertiesService;
            _accountsNaturesService = accountsNaturesService;
            //  CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            TLsDropDownOpenedCommand = new RelayCommand(OnTLsDropDownOpened, () => TLs != null && TLs.Any());
            PropertiesDropDownOpenedCommand = new RelayCommand(OnPropertiesDropDownOpened, () => Properties != null && Properties.Any());
            AccountsNaturesDropDownOpenedCommand = new RelayCommand(OnAccountsNaturesDropDownOpened, () => AccountsNatures != null && AccountsNatures.Any());
            SelectedDLTypes1 = new ObservableCollection<DLType>();
            SelectedDLTypes2 = new ObservableCollection<DLType>();
          //  SLStandardDescriptionListViewModel = SmObjectFactory.Container.GetInstance<SLStandardDescriptionListViewModel>();
            SL = new SL();
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
        private SL _SL;
        public SL SL
        {
            get { return _SL; }
            set { SetProperty(ref _SL, value);
                if (SL != null)
                {
                    SL.PropertyChanged += SL_PropertyChanged;
                }
            }
        }

        private void SL_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TL")
            {
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
                r += (SL.TL.TLCode) * x;
                string s = "0," + (SystemAccountingSettingModelSLLength - SystemAccountingSettingModelTLLength).ToString();
                var lastTLCode = _sLsService.GetLastSLCode(SL.TL.TLId);
                if (lastTLCode == SL.TL.TLId)
                {
                    var stringLastTLCode = "1";
                    var lastSLCode = stringLastTLCode.ToString().PadLeft(SystemAccountingSettingModelSLLength - SystemAccountingSettingModelTLLength, '0');
                    SL.SLCode = int.Parse($"{SL.TL.TLCode}{lastSLCode}");

                    SL.Regex = $"^{SL.TL.TLCode}[0-9]{{{s}}}$";
                    SL.SLCodeEmptyValue = SL.TL.TLCode;
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

                        SL.Regex = $"^{SL.TL.TLCode}[0-9]{{{s}}}$";
                        SL.SLCodeEmptyValue = SL.TL.TLCode;
                    }
                    else
                    {
                        DialogParameters parameters = new DialogParameters();
                        parameters.OkButtonContent = "بستن";
                        parameters.Header = "اخطار";
                        parameters.Content = " شماره گذاری این حساب  به پایان رسیده است";
                        RadWindow.Alert(parameters);

                    }

                }
            }
        }

        private SL _oldSL;
        public SL OldSL
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
        //private SLStandardDescriptionListViewModel _sLStandardDescriptionListViewModel;

        //public SLStandardDescriptionListViewModel SLStandardDescriptionListViewModel
        //{
        //    get { return _sLStandardDescriptionListViewModel; }
        //    set { SetProperty(ref _sLStandardDescriptionListViewModel, value); }
        //}
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
                            DialogParameters parameters = new DialogParameters();
                            parameters.OkButtonContent = "بستن";
                            parameters.Header = "اخطار";
                            parameters.Content = " شماره گذاری این حساب  به پایان رسیده است";
                            RadWindow.Alert(parameters);

                        }

                    }
                }
            }
        }
        public event Action<SL> SaveClicked;
        public SL DataItem { get; set; }
        public event Action<Exception> Failed;

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
                //var sl = sender as SL;
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
            //if (EditMode == false)
               // SLStandardDescriptionListViewModel.SLStandardDescriptions = new ObservableCollection<SLStandardDescription>();
            //  SL = Mapper.Map<SL, EditableSL>(sL);
            //  OldSL = Mapper.Map<SL, EditableSL>(sL);
            //SL.ValidationDelegate += SL_ValidationDelegate;
            SL.ErrorsChanged += RaiseCanExecuteChanged;
            var selectedDLTypes1 = EnumHelper.GetFlags(sL.DLType1).ToList();
            var selectedDLTypes2 = EnumHelper.GetFlags(sL.DLType2).ToList();
            DLTypes = new ObservableCollection<DLType>(await _dLTypesService.GetDLTypesAsync());

        }
        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }
        //private void OnCancel()
        //{
        //    SL = new EditableSL();
        //    UnLoaded();
        //    Done?.Invoke();
        //}
        private async void OnSave()
        {
            string errors = "";
            //   var editingGL = Mapper.Map<EditableGL, GL>(GL);
            if (SL.SLId == 0)
            {


                if (SL.Title == null || SL.Title == "")
                {

                    errors += "عنوان خالی می باشد" + Environment.NewLine;

                }
                if (SL.SLCode == 0)
                {
                    errors += "کد حساب اشتباه می باشد" + Environment.NewLine;

                }
                //if (await _sLsService.HasTitle(SL.Title))
                //    errors += ("عنوان نباید تکراری باشد") + Environment.NewLine; ;
                //if (await _sLsService.HasDuplicate(SL.SLCode))
                //    errors += ("کد  حساب نباید تکراری باشد") + Environment.NewLine;
            }
            else
            {


                if (SL.Title == null || SL.Title == "")
                {

                    errors += "عنوان خالی می باشد" + Environment.NewLine;

                }
                if (SL.SLCode == 0)
                {
                    errors += "کد حساب اشتباه می باشد" + Environment.NewLine;

                }
                if (await _sLsService.HasTitleTree(SL.Title,SL.SLId))
                    errors += ("عنوان نباید تکراری باشد") + Environment.NewLine; ;
                if (await _sLsService.HasduplicateTree(SL.SLCode, SL.SLId))
                    errors += ("کد  حساب نباید تکراری باشد") + Environment.NewLine;
            }
            if (errors.Length > 0)
            {
                DialogParameters parameters = new DialogParameters();
                parameters.OkButtonContent = "بستن";
                parameters.Header = "اخطار";
                parameters.Content = errors;
                RadWindow.Alert(parameters);
            }
            else
            {
                try
                {
                    SaveClicked(SL);
                }
                catch (Exception ex)
                {
                    Failed(ex);
                }
                finally
                {
                    SL = null;
                }

            }

        }
        private bool CanSave()
        {
            //return !SL.HasErrors;
            return true;
        }
        //private async Task<List<string>> SL_ValidationDelegate(object sender, string propertyName)
        //{
        //    var sl = (SL)sender;
        //    List<string> errors = new List<string>();
        //    switch (propertyName)
        //    {
        //        case nameof(SL.Title):

        //            if (!(EditMode == true || OldSL.SLCode == sl.SLCode) && await _sLsService.HasTitle(sl.Title))
        //                errors.Add("عنوان نباید تکراری باشد");
        //            return errors;
        //        case nameof(SL.SLCode):
        //            if (!(EditMode == true || OldSL.Title == sl.Title) && await _sLsService.HasDuplicate(sl.SLCode))
        //                errors.Add("کد معین نباید تکراری باشد");
        //            return errors;
        //        default:
        //            return null;
        //    }
        //}

    }
    #endregion
}

