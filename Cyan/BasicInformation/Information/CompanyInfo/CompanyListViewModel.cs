using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.IServices.BasicInformation.Information;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saina.WPF.BasicInformation.Information.CompanyInfo
{/// <summary>
/// لیست شرکت ها
/// </summary>
   public class CompanyListViewModel : BindableBase
    {
        #region Constructors
        public CompanyListViewModel(ICompaniesService companiesService)
        {
            _companiesService = companiesService;
            //AddCompanyCommand = new RelayCommand(OnAddCompany);
            //EditCompanyCommand = new RelayCommand<Company>(OnEditCompany);
            DeleteCommand = new RelayCommand<Company>(OnDeleteCompany);
          
           // AddRelatedPersonCommand = new RelayCommand<Company>(OnAddRelatedPerson);
        }
        #endregion
        #region Fields
        private ICompaniesService _companiesService;
        private List<Company> _allCompanies;
        //private List<RelatedPerson> _allRelatedPeople;
        public int SLId { get; set; }
        private SainaDbContext _uow;

        #endregion
        #region Commands
        // public ICommand AddRelatedPersonCommand { get; private set; }

        // public ICommand AddCompanyCommand { get; private set; }

        // public ICommand EditCompanyCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        #endregion
        #region Properties
        private ObservableCollection<Company> _companies;
        public ObservableCollection<Company> Companies
        {
            get { return _companies; }
            set { SetProperty(ref _companies, value); }
        }
        private Company _Company;
        public Company Company
        {
            get { return _Company; }
            set { SetProperty(ref _Company, value); }
        }

        //public event Action<Company> AddRelatedPersonRequested;
        //public event Action<Company> EditCompanyRequested;
        //public event Action<Company> AddCompanyRequested ;

        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<Exception> Failed;
        public int RelatedId { get; set; }
        #endregion
        #region Methode
        //private void OnAddRelatedPerson(Company RelatedPerson)
        //{
        //    AddRelatedPersonRequested(RelatedPerson);
        //}
       
        public async  void LoadCompanies()
        {
            _uow = new SainaDbContext();
            if (Companies == null)
            {
                _allCompanies = await _companiesService.GetCompaniesAsync();
                Companies = new ObservableCollection<Company>(_allCompanies);
            
            }
           
        }
        //private void OnAddCompany()
        //{
        //    AddCompanyRequested(new Company());

        //}
        //private void OnEditCompany(Company company)
        //{
        //    EditCompanyRequested(company);
        //}

        private async void OnDeleteCompany(Company company)
        {
            if (Deleting())
            {
                try
                {
                    await _companiesService.DeleteCompanyAsync(company.CompanyId);
                    Companies.Remove(company);
                    Deleted();
                }
                catch (Exception ex)
                {
                    Failed(ex);
                }

            }

        }
        public void Add()
        {
            _uow.Companies.Add(Company);
        }
        public void Save()
        {

            //if (BankAccount != null)
            //{
            //    //stop Entity Framework from trying to save/insert child objects?
            //    var items = BankAccounts.ToList();
            //    for (int i = 0; i < items.Count; i++)
            //    {
            //        if (items[i].Bank != null)
            //            _uow.Entry(items[i].Bank).State = EntityState.Detached;
            //        if (items[i].AccountType != null)
            //            _uow.Entry(items[i].AccountType).State = EntityState.Detached;
            //        if (items[i].CurrencyType != null)
            //            _uow.Entry(items[i].CurrencyType).State = EntityState.Detached;
            //    }
            //}



            var x = _uow.SaveChanges();
        }
        #endregion
    }
}
