using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saina.ApplicationCore.Entities;

namespace Saina.ApplicationCore.IServices
{
    public interface IOrdersRepository
    {
        Task<List<Order>> GetOrdersForCustomersAsync(int customerId);
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> AddOrderAsync(Order order);
        Task<Order> UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);

        Task<List<Product22>> GetProductsAsync();
        Task<List<ProductOption>> GetProductOptionsAsync();
        Task<List<ProductSize>> GetProductSizesAsync();
        Task<List<OrderStatus>> GetOrderStatusesAsync();
    }
}
