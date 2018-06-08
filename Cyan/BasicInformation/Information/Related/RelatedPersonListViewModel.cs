using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.IServices.BasicInformation.Information;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saina.WPF.BasicInformation.Information.Related
{
    /// <summary>
    /// لیست افراد مرتبط
    /// </summary>
   public class RelatedPersonListViewModel:BindableBase
    {
        #region Constructors
        public RelatedPersonListViewModel(IRelatedPeopleService relatedPeopleService)
        {
            _relatedPeopleService = relatedPeopleService;
           // EditRelatedPersonCommand = new RelayCommand<RelatedPerson>(OnEditRelatedPerson);
            DeleteCommand = new RelayCommand<RelatedPerson>(OnDeleteRelatedPerson);
        }
        #endregion
        #region Fields
        private IRelatedPeopleService _relatedPeopleService;
      //  private List<RelatedPerson> _allRelatedPeople;
        #endregion
        #region Commands
        public ICommand AddRelatedPersonCommand { get; private set; }
   //     public ICommand EditRelatedPersonCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        #endregion
        #region Properties
        private ObservableCollection<RelatedPerson> _relatedPeople;
        public ObservableCollection<RelatedPerson> RelatedPeople
        {
            get { return _relatedPeople; }
            set { SetProperty(ref _relatedPeople, value); }
        }
       
       // public event Action<int> AddRelatedPersonRequested;
       // public event Action<RelatedPerson> EditRelatedPersonRequested;
        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<Exception> Failed;
        public int RelatedId { get; set; }

        #endregion
        #region Methode
        public  void LoadRelatedPeople()
        {
            if (RelatedPeople==null)
            {
                RelatedPeople = new ObservableCollection<RelatedPerson>();
            }
        }
        //private void OnEditRelatedPerson(RelatedPerson relatedPerson)
        //{
        //    relatedPerson.RelatedPersonId = RelatedId;
        //    EditRelatedPersonRequested(relatedPerson);
        //}

        private async void OnDeleteRelatedPerson(RelatedPerson relatedPerson)
        {
            if (Deleting?.Invoke() == true)
            {
                try
                {
                    await _relatedPeopleService.DeleteRelatedPersonAsync(relatedPerson.RelatedPersonId);
                    RelatedPeople.Remove(relatedPerson);
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
