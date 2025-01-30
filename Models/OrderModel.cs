namespace OrderManagementAPI.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        public DateTime DateOrder { get; set; }

        public UserModel User { get; set; }

        public List<ProductModel> Products { get; set; }





    }
}
