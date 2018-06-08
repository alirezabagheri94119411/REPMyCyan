using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saina.ApplicationCore.DTOs;
using System.Windows.Input;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using System.Windows;
using Saina.WPF.BasicInformation.Settings.AccountingSetting;
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Telerik.Windows.Controls;
using Saina.Data.Context;
using System.ComponentModel;
using Telerik.Windows.Data;
using System.Data.Entity;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.WPF.Accounting.DocumentAccounting.ReviewAcc;

namespace Saina.WPF.BasicInformation.Accounts.SLAccount
{ /// <summary>
  /// لیست حساب معین
  /// </summary>
    public class SLListViewModel : BindableBase
    {
        #region Constructors
        public SLListViewModel( ISystemAccountingSettingsService systemAccountingSettingsService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();

            _systemAccountingSettingsService = systemAccountingSettingsService;
            AddSLCommand = new RelayCommand(OnAddSL);
            AddSLStandardDescriptionCommand = new RelayCommand<SL>(OnAddSLStandardDescription);
            TLsDropDownOpenedCommand = new RelayCommand(OnTLsDropDownOpened, () => TLs != null && TLs.Any());
            SL = new SL();
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();
        }
        #endregion
        #region Fields
        private List<TL> _allTLs;
        ISystemAccountingSettingsService _systemAccountingSettingsService;
        public CompanyInformationModel CompanyInformationModel { get; set; }
        private ICompanyInformationsService _companyInformationsService;
        #endregion
        #region Commands
        public ICommand AddSLCommand { get; private set; }
        public ICommand AddSLStandardDescriptionCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public RelayCommand TLsDropDownOpenedCommand { get; private set; }
        private SainaDbContext _uow;
        #endregion
        #region Properties
        private bool _enable;
        public bool Enable
        {
            get { return _enable; }
            set { SetProperty(ref _enable, value); }
        }

        private SL _SL;
        public SL SL
        {
            get { return _SL; }
            set
            {
                SetProperty(ref _SL, value);

            }
        }

        private AccessUtility _accessUtility;
        private TL _tL;
        public TL TL
        {
            get { return _tL; }
            set { SetProperty(ref _tL, value); }
        }

        private ObservableCollection<SLStandardDescription> _sLStandardDescriptions;
        public ObservableCollection<SLStandardDescription> SLStandardDescriptions
        {
            get { return _sLStandardDescriptions; }
            set { SetProperty(ref _sLStandardDescriptions, value); }
        }
        private ObservableCollection<Company> _companies;

        public ObservableCollection<Company> Companies
        {
            get { return _companies; }
            set { SetProperty(ref _companies, value); }
        }

