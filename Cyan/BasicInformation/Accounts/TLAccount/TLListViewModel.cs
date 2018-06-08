using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using System.Windows;
using Saina.WPF.BasicInformation.Settings.AccountingSetting;
using Telerik.Windows.Controls;
using Saina.Data.Context;
using Telerik.Windows.Data;
using System.ComponentModel;
using System.Data.Entity;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.WPF.Accounting.DocumentAccounting.ReviewAcc;

namespace Saina.WPF.BasicInformation.Accounts.TLAccount
{
    /// <summary>
    /// لیست حساب کل
    /// </summary>
    public class TLListViewModel : BindableBase
    {
        #region Constructors
        public TLListViewModel( ISystemAccountingSettingsService systemAccountingSettingsService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _systemAccountingSettingsService = systemAccountingSettingsService;
            AddTLCommand = new RelayCommand(OnAddTL);
            GLsDropDownOpenedCommand = new RelayCommand(OnGLsDropDownOpened, () => GLs != null && GLs.Any());
            TL = new TL();
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();
        }
        #endregion
        #region Fields
        ISystemAccountingSettingsService _systemAccountingSettingsService;
        private List<GL> _allGLs;
        public CompanyInformationModel CompanyInformationModel { get; set; }
        private ICompanyInformationsService _companyInformationsService;
        #endregion
        #region Commands
        public ICommand AddTLCommand { get; private set; }
        public ICommand EditTLCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public RelayCommand GLsDropDownOpenedCommand { get; }

        private SainaDbContext _uow;
        #endregion
        #region Properties
        private GL _gL;
        public GL GL
        {
            get { return _gL; }
            set { SetProperty(ref _gL, value); }
        }
        private TL _TL;
        public TL TL
        {
            get { return _TL; }
            set { SetProperty(ref _TL, value); }
        }

        private AccessUtility _accessUtility;
        private ObservableCollection<GL> _gLs;
        public ObservableCollection<GL> GLs
        {
            get { return _gLs; }
            set { SetProperty(ref _gLs, value); }
        }
        private ICollectionView _tLs;
        public ICollectionView TLs
        {
            get { return _tLs; }
            set
            {
                SetProperty(ref _tLs, value); if (_tLs == null) return;
            }
        }
        private string _Regex;
        public string Regex
        {
            get { return _Regex; }
            set { SetProperty(ref _Regex, value); }
        }
        private bool _Code;
        public bool Code
        {
            get { return _Code; }
            set { SetProperty(ref _Code, value); }
        }
        private long _TLCodeEmptyValue;
        public long TLCodeEmptyValue
        {
            get { return _TLCodeEmptyValue; }
            set { SetProperty(ref _TLCodeEmptyValue, value); }
        }

