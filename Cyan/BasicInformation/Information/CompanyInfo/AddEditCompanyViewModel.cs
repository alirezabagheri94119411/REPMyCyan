using AutoMapper;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.IServices.BasicInformation.Information;
using Saina.WPF.BasicInformation.Information.Related;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Information.CompanyInfo
{
    /// <summary>
    /// افزودن و ویرایش شرکت ها
    /// </summary>
    class AddEditCompanyViewModel:BindableBase
    {
        #region Constructors
        public AddEditCompanyViewModel(ICompaniesService companiesService, RelatedPersonListViewModel relatedPersonListViewModel)
        {
           
            _companiesService = companiesService;
           
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            RelatedPersonListViewModel = relatedPersonListViewModel;
        }
        #endregion
        #region Fields
        private ICompaniesService _companiesService;
        private Company _editingCompany = null;
        #endregion
        #region Commands
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        #endregion
        #region Properties
       
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }
        private RelatedPersonListViewModel _relatedPersonListViewModel;

        public RelatedPersonListViewModel RelatedPersonListViewModel
        {
            get { return _relatedPersonListViewModel; }
            set { SetProperty(ref _relatedPersonListViewModel, value); }
        }
        private EditableCompany _Company;
        public EditableCompany Company
        {
            get { return _Company; }
            set { SetProperty(ref _Company, value); }
        }
        private DL _DL;
        public DL DL
        {
            get { return _DL; }
            set { SetProperty(ref _DL, value); }
        }
        public event Action Done;
        public event Action<Exception> Failed;

        #endregion
        #region Methode

        public void SetCompany(Company company)
        {
            if (EditMode == false)
                RelatedPersonListViewModel.RelatedPeople = new ObservableCollection<RelatedPerson>();
            Company = Mapper.Map<Company, EditableCompany>(company);
            Company.ErrorsChanged += RaiseCanExecuteChanged;
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
            var editingCompany = Mapper.Map<EditableCompany, Company>(Company);
            editingCompany.RelatedPeople = RelatedPersonListViewModel.RelatedPeople;

            try
            {
                if (EditMode)
                await _companiesService.UpdateCompanyAsync(editingCompany);
            else
                await _companiesService.AddCompanyAsync(editingCompany);
            Done?.Invoke();
        }
            catch (Exception ex)
            {
                Failed(ex);
    }
            finally
            {
                Company = null;
            }
        }
        private bool CanSave()
        {
            return !Company.HasErrors;
        }
        
        #endregion
    }
}
