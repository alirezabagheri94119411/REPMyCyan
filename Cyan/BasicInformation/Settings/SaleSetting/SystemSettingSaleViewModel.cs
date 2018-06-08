using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Setting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saina.WPF.BasicInformation.Settings.SaleSetting
{
    /// <summary>
    /// تنظیمات سیستم فروش
    /// </summary>
    class SystemSettingSaleViewModel:BindableBase
    {
        #region Fields
        private ISystemSettingSalesService _systemSettingSalesService;
        private IAppContextService _appContextService;
        private List<SL> _allSL;
        private List<SLSetting> _allSLs;
        private ISLsService _sLsService;
        #endregion
        #region Constructors

        public SystemSettingSaleViewModel(IAppContextService appContextService, ISystemSettingSalesService systemSettingSalesService, ISLsService sLsService)
        {
            _sLsService = sLsService;
            _appContextService = appContextService;
            _systemSettingSalesService = systemSettingSalesService;
            SystemSettingSaleModel = _systemSettingSalesService.GetSystemSettingSaleModel();
            SLsDropDownOpenedCommand = new RelayCommand(OnSLsDropDownOpened);
            SaveCommand = new RelayCommand(onSave);
        }

        #endregion
        #region Commands
        public RelayCommand SLsDropDownOpenedCommand { get; private set; }

        public ICommand SaveCommand { get; set; }
        #endregion
        #region Properties
        public SystemSettingSaleModel SystemSettingSaleModel { get; set; }
        private ObservableCollection<SLSetting> _sLs;
        public ObservableCollection<SLSetting> SLs
        {
            get { return _sLs; }
            set { SetProperty(ref _sLs, value); }
        }
        public event Action Done;
        #endregion
        #region Methods
        private async void OnSLsDropDownOpened()
        {
            _allSL = await _sLsService.GetSLsActiveAsync();
            SLs = new ObservableCollection<SLSetting>(_allSL.Select(x => new SLSetting { SLId = x.SLId.ToString(), Title = x.Title }));
        }
        public async void LoadSLs()
        {
            if (SLs == null)
            {
                _allSLs = (await _sLsService.GetSLsActiveAsync()).Select(x => new SLSetting { SLId = x.SLId.ToString(), Title = x.Title }).ToList();

                SLs = new ObservableCollection<SLSetting>(_allSLs);
            }
        }
        private void onSave()
        {
            _systemSettingSalesService.SaveSystemSettingSaleModel(SystemSettingSaleModel);
        }

        #endregion
    }
}
