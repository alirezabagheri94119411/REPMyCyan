using AutoMapper;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Accounts.SLStandard
{
    /// <summary>
    /// افزودن و ویرایش شرح استاندارد
    /// </summary>
  public  class AddEditSLStandardDescriptionViewModel:BindableBase
    {
        #region Constructors
        public AddEditSLStandardDescriptionViewModel(ISLStandardDescriptionsService sLStandardDescriptionsService)
        {
            _sLStandardDescriptionsService = sLStandardDescriptionsService;
           
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);

        }
        #endregion
        #region Fields
        private ISLStandardDescriptionsService _sLStandardDescriptionsService;
      
      
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
        private EditableSLStandardDescription _SLStandardDescription;
        public EditableSLStandardDescription SLStandardDescription
        {
            get { return _SLStandardDescription; }
            set { SetProperty(ref _SLStandardDescription, value); }
        }
       
        public event Action<Exception> Failed;
        public event Action Done;
        #endregion
        #region Methode
      
       
        public void SetSLStandardDescription(SLStandardDescription sLStandardDescription)
        {
            SLStandardDescription = Mapper.Map<SLStandardDescription, EditableSLStandardDescription>(sLStandardDescription);
            SLStandardDescription.ErrorsChanged += RaiseCanExecuteChanged;
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
            var editingSLStandardDescription = Mapper.Map<EditableSLStandardDescription, SLStandardDescription>(SLStandardDescription);
            try
            {
                if (EditMode)
                    await _sLStandardDescriptionsService.UpdateSLStandardDescriptionAsync(editingSLStandardDescription);
                else
                    await _sLStandardDescriptionsService.AddSLStandardDescriptionAsync(editingSLStandardDescription);
                Done?.Invoke();
            }
            catch (Exception ex)
            {
                Failed?.Invoke(ex);
            }
            finally
            {
                SLStandardDescription = null;
            }
        }

        private bool CanSave()
        {
            return true;//!SLStandardDescription.HasErrors;
        }

        #endregion
    }
}
