using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Saina.ApplicationCore.Entities;
using Saina.ApplicationCore.IServices;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Threading;

namespace Saina.WPF.Customers
{
    class CustomerListViewModel : BindableBase
    {
        private ICustomersRepository _repo;
        private List<Customer> _allCustomers;

        public CustomerListViewModel(ICustomersRepository repo)
        {
            _repo = repo;
            PlaceOrderCommand = new RelayCommand<Customer>(OnPlaceOrder);
            AddCustomerCommand = new RelayCommand(OnAddCustomer);
            EditCustomerCommand = new RelayCommand<Customer>(OnEditCustomer);
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            DeleteCommand = new RelayCommand<Customer>(OnDeleteCustomer);
        }

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { SetProperty(ref _customers, value); }
        }
        private string _SearchInput;
        public string SearchInput
        {
            get { return _SearchInput; }
            set
            {
                SetProperty(ref _SearchInput, value);
                FilterCustomers(_SearchInput);
            }
        }

        private void FilterCustomers(string searchInput)
        {
            if (string.IsNullOrWhiteSpace(searchInput))
            {
                Customers = new ObservableCollection<Customer>(_allCustomers);
                return;
            }
            else
            {
                Customers = new ObservableCollection<Customer>(_allCustomers.Where(c => c.FullName.ToLower().Contains(searchInput.ToLower())));
            }
        }


        public ICommand PlaceOrderCommand { get; private set; }
        public ICommand AddCustomerCommand { get; private set; }
        public ICommand EditCustomerCommand { get; private set; }
        public ICommand ClearSearchCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public event Action<int> PlaceOrderRequested ;
        public event Action<Customer> AddCustomerRequested ;
        public event Action<Customer> EditCustomerRequested ;
        public event Func<bool> Deleting;
        public event Action Deleted;
        public event Action<Exception> Failed;

        public async void LoadCustomers()
        {
            if (Customers==null)
            {
                IsBusy = true;
                _allCustomers = await _repo.GetCustomersAsync();
                Customers = new ObservableCollection<Customer>(_allCustomers);
                IsBusy = false;
            }
        }
        public override void UnLoaded()
        {
            Customers = null;
        }
        private void OnPlaceOrder(Customer customer)
        {
            PlaceOrderRequested(customer.Id);
        }

        private void OnAddCustomer()
        {
            AddCustomerRequested(new Customer());

        }
        private void OnEditCustomer(Customer cust)
        {
            EditCustomerRequested(cust);
        }

        private void OnClearSearch()
        {
            SearchInput = null;
        }
        private async void OnDeleteCustomer(Customer customer)
        {
            if (Deleting())
            {
                try
                {
                    await _repo.DeleteCustomerAsync(customer.Id);
                    Customers.Remove(customer);
                    Deleted();
                }
                catch (Exception ex)
                {
                    Failed(ex);
                }

            }

        }

    }
}
