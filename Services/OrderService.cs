using OrderManagementAPI.Models;
using OrderManagementAPI.Repositories.Order;

namespace OrderManagementAPI.Services
{
    public class OrderService
    {

        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            orderRepository = _orderRepository;
        }

        public async Task<OrderModel> CreateOrderAsync(OrderModel order)
        {
            var createOrder = await _orderRepository.CreateOrderAsync(order);
            return createOrder;
        } 

        public async Task<OrderModel> GetOrderByIdAsync (int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            return order;
        }

        public async Task<List<OrderModel>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return orders;

        }


    }
}
