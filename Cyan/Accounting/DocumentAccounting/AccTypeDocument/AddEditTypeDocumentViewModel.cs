using AutoMapper;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.AccTypeDocument
{
    /// <summary>
    /// افزودن و ویرایش نوع سند
    /// </summary>
    class AddEditTypeDocumentViewModel : BindableBase
    {

        #region Constructors
        public AddEditTypeDocumentViewModel(ITypeDocumentsService typeDocumentsService)
        {
            _typeDocumentsService = typeDocumentsService;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);

        }
        #endregion
        #region Fields
        private ITypeDocumentsService _typeDocumentsService;
        #endregion
        #region Commands

        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }

        public event Action Done;

        #endregion
        #region Properties
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }

        private EditableTypeDocument _TypeDocument;
        public EditableTypeDocument TypeDocument
        {
            get { return _TypeDocument; }
            set { SetProperty(ref _TypeDocument, value); }
        }
        public void SetTypeDocument(TypeDocument typeDocument)
        {
            TypeDocument = Mapper.Map<TypeDocument, EditableTypeDocument>(typeDocument);
            TypeDocument.ErrorsChanged += RaiseCanExecuteChanged;
            TypeDocument.ValidationDelegate += TypeDocument_ValidationDelegate;
        }

        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }
        private void OnCancel()
        {
            TypeDocument = null;
            Done?.Invoke();
        }
        public event Action<Exception> Failed;
        public event Action<string> Information;
        
        private async void OnSave()
        {
            var editingTypeDocument = Mapper.Map<EditableTypeDocument, TypeDocument>(TypeDocument);
            if (await _typeDocumentsService.HasTitle(editingTypeDocument.TypeDocumentTitle))
            {
                Information?.Invoke("عنوان تکراری می باشد");
            }
            else
            {

            

            try
            {
                if (EditMode)
                    await _typeDocumentsService.UpdateTypeDocumentAsync(editingTypeDocument);
                else
                    await _typeDocumentsService.AddTypeDocumentAsync(editingTypeDocument);
                Done?.Invoke();
            }
            catch (Exception ex)
            {
                Failed?.Invoke(ex);
            }
            finally
            {
                TypeDocument = null;
            }
            }
        }
        private bool CanSave()
        {
            return !TypeDocument.HasErrors;
        }
        private async Task<List<string>> TypeDocument_ValidationDelegate(object sender, string propertyName)
        {
            var accountDocument = (EditableTypeDocument)sender;
            List<string> errors = new List<string>();
            switch (propertyName)
            {
                case nameof(AccountDocument.AccountDocumentTitle):

                    if (await _typeDocumentsService.HasTitle(accountDocument.TypeDocumentTitle))
                        errors.Add("عنوان نباید تکراری باشد");
                    return errors;

                default:
                    return null;
            }
        }
        #endregion
    }

}
