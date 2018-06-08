using Saina.ApplicationCore.Entities.BasicInformation.Information;
using Saina.Data.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saina.WPF.BasicInformation.Information.BankAccounts
{
    class RelatedPersonListWindowViewModel : BindableBase
    {
        #region Constructors
        public RelatedPersonListWindowViewModel()
        {
            BankAccount = new BankAccount();
        }
        #endregion
        #region Fields
        private SainaDbContext _uow;
        private IEditableCollectionViewAddNewItem _relatedPersons;
        #endregion
        #region Commands

        #endregion
        #region Properties
        public IEditableCollectionViewAddNewItem RelatedPersons
        {
            get { return _relatedPersons; }
            set
            {
                SetProperty(ref _relatedPersons, value);
            }
        }
        private BankAccount _bankAccount;
        public BankAccount BankAccount
        {
            get { return _bankAccount; }
            set { SetProperty(ref _bankAccount, value); }
        }




        #endregion
        #region Methods
        public void Loaded()
        {
            _uow = new SainaDbContext();
            var x = _uow.RelatedPeople.ToList();

            RelatedPersons = new Telerik.Windows.Data.QueryableCollectionView(x);

        }
        public override void UnLoaded()
        {
            _uow.Dispose();
        }
        public void AddRelatedPerson(RelatedPerson relatedPerson)
        {
            _uow.RelatedPeople.Add(relatedPerson);
        }
        public void Save()
        {
            _uow.SaveChanges();
        }
        public int DeleteRelatedPerson(RelatedPerson relatedPerson)
        {
            _uow.RelatedPeople.Remove(relatedPerson);
            return _uow.SaveChanges();
        }

        #endregion



    }
}
