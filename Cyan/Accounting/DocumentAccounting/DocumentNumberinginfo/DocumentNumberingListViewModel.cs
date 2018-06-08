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

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentNumberinginfo
{
    /// <summary>
    /// لیست شماره گذاری اسناد
    /// </summary>
   public class DocumentNumberingListViewModel: BindableBase
    {
        #region Constructors
        public DocumentNumberingListViewModel(IDocumentNumberingsService documentNumberingsService, IAccountDocumentsService accountDocumentsService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _accountDocumentsService = accountDocumentsService;
            _documentNumberingsService = documentNumberingsService;
            AddDocumentNumberingCommand = new RelayCommand(OnAddDocumentNumbering);
            EditDocumentNumberingCommand = new RelayCommand<DocumentNumbering>(OnEditDocumentNumbering);
            DeleteCommand = new RelayCommand<DocumentNumbering>(OnDeleteDocumentNumbering);
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();
        }
        #endregion
        #region Fields
        private IAccountDocumentsService _accountDocumentsService;

        private List<AccountDocument> _allAccountDocuments;
        private IDocumentNumberingsService _documentNumberingsService;
        private List<DocumentNumbering> _allDocumentNumberings;
        public CompanyInformationModel CompanyInformationModel { get; set; }
        private ICompanyInformationsService _companyInformationsService;
        #endregion
        #region Commands
        public ICommand AddDocumentNumberingCommand { get; private set; }
        public ICommand EditDocumentNumberingCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        private AccessUtility _accessUtility;
        #endregion
        #region Properties
        private ObservableCollection<DocumentNumbering> _documentNumberings;
        public ObservableCollection<DocumentNumbering> DocumentNumberings
        {
            get { return _documentNumberings; }
            set { SetProperty(ref _documentNumberings, value); }
        }
        private ObservableCollection<AccountDocument> _accountDocuments;

        public ObservableCollection<AccountDocument> AccountDocuments
        {
            get { return _accountDocuments; }
            set { SetProperty(ref _accountDocuments, value); }
        }

        public event Action<DocumentNumbering> AddDocumentNumberingRequested ;
        public event Action<DocumentNumbering> EditDocumentNumberingRequested ;
        public event Func<bool> Deleting;
        public event Action<string> Error;
        public event Action Deleted;
        public event Action<Exception> Failed;
        #endregion
        #region Methode
       
        public async void LoadDocumentNumberings()
        {
            _allDocumentNumberings = await _documentNumberingsService.GetDocumentNumberingsAsync();
            DocumentNumberings = new ObservableCollection<DocumentNumbering>(_allDocumentNumberings);
        }
        private async  void OnAddDocumentNumbering()
        {
            if (_accessUtility.HasAccess(37))
            {
                _allAccountDocuments = await _accountDocumentsService.GetAccountDocumentsAsync();
                AccountDocuments = new ObservableCollection<AccountDocument>(_allAccountDocuments);
                if (AccountDocuments?.Any() != true)
                {
                    Error(".نوع سند ثبت نشده است");



                }
                else
                {

                    AddDocumentNumberingRequested(new DocumentNumbering());
                }
            }


        }
        private void OnEditDocumentNumbering(DocumentNumbering documentNumbering)
        {
            if (_accessUtility.HasAccess(38))
            {
                EditDocumentNumberingRequested(documentNumbering);
            }
        }
        private async void OnDeleteDocumentNumbering(DocumentNumbering documentNumbering)
        {
            if (_accessUtility.HasAccess(39))
            {
                var x = await _documentNumberingsService.GetAccDocumentHeaderAsync(documentNumbering.AccountDocumentId);
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
                            await _documentNumberingsService.DeleteDocumentNumberingAsync(documentNumbering.DocumentNumberingId);
                            DocumentNumberings.Remove(documentNumbering);
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
