using AutoMapper;
using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using Saina.WPF.BasicInformation.Settings.AccountingSetting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Accounts.TLAccount
{
    /// <summary>
    /// افزودن و ویرایش حساب گروه
    /// </summary>
    public class AddEditTLViewModel : BindableBase
    {
        #region Constructors
        public AddEditTLViewModel(ITLsService tLsService, IGLsService gLsService, ISystemAccountingSettingsService systemAccountingSettingsService)
        {
            _tLsService = tLsService;
            _gLsService = gLsService;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            GLsDropDownOpenedCommand = new RelayCommand(OnGLsDropDownOpened, () => GLs != null && GLs.Any());
            _systemAccountingSettingsService = systemAccountingSettingsService;

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

                if (value != null)
                {
                    SetProperty(ref _selectedGL, value);
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
                    r += (SelectedGL.GLCode) * x;
                   
                    //  var selected = SelectedGL.GLCode;
                    string s = "0," + (SystemAccountingSettingModelTLLength - SystemAccountingSettingModelGLLength).ToString();
                    //var lenght=(SelectedGL.GLCode).ToString() + l.ToString();
                    var lastGLCode = _tLsService.GetLastTLCode(SelectedGL.GLId);
                    if (lastGLCode == SelectedGL.GLId)
                    {
                        var stringLastGLCode = "1";
                        var lastTLCode = stringLastGLCode.ToString().PadLeft(SystemAccountingSettingModelTLLength - SystemAccountingSettingModelGLLength, '0');
                        TL.TLCode = int.Parse($"{SelectedGL.GLCode}{lastTLCode}");

                        TL.Regex = $"^{SelectedGL.GLCode}[0-9]{{{s}}}$";
                        TL.TLCodeEmptyValue = SelectedGL.GLCode;
                    }
                    else
                    {

                        lastGLCode++;
                        //var lastTLLenght = (lastGLCode.ToString()).Length;
                        if (lastGLCode<r)
                        {


                            var stringLastGLCode = lastGLCode.ToString();
                            var lastTLCode = stringLastGLCode.ToString().PadLeft(SystemAccountingSettingModelTLLength - SystemAccountingSettingModelGLLength, '0');
                            TL.TLCode = int.Parse($"{lastTLCode}");

                            TL.Regex = $"^{SelectedGL.GLCode}[0-9]{{{s}}}$";
                            TL.TLCodeEmptyValue = SelectedGL.GLCode;
                        }
                        else 
                        {
                            Error?.Invoke("شماره گذاری این حساب  به پایان رسیده است");
                        }
                    }
                }
            }
        }

        private EditableTL _TL;
        public EditableTL TL
        {
            get { return _TL ?? new EditableTL(); }
            set { SetProperty(ref _TL, value); }
        }
        private ObservableCollection<GL> _gLs;
        public ObservableCollection<GL> GLs
        {
            get { return _gLs; }
            set { SetProperty(ref _gLs, value); }
        }
        public event Action<Exception> Failed;
        public event Action<string> Error;
        public event Action Done;
        #endregion
        #region Methode
        private async void OnGLsDropDownOpened()
        {
            _allGLs = await _gLsService.GetGLsActiveAsync();
            GLs = new ObservableCollection<GL>(_allGLs);
        }
        public async void LoadGLs()
        {
            if (GLs == null)
            {
                _allGLs = await _gLsService.GetGLsActiveAsync();
                GLs = new ObservableCollection<GL>(_allGLs);
            }
        }
        public override void UnLoaded()
        {
            GLs = null;

        }
        public void SetTL(TL tL)
        {



            //TL.Text = SelectedGL?.GLCode?.ToString();
            TL = Mapper.Map<TL, EditableTL>(tL);
            TL.ValidationDelegate += TL_ValidationDelegate;

            TL.ErrorsChanged += RaiseCanExecuteChanged;

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
            var editingTL = Mapper.Map<EditableTL, TL>(TL);
            try
            {
                if (EditMode)
                    await _tLsService.UpdateTLAsync(editingTL);
                else
                    await _tLsService.AddTLAsync(editingTL);
                Done?.Invoke();
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

        private bool CanSave()
        {
            return !TL.HasErrors;
        }
        private async Task<List<string>> TL_ValidationDelegate(object sender, string propertyName)
        {
            var tl = (EditableTL)sender;
            List<string> errors = new List<string>();
            switch (propertyName)
            {
                //case nameof(TL.Title):

                //    if (await _tLsService.HasTitle(tl.Title))
                //        errors.Add("عنوان نباید تکراری باشد");
                //    return errors;
                //case nameof(TL.TLCode):
                //    if (await _tLsService.Hasduplicate(tl.TLCode))
                //        errors.Add("کد  کل نباید تکراری باشد");
                //    return errors;
                default:
                    return null;
            }
        }
        #endregion
    }
}
