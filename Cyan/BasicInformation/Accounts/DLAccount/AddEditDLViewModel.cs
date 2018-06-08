using AutoMapper;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.Common.Toolkit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Saina.ApplicationCore.DTOs;

using System.Text;
using System.Threading.Tasks;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using Saina.WPF.BasicInformation.Settings.AccountingSetting;
using Saina.WPF.BasicInformation.Information.CompanyInfo;
//using Saina.WPF.BasicInformation.Information.PersonInfo;
using Saina.WPF.BasicInformation.Information.BankAccounts;

namespace Saina.WPF.BasicInformation.Accounts.DLAccount
{
    /// <summary>
    /// افزودن و ویرایش  حساب تفصیل
    /// </summary>
    public class AddEditDLViewModel : BindableBase
    {
        #region Constructors
        public AddEditDLViewModel(IDLsService dLsService, ISLsService sLsService, IDLTypesService dLTypesService, IDLAccountsNaturesService dLAccountsNatureService, ISystemAccountingSettingsService systemAccountingSettingsService)
        {
            _systemAccountingSettingsService = systemAccountingSettingsService;

            _dLsService = dLsService;
            _sLsService = sLsService;
            _dLTypesService = dLTypesService;
            _dLAccountsNatureService = dLAccountsNatureService;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            SelectedDLTypes = new ObservableCollection<DLType>();
            SelectedDLAccountsNatures = new ObservableCollection<DLAccountsNature>();
        }
        #endregion
        #region Fields
        ISystemAccountingSettingsService _systemAccountingSettingsService;
        private IDLsService _dLsService;
        private List<SL> _allSLs;
        private ISLsService _sLsService;
        private IDLTypesService _dLTypesService;
        private IDLAccountsNaturesService _dLAccountsNatureService;
        #endregion
        #region Commands
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand SLsDropDownOpenedCommand { get; private set; }
        #endregion
        #region Properties
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }
        private EditableDL _DL;
        public EditableDL DL
        {
            get { return _DL; }
            set
            {
                SetProperty(ref _DL, value);
              
                
            }
        }

        private ObservableCollection<SL> _sLs;
        public ObservableCollection<SL> SLs
        {
            get { return _sLs; }
            set { SetProperty(ref _sLs, value); }
        }
        private ObservableCollection<DLType> _dLTypes;

        public ObservableCollection<DLType> DLTypes
        {
            get { return _dLTypes; }
            set { SetProperty(ref _dLTypes, value); }
        }
        private ObservableCollection<DLType> selectedDLTypes;

        public ObservableCollection<DLType> SelectedDLTypes
        {
            get { return selectedDLTypes; }
            set { SetProperty(ref selectedDLTypes, value); }
        }
        private ObservableCollection<DLAccountsNature> _dLAccountsNatures;

        public ObservableCollection<DLAccountsNature> DLAccountsNatures
        {
            get { return _dLAccountsNatures; }
            set { SetProperty(ref _dLAccountsNatures, value); }
        }
        private ObservableCollection<DLAccountsNature> selectedDLAccountsNatures;

        public ObservableCollection<DLAccountsNature> SelectedDLAccountsNatures
        {
            get { return selectedDLAccountsNatures; }
            set { SetProperty(ref selectedDLAccountsNatures, value); }
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
        public event Action Done;
        public event Action<Exception> Failed;
        #endregion
        #region Methode
        public void LoadDLs()
        {
            if (!EditMode)
            {

          
            var SystemAccountingSettingModel = AutoMapper.Mapper.Map<SystemAccountingSettingModel, EditableSystemAccountingSettingModel>(_systemAccountingSettingsService.GetSystemAccountingSettingModel());
            var SystemAccountingSettingModelDLLength = int.Parse(SystemAccountingSettingModel.DLLength);

            string s = "0," + SystemAccountingSettingModelDLLength.ToString();
            var lastDLCode = _dLsService.GetLastDLCode() + 1;
            var stringLastDLCode = lastDLCode.ToString();
            var lastDLsCode = stringLastDLCode.ToString().PadRight(SystemAccountingSettingModelDLLength, '0');
            DL.DLCode = int.Parse($"{lastDLsCode}");
            DL.Regex = $"^[0-9]{{{s}}}$";
            DL.DLCodeEmpyValue = lastDLCode;
              
            }
            DL.PropertyChanged += (sender, earg) =>
            {
                //var dl = sender as EditableDL;
                //if (earg.PropertyName == "DLType")
                //    if (dl.DLType.HasFlag(DLTypeEnum.People))
                //        PersonListViewModel = SmObjectFactory.Container.GetInstance<PersonListViewModel>();
                //if (dl.DLType.HasFlag(DLTypeEnum.Company))
                //    CompanyListViewModel = SmObjectFactory.Container.GetInstance<CompanyListViewModel>();
                //if (dl.DLType.HasFlag(DLTypeEnum.BankAccount))
                //    BankAccountListViewModel = SmObjectFactory.Container.GetInstance<BankAccountListViewModel>();
            };
        }

        public  void SetDL(DL dL)
        {

            DL = Mapper.Map<DL, EditableDL>(dL);
          //  DL.ValidationDelegate += DL_ValidationDelegate;
            DL.ErrorsChanged += RaiseCanExecuteChanged;
          
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
            var editingDL = Mapper.Map<EditableDL, DL>(DL);
            try
            {
                if (EditMode)
                    await _dLsService.UpdateDLAsync(editingDL);
                else
                    await _dLsService.AddDLAsync(editingDL);
                Done?.Invoke();
            }
            catch (Exception ex)
            {
                Failed(ex);
            }
            finally
            {
                DL = null;
            }

        }

        private bool CanSave()
        {
            return !DL.HasErrors;
        }
        //private async Task<List<string>> DL_ValidationDelegate(object sender, string propertyName)
        //{
        //    var dl = (EditableDL)sender;
        //    List<string> errors = new List<string>();
        //    //switch (propertyName)
        //    //{
        //    //    case nameof(DL.Title):

        //    //        if (await _dLsService.HasTitle(dl.Title))
        //    //            errors.Add("عنوان نباید تکراری باشد");
        //    //        return errors;
        //    //    case nameof(DL.DLCode):
        //    //        if (await _dLsService.Hasduplicate(dl.DLCode))
        //    //            errors.Add("کد تفصیل نباید تکراری باشد");
        //    //        return errors;
        //    //    default:
        //    //        return null;
        //    //}
        //}
        #endregion












    }
}
