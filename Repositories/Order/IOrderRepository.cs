using OrderManagementAPI.Models;

namespace OrderManagementAPI.Repositories.Order
{
    public interface IOrderRepository
    {

        Task<OrderModel> CreateOrderAsync(OrderModel order);
        Task<OrderModel> GetOrderByIdAsync(int id);
        Task<List<OrderModel>> GetAllOrdersAsync();
    }
}
