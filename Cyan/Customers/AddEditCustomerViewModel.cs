using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saina.ApplicationCore.Entities;
using Saina.ApplicationCore.IServices;

namespace Saina.WPF.Customers
{
    class AddEditCustomerViewModel : BindableBase
    {
        private ICustomersRepository _repo;
        public AddEditCustomerViewModel(ICustomersRepository repo)
        {
            _repo = repo;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, ()=> !Customer.HasErrors); 
        }
        private bool _EditMode;
        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }

        private Customer _Customer;
        public Customer Customer
        {
            get { return _Customer; }
            set { SetProperty(ref _Customer, value); }
        }


        public void SetCustomer(Customer cust)
        {
            Customer = cust;
            //Customer = Mapper.Map<Customer, SimpleEditableCustomer>(cust);
            Customer.ErrorsChanged += (s,ea)=> { SaveCommand.RaiseCanExecuteChanged(); };
        }

        //private void RaiseCanExecuteChanged(object sender, EventArgs e)
        //{
        //    SaveCommand.RaiseCanExecuteChanged();
        //}

        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }

        public event Action Done;

        private void OnCancel()
        {
            _repo.Reject(Customer);
            //entry.State = EntityState.Unchanged;
            Done?.Invoke();
        }

        private async void OnSave()
        {
            //_editingCustomer = Mapper.Map<SimpleEditableCustomer, Customer>(Customer);
            if (EditMode)
                await _repo.UpdateCustomerAsync(Customer);
            else
                await _repo.AddCustomerAsync(Customer);
            Done?.Invoke();
        }

        //private bool CanSave()
        //{
        //    return !Customer.HasErrors;
        //}

    }
}
