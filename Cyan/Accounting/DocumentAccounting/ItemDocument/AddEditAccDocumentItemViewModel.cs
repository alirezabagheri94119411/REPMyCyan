using AutoMapper;
using Saina.ApplicationCore.Entities.Accounting.DocumentAccounting;
using Saina.ApplicationCore.IServices.Accounting.DocumentAccounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.Accounting.DocumentAccounting.ItemDocument
{
    class AddEditAccDocumentItemViewModel:BindableBase
    {
        #region Constructors
        public AddEditAccDocumentItemViewModel(IAccDocumentItemsService accDocumentItemsService)
        {
            _accDocumentItemsService = accDocumentItemsService;

            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);

        }
        #endregion
        #region Fields
        private IAccDocumentItemsService _accDocumentItemsService;


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
        private EditableAccDocumentItem _AccDocumentItem;
        public EditableAccDocumentItem AccDocumentItem
        {
            get { return _AccDocumentItem; }
            set { SetProperty(ref _AccDocumentItem, value); }
        }

        public event Action<Exception> Failed;
        public event Action Done;
        #endregion
        #region Methode


        public void SetAccDocumentItem(AccDocumentItem accDocumentItem)
        {
            AccDocumentItem = Mapper.Map<AccDocumentItem, EditableAccDocumentItem>(accDocumentItem);
            AccDocumentItem.ErrorsChanged += RaiseCanExecuteChanged;
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
            var editingAccDocumentItem = Mapper.Map<EditableAccDocumentItem, AccDocumentItem>(AccDocumentItem);
            try
            {
                if (EditMode)
                    await _accDocumentItemsService.UpdateAccDocumentItemAsync(editingAccDocumentItem);
                else
                    await _accDocumentItemsService.AddAccDocumentItemAsync(editingAccDocumentItem);
                Done?.Invoke();
            }
            catch (Exception ex)
            {
                Failed?.Invoke(ex);
            }
            finally
            {
                AccDocumentItem = null;
            }
        }

        private bool CanSave()
        {
            return true;//!AccDocumentItem.HasErrors;
        }

        #endregion
    }
}