        private void TLListViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "GL")
            {
                TL = (TL)TLs.CurrentItem;
                if (((TL)TLs.CurrentItem)?.GL is GL SelectedGL)
                {
                    var SystemAccountingSettingModel = AutoMapper.Mapper.Map<SystemAccountingSettingModel, EditableSystemAccountingSettingModel>(_systemAccountingSettingsService.GetSystemAccountingSettingModel());
                    int.TryParse(SystemAccountingSettingModel.TLLength, out int SystemAccountingSettingModelTLLength);
                    int.TryParse(SystemAccountingSettingModel.GLLength, out int SystemAccountingSettingModelGLLength);
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
                    long lastGLCode =0;
                    if (!_uow.TLs.Any(z => z.GLId == SelectedGL.GLId))
                        lastGLCode= SelectedGL.GLCode;
                    else
                    {
                    lastGLCode= _uow.TLs.Where(z => z.GLId == SelectedGL.GLId).Max(z => z.TLCode);
                    }
                    if (lastGLCode == SelectedGL.GLCode)
                    {
                        var stringLastGLCode = "1";
                        var lastTLCode = stringLastGLCode.ToString().PadLeft(SystemAccountingSettingModelTLLength - SystemAccountingSettingModelGLLength, '0');
                        TL.TLCode = int.Parse($"{SelectedGL.GLCode}{lastTLCode}");

                        Regex = $"^{SelectedGL.GLCode}[0-9]{{{s}}}$";
                        TLCodeEmptyValue = SelectedGL.GLCode;
                        Enable = true;
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

                            Regex = $"^{SelectedGL.GLCode}[0-9]{{{s}}}$";
                            TLCodeEmptyValue = SelectedGL.GLCode;
                            Enable = true;
                        }
                        else
                        {
                            Error?.Invoke("شماره گذاری این حساب  به پایان رسیده است");
                        }
                    }
                }


            }
        }

        private ObservableCollection<SL> _sLs;
        public ObservableCollection<SL> SLs
        {
            get { return _sLs; }
            set { SetProperty(ref _sLs, value); }
        }
        private bool _enable;
        public bool Enable
        {
            get { return _enable; }
            set { SetProperty(ref _enable, value); }
        }
        public event Action<TL> EditTLRequested;

        public event Action<string> Error;
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

        #endregion
        #region Methode
        public  void OnGLsDropDownOpened()
        {
            _allGLs = _uow.GLs.Where(x => x.Status == true).ToList();
            GLs = new ObservableCollection<GL>(_allGLs);
        }
        public  void LoadTLs()
        {
            _uow = new SainaDbContext();

            _allGLs = _uow.GLs.Where(x => x.Status == true).ToList();
            GLs = new ObservableCollection<GL>(_allGLs);
            var SystemAccountingSettingModel = AutoMapper.Mapper.Map<SystemAccountingSettingModel, EditableSystemAccountingSettingModel>(_systemAccountingSettingsService.GetSystemAccountingSettingModel());
            int.TryParse(SystemAccountingSettingModel.TLLength, out int SystemAccountingSettingModelTLLength);
            if (SystemAccountingSettingModelTLLength == 0)
            {
                Error?.Invoke(".تنظیمات حسابداری انجام نشده است");
                EnableTab = false;
            }

            else
            {
                EnableTab = true;
                var temp = _uow.TLs.Include(y => y.GL).ToList();
                foreach (var item in temp)
                {
                    item.PropertyChanged += TLListViewModel_PropertyChanged;
                }
                TLs = new QueryableCollectionView(temp);
                TLs.CollectionChanged += TLs_CollectionChanged;
            }
        }
        private void TLs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                e.NewItems.Cast<TL>().First().PropertyChanged += TLListViewModel_PropertyChanged;
            }
        }
        private void OnAddTL()
        {
            _allGLs = _uow.GLs.Where(x => x.Status == true).ToList();
            GLs = new ObservableCollection<GL>(_allGLs);
            if (GLs?.Any() != true)
            {
                Error(".کد گروه ثبت نشده است");
                EnableSave = false;
                EnableTab = false;
            }
            else
            {
                EnableSave = true;
                EnableTab = true;
            }
        }
        #endregion
        #region CodeBehind
        public void AddTL(TL tL)
        {
            _uow.TLs.Add(tL);
        }
       
        public void Save()
        {
            var r = _uow.SaveChanges();
        }
        public bool HasSL(int id)
        {
            var cc = _uow.TLs.FirstOrDefault(x => x.TLId == id)?.SLs;
            return _uow.TLs.FirstOrDefault(x => x.TLId == id)?.SLs?.Any() == true;
        }
        public int DeleteTL(TL tL)
        {
            if (HasSL(tL.TLId) == false)
            {
                _uow.TLs.Attach(tL);
            _uow.TLs.Remove(tL);
            return _uow.SaveChanges();
            }
            else
            {

                return 0;
            }
        }
        public void Reject()
        {
            _uow.RejectChanges();
        }
        #endregion
    }
}
