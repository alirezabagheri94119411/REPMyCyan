using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using Saina.WPF.BasicInformation.Settings.AccountingSetting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Saina.ApplicationCore.DTOs;
using Telerik.Windows.Controls;
using Saina.Data.Context;
using System.ComponentModel;
using Telerik.Windows.Data;
using System.Data.Entity;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.WPF.Accounting.DocumentAccounting.ReviewAcc;

namespace Saina.WPF.BasicInformation.Accounts.GLAccount
{
    /// <summary>
    /// لیست حساب گروه
    /// </summary>
    public class GLListViewModel : BindableBase
    {
        #region Constructors
        public GLListViewModel(ISystemAccountingSettingsService systemAccountingSettingsService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _systemAccountingSettingsService = systemAccountingSettingsService;
            AddGLCommand = new RelayCommand(OnAddGL);
            GL = new GL();
        }
        #endregion
        #region Fields
        ISystemAccountingSettingsService _systemAccountingSettingsService;
        private ICompanyInformationsService _companyInformationsService;

        private List<GL> _allGLs;
        private SainaDbContext _uow;
        #endregion
        #region Commands
        public ICommand AddGLCommand { get; private set; }
        public RelayCommand GetTLCommand { get; }
        #endregion
        #region Properties
        private ICollectionView _gLs;
        public ICollectionView GLs
        {
            get { return _gLs; }
            set
            {
                SetProperty(ref _gLs, value); if (GLs == null) return;
                GLs.CollectionChanged += GLs_CollectionChanged;
            }
        }
        public CompanyInformationModel CompanyInformationModel { get; set; }

        private EditableSystemAccountingSettingModel _SystemAccountingSettingModel;
        public EditableSystemAccountingSettingModel SystemAccountingSettingModel
        {
            get { return _SystemAccountingSettingModel; }
            set { SetProperty(ref _SystemAccountingSettingModel, value); }
        }

