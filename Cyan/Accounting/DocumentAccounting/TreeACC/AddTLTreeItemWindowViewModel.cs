using AutoMapper;
using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using Saina.WPF.BasicInformation.Accounts.TLAccount;
using Saina.WPF.BasicInformation.Settings.AccountingSetting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace Saina.WPF.Accounting.DocumentAccounting.TreeACC
{
    public class AddTLTreeItemWindowViewModel : BindableBase
    {
        #region Constructors
        public AddTLTreeItemWindowViewModel(ITLsService tLsService, IGLsService gLsService, ISystemAccountingSettingsService systemAccountingSettingsService)
        {
            _tLsService = tLsService;
            _gLsService = gLsService;
            //   CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            GLsDropDownOpenedCommand = new RelayCommand(OnGLsDropDownOpened, () => GLs != null && GLs.Any());
            _systemAccountingSettingsService = systemAccountingSettingsService;
            //TL = new TL();
        }
        #endregion
        #region Fields
        private ITLsService _tLsService;
        private ISystemAccountingSettingsService _systemAccountingSettingsService;
        private List<GL> _allGLs;
        private IGLsService _gLsService;
        //  private TL _editingTL = null;
        #endregion
        #region Commands
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand GLsDropDownOpenedCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        #endregion
        #region Properties
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }

        private GL _selectedGL;

        public GL SelectedGL
        {
            get { return _selectedGL; }
            set
            {

                SetProperty(ref _selectedGL, value);
            }
        }

        private TL _TL;
        public TL TL
        {
            get { return _TL; }
            set
            {
                SetProperty(ref _TL, value);
                if (TL != null)
                {
                    TL.PropertyChanged += TL_PropertyChanged;
                }
            }
        }

        private void TL_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "GL")
            {
                var SystemAccountingSettingModel = AutoMapper.Mapper.Map<SystemAccountingSettingModel, EditableSystemAccountingSettingModel>(_systemAccountingSettingsService.GetSystemAccountingSettingModel());
                var SystemAccountingSettingModelTLLength = int.Parse(SystemAccountingSettingModel.TLLength);
                var SystemAccountingSettingModelGLLength = int.Parse(SystemAccountingSettingModel.GLLength);
                var len = SystemAccountingSettingModelTLLength - SystemAccountingSettingModelGLLength;
                long x = 1;
                long r = 0;
                for (int i = 1; i <= len; i++)
                {
                    r += x * 9;
                    x *= 10;
                }
                r += (TL.GL.GLCode) * x;

                //  var selected = TL.GL.GLCode;
                string s = "0," + (SystemAccountingSettingModelTLLength - SystemAccountingSettingModelGLLength).ToString();
                //var lenght=(TL.GL.GLCode).ToString() + l.ToString();
                var lastGLCode = _tLsService.GetLastTLCode(TL.GL.GLId);
                if (lastGLCode == TL.GL.GLId)
                {
                    var stringLastGLCode = "1";
                    var lastTLCode = stringLastGLCode.ToString().PadLeft(SystemAccountingSettingModelTLLength - SystemAccountingSettingModelGLLength, '0');
                    TL.TLCode = int.Parse($"{TL.GL.GLCode}{lastTLCode}");

                    TL.Regex = $"^{TL.GL.GLCode}[0-9]{{{s}}}$";
                    TL.TLCodeEmptyValue = TL.GL.GLCode;
                }
                else
                {

                    lastGLCode++;
                    //var lastTLLenght = (lastGLCode.ToString()).Length;
                    if (lastGLCode < r)
                    {


                        var stringLastGLCode = lastGLCode.ToString();
                        var lastTLCode = stringLastGLCode.ToString().PadLeft(SystemAccountingSettingModelTLLength - SystemAccountingSettingModelGLLength, '0');
                        TL.TLCode = int.Parse($"{lastTLCode}");

                        TL.Regex = $"^{TL.GL.GLCode}[0-9]{{{s}}}$";
                        TL.TLCodeEmptyValue = TL.GL.GLCode;
                    }
                    else
                    {
                        DialogParameters parameters = new DialogParameters();
                        parameters.OkButtonContent = "بستن";
                        parameters.Header = "اخطار";
                        parameters.Content = " شماره گذاری این حساب به پایان رسیده است";
                        RadWindow.Alert(parameters);
                    }
                }

            }
        }

        private ObservableCollection<GL> _gLs;
        public ObservableCollection<GL> GLs
        {
            get { return _gLs; }
            set { SetProperty(ref _gLs, value); }
        }
        public event Action<Exception> Failed;
        public event Action<TL> SaveClicked;
        public TL DataItem { get; set; }
        #endregion
        #region Methode
        private async void OnGLsDropDownOpened()
        {
            _allGLs = await _gLsService.GetGLsActiveAsync();
            GLs = new ObservableCollection<GL>(_allGLs);
        }
        public async void LoadGLs()
        {
            //var SystemAccountingSettingModel = AutoMapper.Mapper.Map<SystemAccountingSettingModel, EditableSystemAccountingSettingModel>(_systemAccountingSettingsService.GetSystemAccountingSettingModel());
            //var SystemAccountingSettingModelTLLength = int.Parse(SystemAccountingSettingModel.TLLength);
            //var SystemAccountingSettingModelGLLength = int.Parse(SystemAccountingSettingModel.GLLength);
            //var len = SystemAccountingSettingModelTLLength - SystemAccountingSettingModelGLLength;
           
            //string s = "0," + (SystemAccountingSettingModelTLLength - SystemAccountingSettingModelGLLength).ToString();
            ////var lenght=(SelectedGL.GLCode).ToString() + l.ToString();
            //var lastGLCode = _tLsService.GetLastTLCode(SelectedGL.GLId);
            //if (lastGLCode == SelectedGL.GLId)
            //{
            //    var stringLastGLCode = "1";
            //    var lastTLCode = stringLastGLCode.ToString().PadLeft(SystemAccountingSettingModelTLLength - SystemAccountingSettingModelGLLength, '0');
            //    TL.TLCode = int.Parse($"{SelectedGL.GLCode}{lastTLCode}");

            //    TL.Regex = $"^{SelectedGL.GLCode}[0-9]{{{s}}}$";
            //    TL.TLCodeEmptyValue = SelectedGL.GLCode;
            //}
            //else
            //{

            //    lastGLCode++;
            //    //var lastTLLenght = (lastGLCode.ToString()).Length;
            //    if (lastGLCode < r)
            //    {


            //        var stringLastGLCode = lastGLCode.ToString();
            //        var lastTLCode = stringLastGLCode.ToString().PadLeft(SystemAccountingSettingModelTLLength - SystemAccountingSettingModelGLLength, '0');
            //        TL.TLCode = int.Parse($"{lastTLCode}");

            //        TL.Regex = $"^{SelectedGL.GLCode}[0-9]{{{s}}}$";
            //        TL.TLCodeEmptyValue = SelectedGL.GLCode;
            //    }
            //    else
            //    {
            //        DialogParameters parameters = new DialogParameters();
            //        parameters.OkButtonContent = "بستن";
            //        parameters.Header = "اخطار";
            //        parameters.Content = " شماره گذاری این حساب به پایان رسیده است";
            //        RadWindow.Alert(parameters);
            //    }
            //}
        }
        public override void UnLoaded()
        {
            GLs = null;

        }
        public void SetTL(TL tL)
        {


        }
        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private async void OnSave()
        {
            string errors = "";
            //   var editingGL = Mapper.Map<EditableGL, GL>(GL);
            if (TL.TLId == 0)
            {


                if (TL.Title == null || TL.Title == "")
                {

                    errors += "عنوان خالی می باشد" + Environment.NewLine;

                }
                if (TL.TLCode == 0)
                {
                    errors += "کد حساب اشتباه می باشد" + Environment.NewLine;

                }
                if (await _tLsService.HasTitleTree(TL.Title, TL.TLId))
                    errors += ("عنوان نباید تکراری باشد") + Environment.NewLine; ;
                if (await _tLsService.HasduplicateTree(TL.TLCode, TL.TLId))
                    errors += ("کد  حساب نباید تکراری باشد") + Environment.NewLine;
            }
            else
            {
                if (TL.Title == null || TL.Title == "")
                {

                    errors += "عنوان خالی می باشد" + Environment.NewLine;

                }
                if (TL.TLCode == 0)
                {
                    errors += "کد حساب اشتباه می باشد" + Environment.NewLine;

                }
                if (await _tLsService.HasTitleTree(TL.Title, TL.TLId))
                    errors += ("عنوان نباید تکراری باشد") + Environment.NewLine; ;
                if (await _tLsService.HasduplicateTree(TL.TLCode, TL.TLId))
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
                    SaveClicked(TL);
                }
                catch (Exception ex)
                {
                    Failed(ex);
                }
                finally
                {
                    TL = null;
                }

            }

        }
        private bool CanSave()
        {
            return true;
        }

        #endregion
    }
}
