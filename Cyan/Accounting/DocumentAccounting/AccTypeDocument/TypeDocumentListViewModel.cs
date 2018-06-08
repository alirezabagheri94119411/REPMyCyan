using Saina.ApplicationCore.DTOs;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
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

namespace Saina.WPF.Accounting.DocumentAccounting.AccTypeDocument
{
    /// <summary>
    /// لیست نوع اسناد
    /// </summary>
    class TypeDocumentListViewModel : BindableBase
    {
        #region Constructors
        public TypeDocumentListViewModel(ITypeDocumentsService typeDocumentsService, ICompanyInformationsService companyInformationsService)
        {
            _companyInformationsService = companyInformationsService;
            CompanyInformationModel = _companyInformationsService.GetCompanyInformationModel();
            _typeDocumentsService = typeDocumentsService;
            AddTypeDocumentCommand = new RelayCommand(OnAddTypeDocument);
            EditTypeDocumentCommand = new RelayCommand<TypeDocument>(OnEditTypeDocument);
            DeleteCommand = new RelayCommand<TypeDocument>(OnDeleteTypeDocument);
            _accessUtility = SmObjectFactory.Container.GetInstance<AccessUtility>();
        }
        #endregion
        #region Fields
        private ITypeDocumentsService _typeDocumentsService;
        private List<TypeDocument> _allTypeDocuments;
        public CompanyInformationModel CompanyInformationModel { get; set; }
        private ICompanyInformationsService _companyInformationsService;
        #endregion
        #region Commands
        public ICommand AddTypeDocumentCommand { get; private set; }
        public ICommand EditTypeDocumentCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        private AccessUtility _accessUtility;
        #endregion
        #region Properties
        private ObservableCollection<TypeDocument> _typeDocuments;
        public ObservableCollection<TypeDocument> TypeDocuments
        {
            get { return _typeDocuments; }
            set { SetProperty(ref _typeDocuments, value); }
        }

        public event Action<TypeDocument> AddTypeDocumentRequested;
        public event Action<TypeDocument> EditTypeDocumentRequested;
        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<string> Error;
        public event Action<Exception> Failed;
        #endregion
        #region Methode
        public async void LoadTypeDocuments()
        {
            _allTypeDocuments = await _typeDocumentsService.GetTypeDocumentsAsync();
            TypeDocuments = new ObservableCollection<TypeDocument>(_allTypeDocuments);
        }
        private void OnAddTypeDocument()
        {
            if (_accessUtility.HasAccess(43))
            {
                AddTypeDocumentRequested(new TypeDocument());
            }

        }
        private void OnEditTypeDocument(TypeDocument typeDocument)
        {
            if (_accessUtility.HasAccess(44))
            {
                var ids = new List<int> { 1, 2, 3, 4, 5, 6 };

                if (ids.Contains(typeDocument.TypeDocumentId))
                {
                    Error?.Invoke("امکان ویرایش وجود ندارد");
                }
                else
                {

                    EditTypeDocumentRequested(typeDocument);
                }
            }
        }

        private async void OnDeleteTypeDocument(TypeDocument typeDocument)
        {
            if (_accessUtility.HasAccess(45))
            {
                var ids = new List<int> { 1, 2, 3, 4, 5, 6 };


                if (Deleting())
                {
                    if (ids.Contains(typeDocument.TypeDocumentId))
                        Error?.Invoke("امکان حذف وجود ندارد");
                    else
                    {


                        try
                        {
                            await _typeDocumentsService.DeleteTypeDocumentAsync(typeDocument.TypeDocumentId);
                            TypeDocuments.Remove(typeDocument);
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
