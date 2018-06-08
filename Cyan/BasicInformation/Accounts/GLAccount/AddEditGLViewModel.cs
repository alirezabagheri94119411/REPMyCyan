using AutoMapper;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using Saina.WPF.BasicInformation.Settings.AccountingSetting;
using System;
using System.Collections.Generic;
using Saina.ApplicationCore.DTOs;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Accounts.GLAccount
{
    /// <summary>
    /// افزودن و ویرایش حساب گروه
    /// </summary>
    public class AddEditGLViewModel : BindableBase
    {
        #region Constructors
        public AddEditGLViewModel(IGLsService gLsService, ISystemAccountingSettingsService systemAccountingSettingsService)
        {
            _systemAccountingSettingsService = systemAccountingSettingsService;

            _gLsService = gLsService;

            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            SaveAddCommand = new RelayCommand(OnSaveAdd, CanSaveAdd);
            GL = new EditableGL();
        }
        #endregion
        #region Fields
        private ISystemAccountingSettingsService _systemAccountingSettingsService;
        private IGLsService _gLsService;

        private GL _editingGL = null;
        #endregion
        #region Commands
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand SaveAddCommand { get; }
        public RelayCommand AccountGroupsDropDownOpenedCommand { get; private set; }
        #endregion
        #region Properties
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }
        private EditableGL _GL;
        public EditableGL GL
        {
            get { return _GL; }
            set
            {
                SetProperty(ref _GL, value);

            }
        }

        public event Action Done;
        public event Action<Exception> Failed;

        #endregion
        #region Methode


        public void SetGL(GL gL)
        {
            GL = Mapper.Map<GL, EditableGL>(gL);
            GL.ValidationDelegate += GL_ValidationDelegate;
            GL.ErrorsChanged += RaiseCanExecuteChanged;
        }
        public void LoadGLs()
        {
            var SystemAccountingSettingModel = AutoMapper.Mapper.Map<SystemAccountingSettingModel, EditableSystemAccountingSettingModel>(_systemAccountingSettingsService.GetSystemAccountingSettingModel());
            var SystemAccountingSettingModelGLLength = int.Parse(SystemAccountingSettingModel.GLLength);

            string s = "0," + SystemAccountingSettingModelGLLength.ToString();
            long lastGLCode= _gLsService.GetLastGLCode();
            if (EditMode == false)
            {

                lastGLCode = _gLsService.GetLastGLCode() + 1;
               // var lenght = lastGLCode.ToString().Length;

                if (lastGLCode == 1)
                {
                    var stringLastGLCode = lastGLCode.ToString();
                    var lastGLsCode = stringLastGLCode.ToString().PadRight(SystemAccountingSettingModelGLLength, '0');
                    GL.GLCode = int.Parse(lastGLsCode);
                }

           
                else if (lastGLCode != 1)
                {
                    GL.GLCode = lastGLCode;
                }
                GL.GLCodeEmptyValue = lastGLCode;
            }
          
           // var cc = int.Parse(lastGLsCode);
            GL.Regex = $"^[0-9]{{{s}}}$";
          //  GL.GLCodeEmptyValue = lastGLCode;
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
            var editingGL = Mapper.Map<EditableGL, GL>(GL);
            try
            {
                if (EditMode)
                    await _gLsService.UpdateGLAsync(editingGL);
                else
                    await _gLsService.AddGLAsync(editingGL);
                Done?.Invoke();
            }
            catch (Exception ex)
            {
                Failed(ex);
            }
            finally
            {
                GL = null;
            }

        }

        private bool CanSave()
        {
            return !GL.HasErrors;
        }
        private bool CanSaveAdd()
        {
            return !GL.HasErrors;
            //return true;
        }

        private async void OnSaveAdd()
        {
            var editingGL = Mapper.Map<EditableGL, GL>(GL);
            try
            {
                if (EditMode)
                    await _gLsService.UpdateGLAsync(editingGL);
                else
                    await _gLsService.AddGLAsync(editingGL);
             //   Done?.Invoke();
            }
            catch (Exception ex)
            {
                Failed(ex);
            }
            finally
            {
                GL = null;
            }
        }
        private async Task<List<string>> GL_ValidationDelegate(object sender, string propertyName)
        {
            var gl = (EditableGL)sender;
            List<string> errors = new List<string>();
            switch (propertyName)
            {
                case nameof(GL.Title):

                    //if (await _gLsService.HasTitle(gl.Title))
                    //    errors.Add("عنوان نباید تکراری باشد");
                   
                    return errors;
                case nameof(GL.GLCode):
                    //if (await _gLsService.Hasduplicate(gl.GLCode))
                    //    errors.Add("کد  گروه نباید تکراری باشد");
                    if (GL.GLCode == 0)
                    {
                        errors.Add("کد  گروه نباید 0 باشد");

                    }
                    return errors;
                   
                default:
                    return null;
            }
        }
        #endregion












    }
}
