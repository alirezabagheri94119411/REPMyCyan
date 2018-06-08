using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.AccountDocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using Saina.WPF.Behaviors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentInfo
{
    /// <summary>
    /// لیست اسناد حسابداری
    /// </summary>
    class AccountDocumentListViewModel:BindableBase
    {
        #region Constructors
        public AccountDocumentListViewModel(IAccountDocumentsService accountDocumentsService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();

            _accountDocumentsService = accountDocumentsService;
            AddAccountDocumentCommand = new RelayCommand(OnAddAccountDocument);
            EditAccountDocumentCommand = new RelayCommand<AccountDocument>(OnEditAccountDocument);
            DeleteCommand = new RelayCommand<AccountDocument>(OnDeleteAccountDocument);
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();
        }
        #endregion
        #region Fields
        private IAccountDocumentsService _accountDocumentsService;

        private List<AccountDocument> _allAccountDocuments;
        public CompanyInformationModel CompanyInformationModel { get; set; }
        private ICompanyInformationsService _companyInformationsService;


        #endregion
        #region Commands
        public ICommand AddAccountDocumentCommand { get; private set; }
        public ICommand EditAccountDocumentCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        private AccessUtility _accessUtility;
        #endregion
        #region Properties
        private ObservableCollection<AccountDocument> _accountDocuments;
        public ObservableCollection<AccountDocument> AccountDocuments
        {
            get { return _accountDocuments; }
            set { SetProperty(ref _accountDocuments, value); }
        }
        public event Action<AccountDocument> AddAccountDocumentRequested ;
        public event Action<AccountDocument> EditAccountDocumentRequested ;
        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<string> Error;
        public event Action<Exception> Failed;
        #endregion
        #region Methode
        public async void LoadAccountDocuments()
        {
            _allAccountDocuments = await _accountDocumentsService.GetAccountDocumentsAsync();
            AccountDocuments = new ObservableCollection<AccountDocument>(_allAccountDocuments);
        }
        private void OnAddAccountDocument()
        {
            if (_accessUtility.HasAccess(34))
            {
                AddAccountDocumentRequested(new AccountDocument());
            }

        }
        private void OnEditAccountDocument(AccountDocument accountDocument)
        {
            if (_accessUtility.HasAccess(35))
            {
                EditAccountDocumentRequested(accountDocument);
            }
        }

        private async void OnDeleteAccountDocument(AccountDocument accountDocument)
        {
            if (_accessUtility.HasAccess(36))
            {
                var x = await _accountDocumentsService.GetDocumentNumberingAsync(accountDocument.AccountDocumentId);
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
                            await _accountDocumentsService.DeleteAccountDocumentAsync(accountDocument.AccountDocumentId);
                            AccountDocuments.Remove(accountDocument);
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

