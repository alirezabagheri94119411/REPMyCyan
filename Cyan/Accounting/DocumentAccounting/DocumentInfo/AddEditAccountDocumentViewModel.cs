using AutoMapper;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.AccountDocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.DocumentInfo
{
    /// <summary>
    /// افزودن و ویرایش اسناد حسابداری
    /// </summary>
    public class AddEditAccountDocumentViewModel : BindableBase
    {
        #region Constructors
        public AddEditAccountDocumentViewModel(IAccountDocumentsService accountDocumentsService)
        {
            _accountDocumentsService = accountDocumentsService;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
        }
        #endregion
        #region Fields
        private IAccountDocumentsService _accountDocumentsService;
     //   private AccountDocument _editingAccountDocument = null;
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
        private EditableAccountDocument _accountDocument;

        public EditableAccountDocument AccountDocument
        {
            get { return _accountDocument; }
            set
            {
                SetProperty(ref _accountDocument, value);
            }
        }

        public event Action<Exception> Failed;
        public event Action Done;
        #endregion
        #region Methode
        
        public void SetAccountDocument(AccountDocument accountDocument)
        {
            AccountDocument = Mapper.Map<AccountDocument, EditableAccountDocument>(accountDocument);
            AccountDocument.ValidationDelegate += AccountDocument_ValidationDelegate;

            AccountDocument.ErrorsChanged += RaiseCanExecuteChanged;
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
            var editingAccountDocument = Mapper.Map<EditableAccountDocument, AccountDocument>(AccountDocument);
            try
            {
                if (EditMode)
                await _accountDocumentsService.UpdateAccountDocumentAsync(editingAccountDocument);
            else
                await _accountDocumentsService.AddAccountDocumentAsync(editingAccountDocument);
            Done?.Invoke();
            }
            catch (Exception ex)
            {
                Failed(ex);
            }
            finally
            {
                AccountDocument = null;
            }
        }
       
        private bool CanSave()
        {
            return !AccountDocument.HasErrors;
        }
        private async Task<List<string>> AccountDocument_ValidationDelegate(object sender, string propertyName)
        {
            var accountDocument = (EditableAccountDocument)sender;
            List<string> errors = new List<string>();
            switch (propertyName)
            {
                case nameof(AccountDocument.AccountDocumentTitle):

                    if (await _accountDocumentsService.HasTitle(accountDocument.AccountDocumentTitle))
                        errors.Add("عنوان نباید تکراری باشد");
                    return errors;

                default:
                    return null;
            }
        }
        #endregion
    }
}
