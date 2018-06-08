using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Saina.ApplicationCore.Entities;
using Saina.ApplicationCore.IServices;
using Saina.Data;
using Saina.Data.Context;

namespace Saina.Data.Services
{
    public class OrdersRepository : IOrdersRepository
    {
        DbContext _context;
        public OrdersRepository(SainaDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrdersForCustomersAsync(int customerId)
    {
        return await _context.Set<Order>().Where(o => o.CustomerId == customerId).ToListAsync();
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _context.Set<Order>().ToListAsync();
    }

    public async Task<Order> AddOrderAsync(Order order)
    {
        _context.Set<Order>().Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Order> UpdateOrderAsync(Order order)
    {
        if (!_context.Set<Order>().Local.Any(o => o.Id == order.Id))
        {
            _context.Set<Order>().Attach(order);
        }
        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task DeleteOrderAsync(int orderId)
    {
        using (TransactionScope scope = new TransactionScope())
        {
            var order = _context.Set<Order>().Include("OrderItems").Include("OrderItems.OrderItemOptions").FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                foreach (OrderItem item in order.OrderItems)
                {
                    foreach (var orderItemOption in item.Options)
                    {
                        _context.Set<OrderItemOption>().Remove(orderItemOption);
                    }
                    _context.Set<OrderItem>().Remove(item);
                }
                _context.Set<Order>().Remove(order);
            }
            await _context.SaveChangesAsync();
            scope.Complete();
        }
    }


    public async Task<List<Product22>> GetProductsAsync()
    {
        return await _context.Set<Product22>().ToListAsync();
    }

    public async Task<List<ProductOption>> GetProductOptionsAsync()
    {
        return await _context.Set<ProductOption>().ToListAsync();
    }

    public async Task<List<ProductSize>> GetProductSizesAsync()
    {
        return await _context.Set<ProductSize>().ToListAsync();
    }

    public async Task<List<OrderStatus>> GetOrderStatusesAsync()
    {
        return await _context.Set<OrderStatus>().ToListAsync();
    }
}
}
