using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Saina.ApplicationCore.Entities;

namespace Saina.ApplicationCore.IServices
{
    public interface ICustomersRepository
    {
        Task<List<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerAsync(int id);
        Task<Customer> AddCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int customerId);
        void Reject(Customer customer);
    }
}
