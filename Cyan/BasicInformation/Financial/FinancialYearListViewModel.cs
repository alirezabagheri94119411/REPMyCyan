using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.Data.Services.BasicInformation;
using Saina.WPF.Behaviors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace Saina.WPF.BasicInformation.Financial
{
    /// <summary>
    /// لیست سال مالی
    /// </summary>
    class FinancialYearListViewModel : BindableBase
    {
        #region Constructors
        public FinancialYearListViewModel(IFinancialYearsService FinancialYearsService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _FinancialYearsService = FinancialYearsService;
            AddFinancialYearCommand = new RelayCommand(OnAddFinancialYear);
            EditFinancialYearCommand = new RelayCommand<FinancialYear>(OnEditFinancialYear);
            DeleteCommand = new RelayCommand<FinancialYear>(OnDeleteFinancialYear);
            

             _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();
        }
        #endregion
        #region Fields
        private IFinancialYearsService _FinancialYearsService;
        private List<FinancialYear> _allFinancialYears;
        public CompanyInformationModel CompanyInformationModel { get; set; }

        private IAppContextService _appContextService;
        private ICompanyInformationsService _companyInformationsService;
        #endregion
        #region Commands
        public ICommand AddFinancialYearCommand { get; private set; }
        public ICommand EditFinancialYearCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        private AccessUtility _accessUtility;
        #endregion
        #region Properties
        private ObservableCollection<FinancialYear> _FinancialYears;
        public ObservableCollection<FinancialYear> FinancialYears
        {
            get { return _FinancialYears; }
            set { SetProperty(ref _FinancialYears, value); }
        }
        public event Action<FinancialYear> AddFinancialYearRequested;
        public event Action<FinancialYear> EditFinancialYearRequested;
        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<Exception> Failed;

        #endregion
        #region Methode
        public async void LoadFinancialYears()
        {
            var Years = await _FinancialYearsService.GetFinancialYearsAsync();
            FinancialYears = new ObservableCollection<FinancialYear>(Years);

        }

        private void OnAddFinancialYear()
        {
            if (_accessUtility.HasAccess(40))
            {
                AddFinancialYearRequested(new FinancialYear());
            }
        }
        private void OnEditFinancialYear(FinancialYear FinancialYear)
        {
     
            if (_accessUtility.HasAccess(41))
            {

                EditFinancialYearRequested(FinancialYear);
            }

        }

      

        private async void OnDeleteFinancialYear(FinancialYear FinancialYear)
        {
            if (_accessUtility.HasAccess(42))
            {
                if (Deleting())
                {
                    try
                    {
                        await _FinancialYearsService.DeleteFinancialYearAsync(FinancialYear.FinancialYearId);
                        FinancialYears.Remove(FinancialYear);
                        Deleted();
                    }
                    catch (Exception ex)
                    {
                        Failed(ex);
                    }

                }
            }

        }
        #endregion

    }
}
