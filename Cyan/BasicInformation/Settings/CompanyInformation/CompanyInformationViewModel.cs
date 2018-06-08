using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.WPF.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saina.WPF.BasicInformation.Settings.CompanyInformation
{
    /// <summary>
    /// تنظیمات اطلاعات شرکت
    /// </summary>
    public class CompanyInformationViewModel:BindableBase
    {
        #region Fields
        private ICompanyInformationsService _companyInformationsService;
        private IAppContextService _appContextService;
        #endregion
        #region Constructors

        public CompanyInformationViewModel(IAppContextService appContextService, ICompanyInformationsService companyInformationsService)
        {
            _appContextService = appContextService;
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            SaveCommand = new RelayCommand(onSave);
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();
        }

        #endregion
        #region Commands
        public ICommand SaveCommand { get; set; }

        private AccessUtility _accessUtility;
        #endregion
        #region Properties
        public CompanyInformationModel CompanyInformationModel { get; set; }
        #endregion
        #region Methods
        public void onSave()
        {
            if (_accessUtility.HasAccess(2))
            {
                _companyInformationsService.SaveCompanyInformationModel(CompanyInformationModel);
            }

        }
        #endregion

    }

}
