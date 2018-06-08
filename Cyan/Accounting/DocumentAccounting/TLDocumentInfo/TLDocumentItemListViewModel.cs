using AutoMapper;
using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.BasicInformation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.TLDocumentInfo
{
    class TLDocumentItemListViewModel : BindableBase
    {
        #region Constructors
        public TLDocumentItemListViewModel(IAppContextService appContextService, ITLDocumentsService tLDocumentsService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _tLDocumentsService = tLDocumentsService;

            _appContextService = appContextService;
            CancelCommand = new RelayCommand(OnCancel);
            //  SaveCommand = new RelayCommand(OnSave, CanSave);

        }


        #endregion
        #region Fields
        private ITLDocumentsService _tLDocumentsService;

        private IAppContextService _appContextService;
        public CompanyInformationModel CompanyInformationModel { get; set; }
        private ICompanyInformationsService _companyInformationsService;

        //   private AccDocumentHeader _editingAccDocumentHeader = null;
        #endregion
        #region Commands
        public RelayCommand CancelCommand { get; private set; }
       


        #endregion
        #region Properties
        private EditableTLDocumentHeader _tLDocumentHeader;
        public EditableTLDocumentHeader TLDocumentHeader
        {
            get { return _tLDocumentHeader; }
            set { SetProperty(ref _tLDocumentHeader, value); }
        }
        private ObservableCollection<TLDocumentItem> _tLDocumentItems;
        public ObservableCollection<TLDocumentItem> TLDocumentItems
        {
            get { return _tLDocumentItems; }
            set { SetProperty(ref _tLDocumentItems, value); }
        }
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }


        public event Action<Exception> Failed;

        public event Action Done;
        #endregion
        #region Methode

        public void SetTLDocumentHeader(TLDocumentHeader tLDocumentHeader)
        {
            TLDocumentHeader = Mapper.Map<TLDocumentHeader, EditableTLDocumentHeader>(tLDocumentHeader);
          
        }

        private void OnCancel()
        {
            Done?.Invoke();
        }
        public async void LoadAccDocumentItems()
        {
            IsBusy = true;
            Id = TLDocumentHeader.TLDocumentHeaderId;
            TLDocumentItems = new ObservableCollection<TLDocumentItem>(await _tLDocumentsService.GetTLDocumentItemsAsync(Id));
            IsBusy = false;
        }

        #endregion
    }
}
