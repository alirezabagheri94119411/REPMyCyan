using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.IServices.BasicInformation.Accounts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saina.WPF.BasicInformation.Accounts.SLStandard
{
    /// <summary>
    /// لیست شرح استاندارد
    /// </summary>
    public class SLStandardDescriptionListViewModel : BindableBase
    {
        #region Constructors
        public SLStandardDescriptionListViewModel(ISLStandardDescriptionsService sLStandardDescriptionsService)
        {
            _sLStandardDescriptionsService = sLStandardDescriptionsService;
            //EditSLStandardDescriptionCommand = new RelayCommand<SLStandardDescription>(OnEditSLStandardDescription);
            DeleteCommand = new RelayCommand<SLStandardDescription>(OnDeleteSLStandardDescription);
        }
        #endregion
        #region Fields
        private ISLStandardDescriptionsService _sLStandardDescriptionsService;
    //    private List<SLStandardDescription> _allSLStandardDescriptions;
        #endregion
        #region Commands
        public ICommand AddSLStandardDescriptionCommand { get; private set; }
       // public ICommand EditSLStandardDescriptionCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        #endregion
        #region Properties
        private ObservableCollection<SLStandardDescription> _sLStandardDescriptions;
        public ObservableCollection<SLStandardDescription> SLStandardDescriptions
        {
            get { return _sLStandardDescriptions; }
            set { SetProperty(ref _sLStandardDescriptions, value); }
        }

       // public event Action<SLStandardDescription> EditSLStandardDescriptionRequested;
        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<Exception> Failed;
        public int SLId { get; set; }

        #endregion
        #region Methode

        public  void LoadSLStandardDescriptions()
        {
            if (SLStandardDescriptions == null)
            {
                //_allSLStandardDescriptions = await _sLStandardDescriptionsService.GetSLStandardDescriptionsAsync(SLId);
                //SLStandardDescriptions = new ObservableCollection<SLStandardDescription>(_allSLStandardDescriptions);
                SLStandardDescriptions = new ObservableCollection<SLStandardDescription>();
            }
        }
        //private void OnEditSLStandardDescription(SLStandardDescription sLStandardDescription)
        //{
        //    sLStandardDescription.SLId = SLId;
        //    EditSLStandardDescriptionRequested(sLStandardDescription);
        //}

        private async void OnDeleteSLStandardDescription(SLStandardDescription sLStandardDescription)
        {
            if (Deleting?.Invoke() == true)
            {
                try
                {
                    await _sLStandardDescriptionsService.DeleteSLStandardDescriptionAsync(sLStandardDescription.SLStandardDescriptionId);
                    SLStandardDescriptions.Remove(sLStandardDescription);
                    Deleted();
                }
                catch (Exception ex)
                {
                    Failed(ex);
                }

            }

        }
        #endregion
    }
}
