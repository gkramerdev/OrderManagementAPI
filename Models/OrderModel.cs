namespace OrderManagementAPI.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        public DateTime DateOrder { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }

        public UserModel User { get; set; }

        public List<OrderItemModel> Items { get; set; }





    }
}