        private ICollectionView _sLs;
        public ICollectionView SLs
        {
            get { return _sLs; }
            set
            {
                SetProperty(ref _sLs, value); if (_sLs == null) return;
            }
        }
        private string _Regex;
        public string Regex
        {
            get { return _Regex; }
            set { SetProperty(ref _Regex, value); }
        }
        private long _SLCodeEmptyValue;
        public long SLCodeEmptyValue
        {
            get { return _SLCodeEmptyValue; }
            set { SetProperty(ref _SLCodeEmptyValue, value); }
        }
        private void SLListViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName=="TL")
            {
                SL = (SL)SLs.CurrentItem;
                if (((SL)SLs.CurrentItem)?.TL  is TL SelectedTL)
                {
                    var SystemAccountingSettingModel = AutoMapper.Mapper.Map<SystemAccountingSettingModel, EditableSystemAccountingSettingModel>(_systemAccountingSettingsService.GetSystemAccountingSettingModel());
                    int.TryParse(SystemAccountingSettingModel.SLLength, out int SystemAccountingSettingModelSLLength);
                    int.TryParse(SystemAccountingSettingModel.TLLength, out int SystemAccountingSettingModelTLLength);
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
                    long lastTLCode = 0;
                    if (!_uow.SLs.Any(z => z.TLId == SelectedTL.TLId))
                        lastTLCode= SelectedTL.TLCode;
                    else
                    {

                    lastTLCode= _uow.SLs.Where(z => z.TLId == SelectedTL.TLId).Max(z => z.SLCode);
                    }
                    if (lastTLCode == SelectedTL.TLCode)
                    {
                        var stringLastTLCode = "1";
                        var lastSLCode = stringLastTLCode.ToString().PadLeft(SystemAccountingSettingModelSLLength - SystemAccountingSettingModelTLLength, '0');
                        SL.SLCode = int.Parse($"{SelectedTL.TLCode}{lastSLCode}");

                        Regex = $"^{SelectedTL.TLCode}[0-9]{{{s}}}$";
                        SLCodeEmptyValue = SelectedTL.TLCode;
                        Enable = true;

                    }
                    else
                    {

                        lastTLCode++;
                        if (lastTLCode < r)
                        {


                            var stringLastTLCode = lastTLCode.ToString();
                            var lastSLCode = stringLastTLCode.ToString().PadLeft(SystemAccountingSettingModelSLLength - SystemAccountingSettingModelTLLength, '0');
                            SL.SLCode = int.Parse($"{lastTLCode}");

                            Regex = $"^{SelectedTL.TLCode}[0-9]{{{s}}}$";
                            SLCodeEmptyValue = SelectedTL.TLCode;
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

        private ObservableCollection<TL> _tls;

        public ObservableCollection<TL> TLs
        {
            get { return _tls; }
            set
            {
                SetProperty(ref _tls, value);

            }
        }
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
        public event Action<SL> AddSLRequested;
        public event Action<SL> EditSLRequested;
        public event Action<SL> AddSLStandardDescriptionRequested;
        public event Action<string> Error;
        #endregion
        #region Methode
    
        public async void OnTLsDropDownOpened()
        {
          
        }
        public  void LoadSLs()
        {
            _uow = new SainaDbContext();


            _allTLs = _uow.TLs.Where(x => x.Status == true).ToList();
            TLs = new ObservableCollection<TL>(_allTLs);
            var SystemAccountingSettingModel = AutoMapper.Mapper.Map<SystemAccountingSettingModel, EditableSystemAccountingSettingModel>(_systemAccountingSettingsService.GetSystemAccountingSettingModel());
            int.TryParse(SystemAccountingSettingModel.SLLength, out int SystemAccountingSettingModelSLLength);
          
            if (SystemAccountingSettingModelSLLength == 0)
            {
                Error(".تنظیمات حسابداری انجام نشده است");
                EnableTab = false;
            }
          
            else
            {
                EnableTab = true;
               var temp = _uow.SLs.Include(y => y.TL).ToList();
                foreach (var item in temp)
                {
                    item.PropertyChanged += SLListViewModel_PropertyChanged;
                }
                SLs = new QueryableCollectionView(temp);
                SLs.CollectionChanged += SLs_CollectionChanged;

            }

        }

        private void SLs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
          if(e.Action== System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                e.NewItems.Cast<SL>().First().PropertyChanged += SLListViewModel_PropertyChanged;
            }
        }

        private  void OnAddSL()
        {
            _allTLs = _uow.TLs.Where(x => x.Status == true).ToList();
            TLs = new ObservableCollection<TL>(_allTLs);
            if (TLs?.Any() != true)
            {
                Error(".کد کل ثبت نشده است");
                EnableSave = false;
                EnableTab = false;
            }
            else
            {
                EnableSave = true;
                EnableTab = true;

            }

        }
       
        private void OnAddSLStandardDescription(SL sLStandardDescription)
        {
            AddSLStandardDescriptionRequested(sLStandardDescription);
        }
        public void Reject()
        {
            _uow.RejectChanges();
        }
        private bool _Code;
        public bool Code
        {
            get { return _Code; }
            set { SetProperty(ref _Code, value); }
        }

        public bool HasSL(int id)
        {
            var cc = _uow.AccDocumentItems.Any(x => x.SLId == id);
            return cc;
        }
        #endregion
        #region CodeBehind
        public void AddSL(SL sL)
        {
            _uow.SLs.Add(sL);
        }
        public int DeleteSL(SL sL)
        {
            if (HasItem(sL.SLId) == false)
            {

                _uow.SLs.Attach(sL);
                _uow.SLs.Remove(sL);
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
        public bool HasItem(int id)
        {
            return  _uow.AccDocumentItems.Any(x => x.SLId == id);
            
        }
    
       
        #endregion
    }
}


