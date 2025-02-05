using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Infra;
using OrderManagementAPI.Models;

namespace OrderManagementAPI.Repositories.Order
{
    public class OrderRepository : IOrderRepository 
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            context = _context;
        }

        public async Task<OrderModel> CreateOrderAsync(OrderModel order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); 
            return order;
        }

        public async Task<List<OrderModel>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product).ToListAsync();
        }

        public async Task<OrderModel> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include (o => o.Items)
                .ThenInclude (i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
