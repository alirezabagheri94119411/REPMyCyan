using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using Saina.WPF.Behaviors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace Saina.WPF.BasicInformation.Settings.AccountingSetting
{
    /// <summary>
    /// تنظیمات سیستم حسابداری
    /// </summary>
    public class SystemAccountingSettingViewModel : BindableBase
    {
        #region Fields
        private ISystemAccountingSettingsService _systemAccountingSettingsService;
        private IAppContextService _appContextService;
        private IGLsService _gLsService;
        private List<GL> _allGLs;

        private ITLsService _tLsService;
        private List<TL> _allTLs;
        private ISLsService _sLsService;
        private List<SL> _allSLs;
        private IDLsService _dLsService;
        private List<DL> _allDLs;
        #endregion
        #region Constructors

        public SystemAccountingSettingViewModel(IAppContextService appContextService, IGLsService gLsService, ITLsService tLsService, ISLsService sLsService, IDLsService dLsService, ISystemAccountingSettingsService systemAccountingSettingsService)
        {
            _gLsService = gLsService;
            _tLsService = tLsService;
            _sLsService = sLsService;
            _dLsService = dLsService;
            _appContextService = appContextService;
            _systemAccountingSettingsService = systemAccountingSettingsService;
            SystemAccountingSettingModel = AutoMapper.Mapper.Map<SystemAccountingSettingModel, EditableSystemAccountingSettingModel>(_systemAccountingSettingsService.GetSystemAccountingSettingModel());
            SystemAccountingSettingModel.ValidationDelegate += SystemAccountingSettingModel_ValidationDelegate;
            SaveCommand = new RelayCommand(onSave);
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();
        }

        private bool CanSave()
        {
            if ((SystemAccountingSettingModel.GLLength!=null && SystemAccountingSettingModel.GLLength!="" )&&
                (SystemAccountingSettingModel.TLLength != null && SystemAccountingSettingModel.TLLength != "")&&
                (SystemAccountingSettingModel.SLLength != null && SystemAccountingSettingModel.SLLength != "") &&
                (SystemAccountingSettingModel.DLLength != null && SystemAccountingSettingModel.DLLength != "")) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async void LoadSystemAccountingSettingModel()
        {
            _allGLs = await _gLsService.GetGLsAsync();
            GLs = new ObservableCollection<GL>(_allGLs);
            _allTLs = await _tLsService.GetTLsAsync();
            TLs = new ObservableCollection<TL>(_allTLs);
            _allSLs = await _sLsService.GetSLsAsync();
            SLs = new ObservableCollection<SL>(_allSLs);
            _allDLs = await _dLsService.GetDLsAsync();
            DLs = new ObservableCollection<DL>(_allDLs);
            SystemAccountingSettingModel.GLActive = true;
            SystemAccountingSettingModel.DLActive = true;
            SystemAccountingSettingModel.TLActive = true;
            SystemAccountingSettingModel.SLActive = true;
            if (GLs?.Any() != true)
            {
                SystemAccountingSettingModel.GLActive = true;
            }
            else
            {
                SystemAccountingSettingModel.GLActive = false;
            }
            if (TLs?.Any() != true)
            {
                SystemAccountingSettingModel.TLActive = true;
            }
            else
            {
                SystemAccountingSettingModel.TLActive = false;
            }
            if (SLs?.Any() != true)
            {
                SystemAccountingSettingModel.SLActive = true;
            }
            else
            {
                SystemAccountingSettingModel.SLActive = false;
            }
            if (DLs?.Any() != true)
            {
                SystemAccountingSettingModel.DLActive = true;
            }
            else
            {
                SystemAccountingSettingModel.DLActive = false;
            }
        }
            private async Task<List<string>> SystemAccountingSettingModel_ValidationDelegate(object sender, string propertyName)
        {
       
            var editableSystemAccountingSettingModel = (EditableSystemAccountingSettingModel)sender;
            List<string> errors = new List<string>();
            int.TryParse(editableSystemAccountingSettingModel.GLLength, out int gl);
            int.TryParse(editableSystemAccountingSettingModel.TLLength, out int tl);
            int.TryParse(editableSystemAccountingSettingModel.SLLength, out int sl);
            switch (propertyName)
            {
                case nameof(EditableSystemAccountingSettingModel.GLLength):
                    if (gl == 0)
                        {
                            errors.Add("عدد وارد نمایید");
                        }
                        if (gl >= tl)
                        {
                            errors.Add("کد گروه نباید بزرگتر از کد کل باشد");
                        }
                        if ((gl + tl + sl) > 25)
                        {
                            errors.Add("مجموع طول کد گروه، کد کل و کد معین نمی تواند بیشتر از 25 باشد");
                        }
                    
                   
                    //editableSystemAccountingSettingModel.ValidateProperty(nameof(EditableSystemAccountingSettingModel.TLLength), editableSystemAccountingSettingModel.TLLength);
                    //editableSystemAccountingSettingModel.ValidateProperty(nameof(EditableSystemAccountingSettingModel.SLLength), editableSystemAccountingSettingModel.SLLength);

                    break;
                case nameof(EditableSystemAccountingSettingModel.TLLength):
                   
                        if (tl == 0)
                        {
                            errors.Add("عدد وارد نمایید");
                        }
                        if (gl >= tl)
                        {
                            errors.Add("کد گروه نباید بزرگتر از کد کل باشد");
                        }
                        if ((gl + tl + sl) > 25)
                        {
                            errors.Add("مجموع طول کد گروه، کد کل و کد معین نمی تواند بیشتر از 25 باشد");
                        }
                        //editableSystemAccountingSettingModel.ValidateProperty(nameof(EditableSystemAccountingSettingModel.SLLength), editableSystemAccountingSettingModel.SLLength);
                        editableSystemAccountingSettingModel.ValidateProperty(nameof(EditableSystemAccountingSettingModel.GLLength), editableSystemAccountingSettingModel.GLLength);
                      
                    break;
                case nameof(EditableSystemAccountingSettingModel.SLLength):
                   
                        if (sl == 0)
                        {
                            errors.Add("عدد وارد نمایید");
                        }
                        if (tl >= sl)
                        {
                            errors.Add("کد کل نباید بزرگتر از کد معین باشد");
                        }
                        if ((gl + tl + sl) > 25)
                        {
                            errors.Add("مجموع طول کد گروه، کد کل و کد معین نمی تواند بیشتر از 25 باشد");
                        }
                        editableSystemAccountingSettingModel.ValidateProperty(nameof(EditableSystemAccountingSettingModel.TLLength), editableSystemAccountingSettingModel.TLLength);
                        editableSystemAccountingSettingModel.ValidateProperty(nameof(EditableSystemAccountingSettingModel.GLLength), editableSystemAccountingSettingModel.GLLength);
                  
                    break;
                case nameof(EditableSystemAccountingSettingModel.DLLength):
                    if (sl==0 || tl==0 || gl==0)
                    {
                        errors.Add("لطفا اطاعات را وارد نمایید.");
                    }
                    break;

                default:
                    return null;
            }
            //editableSystemAccountingSettingModel.NotifyAllPropertiesHaveChanged();
            return errors;

        }

        #endregion
        #region Commands
        public ICommand SaveCommand { get; set; }

        private AccessUtility _accessUtility;
        #endregion
        #region Properties
        public EditableSystemAccountingSettingModel SystemAccountingSettingModel { get; set; }
        private ObservableCollection<GL> _gLs;

        public ObservableCollection<GL> GLs
        {
            get { return _gLs; }
            set { SetProperty(ref _gLs, value); }
        }
        private ObservableCollection<TL> _tLs;

        public ObservableCollection<TL> TLs
        {
            get { return _tLs; }
            set { SetProperty(ref _tLs, value); }

        }
        private ObservableCollection<SL> _sLs;

        public ObservableCollection<SL> SLs
        {
            get { return _sLs; }
            set { SetProperty(ref _sLs, value); }

        }
        private ObservableCollection<DL> _dLs;

        public ObservableCollection<DL> DLs
        {
            get { return _dLs; }
            set { SetProperty(ref _dLs, value); }

        }

        public event Action<Exception> Failed;
        public event Action<string> Error;

        #endregion
        #region Methods

        private void onSave()
        {
            if (_accessUtility.HasAccess(1))
            {
                try
                {
                    var editableSystemAccountingSettingModel = AutoMapper.Mapper.Map<EditableSystemAccountingSettingModel, SystemAccountingSettingModel>(SystemAccountingSettingModel);
                    //     if ((SystemAccountingSettingModel.GLLength != null && SystemAccountingSettingModel.GLLength != "" && SystemAccountingSettingModel.GLLength != "0") &&
                    //(SystemAccountingSettingModel.TLLength != null && SystemAccountingSettingModel.TLLength != "" && SystemAccountingSettingModel.TLLength != "0") &&
                    //(SystemAccountingSettingModel.SLLength != null && SystemAccountingSettingModel.SLLength != "" && SystemAccountingSettingModel.SLLength != "0") &&
                    //(SystemAccountingSettingModel.DLLength != null && SystemAccountingSettingModel.DLLength != "" && SystemAccountingSettingModel.DLLength != "0"))
                    //{
                    //    editableSystemAccountingSettingModel.StatusAcc = "1";
                    //}
                    //else
                    //{
                    //    editableSystemAccountingSettingModel.StatusAcc = "0";
                    //}
                    _systemAccountingSettingsService.SaveSystemAccountingSettingModel(editableSystemAccountingSettingModel);
                   // Error?.Invoke(".تغییرات انجام شد");
                    MessageBox.Show("تنظیمات حسابداری با موفقیت انجام شد..", "اطلاعات");
                }
                catch (Exception ex)
                {
                    Failed(ex);
                }

                //  MessageBox.Show("تنظیمات حسابداری با موفقیت انجام شد..","اطلاعات");

            }
        }
        
        #endregion

    }
}
