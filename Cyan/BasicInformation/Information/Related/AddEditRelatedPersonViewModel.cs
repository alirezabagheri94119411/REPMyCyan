using AutoMapper;
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.IServices.BasicInformation.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Information.Related
{
    /// <summary>
    /// افزودن و ویرایش افراد مرتبط
    /// </summary>
    class AddEditRelatedPersonViewModel:BindableBase
    {
        #region Constructors
        public AddEditRelatedPersonViewModel(IRelatedPeopleService relatedPeopleService)
        {
            _relatedPeopleService = relatedPeopleService;

            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);

        }
        #endregion
        #region Fields
        private IRelatedPeopleService _relatedPeopleService;

      

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
        private EditableRelatedPerson _RelatedPerson;
        public EditableRelatedPerson RelatedPerson
        {
            get { return _RelatedPerson; }
            set { SetProperty(ref _RelatedPerson, value); }
        }

        public event Action<Exception> Failed;
        public event Action Done;
        #endregion
        #region Methode


        public void SetRelatedPerson(RelatedPerson relatedPerson)
        {
          
            RelatedPerson = Mapper.Map<RelatedPerson, EditableRelatedPerson>(relatedPerson);
            RelatedPerson.ErrorsChanged += RaiseCanExecuteChanged;
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
            var editingRelatedPerson = Mapper.Map<EditableRelatedPerson, RelatedPerson>(RelatedPerson);
            try
            {
                if (EditMode)
                    await _relatedPeopleService.UpdateRelatedPersonAsync(editingRelatedPerson);
                else
                    await _relatedPeopleService.AddRelatedPersonAsync(editingRelatedPerson);
                Done?.Invoke();
            }
            catch (Exception ex)
            {
                Failed?.Invoke(ex);
            }
            finally
            {
                RelatedPerson = null;
            }
        }

        private bool CanSave()
        {
            return true;//!RelatedPerson.HasErrors;
        }

        #endregion
    }
}
