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

namespace Saina.WPF.BasicInformation.Settings.SalarySetting
{
    /// <summary>
    /// تنظیمات سیستم حقوق و دستمزد
    /// </summary>
   public class SalarySystemSettingViewModel:BindableBase
    {
        #region Fields
        private ISalarySystemSettingsService _salarySystemSettingsService;
        private IAppContextService _appContextService;
        private List<SLSetting> _allSLs;
        private List<SL> _allSL;
        private ISLsService _sLsService;
        private List<SelectAgentSetting> _allSelectAgents;
        private List<SelectAgent> _allSelectAgent;
        private ISelectAgentsService _selectAgentsService;
        #endregion
        #region Constructors

        public SalarySystemSettingViewModel(IAppContextService appContextService, ISalarySystemSettingsService salarySystemSettingsService, ISLsService sLsService, ISelectAgentsService selectAgentsService)
        {
            _selectAgentsService = selectAgentsService;
            _sLsService = sLsService;
            _appContextService = appContextService;
            _salarySystemSettingsService = salarySystemSettingsService;
            SalarySystemSettingModel = _salarySystemSettingsService.GetSalarySystemSettingModel();
            SLsDropDownOpenedCommand = new RelayCommand(OnSLsDropDownOpened);
            SaveCommand = new RelayCommand(onSave);
        }

        #endregion
        #region Commands
        public ICommand SaveCommand { get; set; }
        public RelayCommand SLsDropDownOpenedCommand { get; private set; }

        #endregion
        #region Properties
        public SalarySystemSettingModel SalarySystemSettingModel { get; set; }
        private ObservableCollection<SLSetting> _sLs;
        public ObservableCollection<SLSetting> SLs
        {
            get { return _sLs; }
            set { SetProperty(ref _sLs, value); }
        }
        private ObservableCollection<SelectAgentSetting> _selectAgents;

        public ObservableCollection<SelectAgentSetting> SelectAgents
        {
            get { return _selectAgents; }
            set { SetProperty(ref _selectAgents, value); }
        }
        public event Action Done;
        #endregion
        #region Methods
        private async void OnSLsDropDownOpened()
        {
            _allSL = await _sLsService.GetSLsActiveAsync();
            SLs = new ObservableCollection<SLSetting>(_allSL.Select(x => new SLSetting { SLId = x.SLId.ToString(), Title = x.Title }));
            _allSelectAgent = await _selectAgentsService.GetSelectAgentsAsync();
            SelectAgents = new ObservableCollection<SelectAgentSetting>(_allSelectAgent.Select(x => new SelectAgentSetting {SelectAgentId = x.SelectAgentId.ToString(), SelectAgentTitle = x.SelectAgentTitle }));
        }
        public async void LoadSLs()
        {
            if (SLs == null)
            {
                _allSLs = (await _sLsService.GetSLsActiveAsync()).Select(x=>new SLSetting { SLId=x.SLId.ToString(),Title=x.Title }).ToList();
                SLs = new ObservableCollection<SLSetting>(_allSLs);
            }
            if (SelectAgents == null)
            {
                _allSelectAgents = (await _selectAgentsService.GetSelectAgentsAsync()).Select(x => new SelectAgentSetting { SelectAgentId = x.SelectAgentId.ToString(), SelectAgentTitle = x.SelectAgentTitle }).ToList();
                SelectAgents = new ObservableCollection<SelectAgentSetting>(_allSelectAgents);
            }
        }
        private void onSave()
        {
            _salarySystemSettingsService.SaveSalarySystemSettingModel(SalarySystemSettingModel);
        }


        #endregion
    }

    
}
