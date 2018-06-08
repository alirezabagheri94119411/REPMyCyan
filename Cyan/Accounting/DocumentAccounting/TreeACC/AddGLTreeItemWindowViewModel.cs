using AutoMapper;
using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using Saina.WPF.BasicInformation.Accounts.GLAccount;
using Saina.WPF.BasicInformation.Settings.AccountingSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace Saina.WPF.Accounting.DocumentAccounting.TreeACC
{
    public class AddGLTreeItemWindowViewModel : BindableBase
    {
        #region Constructors
        public AddGLTreeItemWindowViewModel(IGLsService gLsService, ISystemAccountingSettingsService systemAccountingSettingsService)
        {
            _systemAccountingSettingsService = systemAccountingSettingsService;

            _gLsService = gLsService;

            //  CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            GL = new GL();
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
        public RelayCommand AccountGroupsDropDownOpenedCommand { get; private set; }
        #endregion
        #region Properties
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }
        private GL _GL;
        public GL GL
        {
            get { return _GL; }
            set
            {
                SetProperty(ref _GL, value);

            }
        }

        public event Action<GL> SaveClicked;
        //  public GL DataItem { get; set; }
        public event Action<Exception> Failed;

        #endregion
        #region Methode


        public void SetGL(GL gL)
        {
            //  GL = Mapper.Map<GL, EditableGL>(gL);
            //  GL.ValidationDelegate += GL_ValidationDelegate;
            //  GL.ErrorsChanged += RaiseCanExecuteChanged;
        }
        public void LoadGLs()
        {
            var SystemAccountingSettingModel = AutoMapper.Mapper.Map<SystemAccountingSettingModel, EditableSystemAccountingSettingModel>(_systemAccountingSettingsService.GetSystemAccountingSettingModel());
            var SystemAccountingSettingModelGLLength = int.Parse(SystemAccountingSettingModel.GLLength);

            string s = "0," + SystemAccountingSettingModelGLLength.ToString();
            long lastGLCode = _gLsService.GetLastGLCode();
            if (GL.GLId == 0)
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
            // GL.GLCodeEmptyValue = 0;

        }

        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }
        //private void OnCancel()
        //{
        //    Done?.Invoke();
        //}
        private async void OnSave()
        {
            string errors = "";

            if (GL.GLId == 0)
            {


                //   var editingGL = Mapper.Map<EditableGL, GL>(GL);
                if (GL.Title == null || GL.Title == "")
                {

                    errors += "عنوان خالی می باشد" + Environment.NewLine;

                }
                if (GL.GLCode == 0)
                {
                    errors += "کد حساب اشتباه می باشد" + Environment.NewLine;

                }
                if ( _gLsService.HasTitle(GL.Title, GL.GLId))
                    errors += ("عنوان نباید تکراری باشد") + Environment.NewLine; ;
                if ( _gLsService.Hasduplicate(GL.GLCode, GL.GLId))
                    errors += ("کد  حساب نباید تکراری باشد") + Environment.NewLine;
            }
            else
            {
                if (GL.Title == null || GL.Title == "")
                {

                    errors += "عنوان خالی می باشد" + Environment.NewLine;

                }
                if (GL.GLCode == 0)
                {
                    errors += "کد حساب اشتباه می باشد" + Environment.NewLine;

                }
                if (await _gLsService.HasTitleTree(GL.Title, GL.GLId))
                    errors += ("عنوان نباید تکراری باشد") + Environment.NewLine; ;
                if (await _gLsService.HasduplicateTree(GL.GLCode, GL.GLId))
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
                    SaveClicked(GL);
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
        }

        private bool CanSave()
        {
            return !GL.HasErrors;
        }
        //private async Task<List<string>> GL_ValidationDelegate(object sender, string propertyName)
        //{
        //    var gl = (GL)sender;
        //    List<string> errors = new List<string>();
        //    switch (propertyName)
        //    {
        //        case nameof(GL.Title):

        //            if (await _gLsService.HasTitle(gl.Title))
        //                errors.Add("عنوان نباید تکراری باشد");
        //            return errors;
        //        case nameof(GL.GLCode):
        //            if (await _gLsService.Hasduplicate(gl.GLCode))
        //                errors.Add("کد  حساب نباید تکراری باشد");
        //            return errors;
        //        default:
        //            return null;
        //    }
        //}
        #endregion
    }
}
