using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.ApplicationCore.IServices.BasicInformation.Information;
using Saina.WPF.Behaviors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace Saina.WPF.BasicInformation.Information.BankInfo
{
    /// <summary>
    /// لیست بانک ها
    /// </summary>
    class BankListViewModel : BindableBase
    {
        #region Constructors
        public BankListViewModel(IBanksService banksService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _banksService = banksService;
            AddBankCommand = new RelayCommand(OnAddBank);
            EditBankCommand = new RelayCommand<Bank>(OnEditBank);
            DeleteCommand = new RelayCommand<Bank>(OnDeleteBank);
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();
        }
        #endregion
        #region Fields
        private IBanksService _banksService;
        private List<Bank> _allBanks;
        public CompanyInformationModel CompanyInformationModel { get; set; }
        private ICompanyInformationsService _companyInformationsService;
        #endregion
        #region Commands
        public ICommand AddBankCommand { get; private set; }
        public ICommand EditBankCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        private AccessUtility _accessUtility;
        #endregion
        #region Properties
        private ObservableCollection<Bank> _banks;
        public ObservableCollection<Bank> Banks
        {
            get { return _banks; }
            set { SetProperty(ref _banks, value); }
        }

        public event Action<Bank> AddBankRequested;
        public event Action<Bank> EditBankRequested;
        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<Exception> Failed;
        public event Action<string> Error;

        #endregion
        #region Methode

        public async void LoadBanks()
        {
            _allBanks = await _banksService.GetBanksAsync();
            Banks = new ObservableCollection<Bank>(_allBanks);
        }
        private void OnAddBank()
        {
            if (_accessUtility.HasAccess(25))
            {
                AddBankRequested(new Bank());
            }

        }
        private void OnEditBank(Bank bank)
        {
            if (_accessUtility.HasAccess(26))
            {
                EditBankRequested(bank);
            }
        }
        private async void OnDeleteBank(Bank bank)
        {
            if (_accessUtility.HasAccess(27))
            {
                var x = await _banksService.GetBankAccountAsync(bank.BankId);
                if (x == true)
                {
                    Error(".امکان حذف وجود ندارد");

                }
                else
                {
                    if (Deleting())
                    {
                        try
                        {
                            await _banksService.DeleteBankAsync(bank.BankId);
                            Banks.Remove(bank);
                            Deleted();
                        }
                        catch (Exception ex)
                        {
                            Failed(ex);
                        }

                    }
                }
            }

        }
        #endregion

    }
}
