using AutoMapper;
using Saina.ApplicationCore.Entities.BasicInformation.Accounts;
using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.IServices.BasicInformation.Information;
using Saina.WPF.BasicInformation.Information.Related;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Information.PersonInfo
{
    /// <summary>
    /// افزودن و ویرایش پرسنل
    /// </summary>
    class AddEditPersonViewModel : BindableBase
    {
        #region Constructors
        public AddEditPersonViewModel(IPeopleService peopleService, RelatedPersonListViewModel relatedPersonListViewModel)
        {

            _peopleService = peopleService;

            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            RelatedPersonListViewModel = relatedPersonListViewModel;

        }
        #endregion
        #region Fields
        private IPeopleService _peopleService;
        private Person _editingPerson = null;
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
        private RelatedPersonListViewModel _relatedPersonListViewModel;

        public RelatedPersonListViewModel RelatedPersonListViewModel
        {
            get { return _relatedPersonListViewModel; }
            set { SetProperty(ref _relatedPersonListViewModel, value); }
        }
        private Person _Person;
        public Person Person
        {
            get { return _Person; }
            set { SetProperty(ref _Person, value); }
        }
        private DL _DL;
        public DL DL
        {
            get { return _DL; }
            set { SetProperty(ref _DL, value); }
        }
        public event Action<Exception> Failed;
        public event Action Done;
        #endregion
        #region Methode

        public void SetPerson(Person person)
        {
            if (EditMode == false)
                RelatedPersonListViewModel.RelatedPeople = new ObservableCollection<RelatedPerson>();

            Person = person; //Mapper.Map<Person, EditablePerson>(person);
            Person.ErrorsChanged += RaiseCanExecuteChanged;
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
            //var editingPerson = Mapper.Map<EditablePerson, Person>(Person);
            //editingPerson.RelatedPeople = RelatedPersonListViewModel.RelatedPeople;
            //try
            //{
            //    // UpdatePerson(Person, _editingPerson);
            //    if (EditMode)
            //        await _peopleService.UpdatePersonAsync(editingPerson);
            //    else
            //        await _peopleService.AddPersonAsync(editingPerson);
            //    Done?.Invoke();
            //}
            //catch (Exception ex)
            //{
            //    Failed(ex);
            //}
            //finally
            //{
            //    Person = null;
            //}
        }
       
        private bool CanSave()
        {
            return !Person.HasErrors;
        }

        #endregion
    }
}