        private ObservableCollection<TL> _tLs;
        public ObservableCollection<TL> TLs
        {
            get { return _tLs; }
            set { SetProperty(ref _tLs, value); }
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

        private AccessUtility _accessUtility;
        private bool _EnableTab;
        public bool EnableTab
        {
            get { return _EnableTab; }
            set { SetProperty(ref _EnableTab, value); }
        }
        private bool _EnableSave;
        public bool EnableSave
        {
            get { return _EnableSave; }
            set { SetProperty(ref _EnableSave, value); }
        }
        private long _LastGLCode;
        public long LastGLCode
        {
            get { return _LastGLCode; }
            set { SetProperty(ref _LastGLCode, value); }
        }
        private int _LastGLLenght;
        public int LastGLLenght
        {
            get { return _LastGLLenght; }
            set { SetProperty(ref _LastGLLenght, value); }
        }

        private string _Sum;
        public string Sum
        {
            get { return _Sum; }
            set { SetProperty(ref _Sum, value); }
        }
        private string _Regex;
        public string Regex
        {
            get { return _Regex; }
            set { SetProperty(ref _Regex, value); }
        }
        private long _GLCodeEmptyValue;
        public long GLCodeEmptyValue
        {
            get { return _GLCodeEmptyValue; }
            set { SetProperty(ref _GLCodeEmptyValue, value); }
        }
        private bool _Code;
        public bool Code
        {
            get { return _Code; }
            set { SetProperty(ref _Code, value); }
        }

        public event Action<Exception> Failed;
        public event Action<string> Error;

        #endregion
        #region Methode

        public override void UnLoaded()
        {
            //_uow.Dispose();
        }
        public void LoadGLs()
        {
            _uow = new SainaDbContext();
            _allGLs = _uow.Set<GL>().ToList();
            // _allGLs = _uow.Set<GL>().AsNoTracking().ToListAsync().ConfigureAwait(false);
            GLs = new QueryableCollectionView(_uow.GLs.ToList());
            SystemAccountingSettingModel = AutoMapper.Mapper.Map<SystemAccountingSettingModel, EditableSystemAccountingSettingModel>(_systemAccountingSettingsService.GetSystemAccountingSettingModel());

            int.TryParse(SystemAccountingSettingModel.GLLength, out int SystemAccountingSettingModelGLLength);
            if (_uow.GLs.Any())
                LastGLCode = _uow.GLs.Select(x => x.GLCode).Max() + 1;
            else
            {
                LastGLCode = 1;
            }

            LastGLLenght = (LastGLCode.ToString()).Length;
            Sum = "0," + SystemAccountingSettingModelGLLength.ToString();
            //var lastSLCode = stringLastGLCode.ToString().PadLeft(SystemAccountingSettingModelSLLength - SystemAccountingSettingModelTLLength, '0');

            Regex = $"^[0-9]{{{Sum}}}$";
            if (SystemAccountingSettingModelGLLength == 0)
            {
                EnableTab = false;
                Error?.Invoke("تنظیمات حسابداری انجام نشده است");
            }
            else if (SystemAccountingSettingModelGLLength > 0 && LastGLLenght != SystemAccountingSettingModelGLLength)
            {
                EnableSave = false;
                EnableTab = true;
            }
            else
            {
                EnableSave = true;
                EnableTab = true;

            }
        }
        private void OnAddGL()
        {
            int.TryParse(SystemAccountingSettingModel.GLLength, out int SystemAccountingSettingModelGLLength);
            if (LastGLCode == 1)
            {
                var stringLastGLCode = LastGLCode.ToString();
                var lastGLsCode = stringLastGLCode.ToString().PadRight(SystemAccountingSettingModelGLLength, '0');
                if (GLs.CurrentItem is GL gl)
                {
                    gl.GLCode = int.Parse(lastGLsCode);
                }

            }
            else if (LastGLCode != 1)
            {
                //if (GLs.CurrentItem is GL gl)
                //{
                //    gl.GLCode = LastGLCode;
                //}

            }
            GLCodeEmptyValue = 0;

        }

        private void GLs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                int.TryParse(SystemAccountingSettingModel.GLLength, out int SystemAccountingSettingModelGLLength);
                long z = 1;
                long r = 0;
                for (int i = 1; i <= SystemAccountingSettingModelGLLength; i++)
                {
                    r +=z * 9;
                    z *= 10;
                }
                if (!_uow.GLs.Any())
                    LastGLCode =  1;
                else
                {
                    LastGLCode = _uow.GLs.Select(x => x.GLCode).Max() + 1;
                }
                if (LastGLCode == 1)
                {
                    var stringLastGLCode = LastGLCode.ToString();
                    var lastGLsCode = stringLastGLCode.ToString().PadRight(SystemAccountingSettingModelGLLength, '0');
                    ((GL)e.NewItems[0]).GLCode = int.Parse(lastGLsCode);
                    LastGLCode++;
                   // LastGLCode = _uow.GLs.Select(x => x.GLCode).Max() + 1;

                }
                else if (LastGLCode > 1 && LastGLCode<=r)
                {
                    ((GL)e.NewItems[0]).GLCode = LastGLCode;
                    LastGLCode = _uow.GLs.Select(x => x.GLCode).Max() + 1;

                }
                else
                {
                    var manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter, new Point(0, 0), 100);

                    var alert = new RadDesktopAlert
                    {
                        FlowDirection = FlowDirection.RightToLeft,
                        Header = "اطلاعات",
                        Content = ".شماره گذاری حساب گروه به پایان رسیده است",
                        ShowDuration = 1000,
                    };
                    manager.ShowAlert(alert);
                }
                GLCodeEmptyValue = LastGLCode;
            }
        }
        public bool CanAdd()
        {
            int.TryParse(SystemAccountingSettingModel.GLLength, out int SystemAccountingSettingModelGLLength);
            if (SystemAccountingSettingModelGLLength > 0 && LastGLLenght == SystemAccountingSettingModelGLLength)
            {
                return true;
            }
            return false;
        }



        private bool CanSave()
        {
            return !GL.HasErrors;
        }

        #endregion
        #region CodeBehind
        public void AddGL(GL gL)
        {
            _uow.GLs.Add(gL);
        }
        public int DeleteGL(GL gL)
        {
                if (HasTL(gL.GLId) == false)
                {

                    _uow.GLs.Attach(gL);
                    _uow.GLs.Remove(gL);
                    return _uow.SaveChanges();
                }
                else
                {
                 
                    return 0;
                }
          
        }
        public void Save()
        {
            var r = _uow.SaveChanges();
        }
        public bool HasTL(int id)
        {
            var cc = _uow.GLs.FirstOrDefault(x => x.GLId == id)?.TLs;
            return _uow.GLs.FirstOrDefault(x => x.GLId == id)?.TLs?.Any() == true;
        }
        public void Reject()
        {
            _uow.RejectChanges();
        }
        #endregion

    }
}
