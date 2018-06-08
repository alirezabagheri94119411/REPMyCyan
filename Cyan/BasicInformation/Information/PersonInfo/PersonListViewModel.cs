using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.ApplicationCore.IServices.BasicInformation.Information;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saina.WPF.BasicInformation.Information.PersonInfo
{
    /// <summary>
    /// لیست پرسنل
    /// </summary>
    public class PersonListViewModel : BindableBase
    {
        #region Constructors
        public PersonListViewModel(IPeopleService peopleService)
        {
            _peopleService = peopleService;
            AddPersonCommand = new RelayCommand(OnAddPerson);
            EditPersonCommand = new RelayCommand<Person>(OnEditPerson);
            DeleteCommand = new RelayCommand<Person>(OnDeletePerson);
            AddRelatedPersonCommand = new RelayCommand<Person>(OnAddRelatedPerson);
            Person = new Person();
        }
        #endregion
        #region Fields
        private IPeopleService _peopleService;
        private List<Person> _allPeople;
        private SainaDbContext _uow;



        #endregion
        #region Commands
        public ICommand AddPersonCommand { get; private set; }
        public ICommand EditPersonCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddRelatedPersonCommand { get; private set; }

        #endregion
        #region Properties
        private ObservableCollection<RelatedPerson> _relatedPeople;
        public ObservableCollection<RelatedPerson> RelatedPeople
        {
            get { return _relatedPeople; }
            set { SetProperty(ref _relatedPeople, value); }
        }
        private ObservableCollection<Person> _people;
        public ObservableCollection<Person> People
        {
            get { return _people; }
            set { SetProperty(ref _people, value); }
        }
        private long? _DCode;
        public long? DCode
        {
            get { return _DCode; }
            set { SetProperty(ref _DCode, value); }
        }

        public event Action<Person> AddPersonRequested ;
        public event Action<Person> EditPersonRequested ;
        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<Exception> Failed;
        public event Action<Person> AddRelatedPersonRequested;
        private Person _Person;
        public Person Person
        {
            get { return _Person; }
            set { SetProperty(ref _Person, value); }
        }

        #endregion
        #region Methode
        private void OnAddRelatedPerson(Person relatedPerson)
        {
            AddRelatedPersonRequested(relatedPerson);
        }
        public async void LoadPeople()
        {
            _uow = new SainaDbContext();

            _allPeople = await _peopleService.GetPeopleCodeAsync(DCode);
            People = new ObservableCollection<Person>(_allPeople);
        }
        private void OnAddPerson()
        {
            AddPersonRequested(new Person());

        }
        private void OnEditPerson(Person person)
        {
            EditPersonRequested(person);
        }

        private async void OnDeletePerson(Person person)
        {
            if (Deleting())
            {
                try
                {
                    await _peopleService.DeletePersonAsync(person.PersonId);
                    People.Remove(person);
                    Deleted();
                }
                catch (Exception ex)
                {
                    Failed(ex);
                }

            }

        }
        public void Add()
        {
            _uow.People.Add(Person);
        }
        public void Save()
        {

            //if (BankAccount != null)
            //{
            //    //stop Entity Framework from trying to save/insert child objects?
            //    var items = BankAccounts.ToList();
            //    for (int i = 0; i < items.Count; i++)
            //    {
            //        if (items[i].Bank != null)
            //            _uow.Entry(items[i].Bank).State = EntityState.Detached;
            //        if (items[i].AccountType != null)
            //            _uow.Entry(items[i].AccountType).State = EntityState.Detached;
            //        if (items[i].CurrencyType != null)
            //            _uow.Entry(items[i].CurrencyType).State = EntityState.Detached;
            //    }
            //}



            var x = _uow.SaveChanges();
        }
        #endregion
    }
}
