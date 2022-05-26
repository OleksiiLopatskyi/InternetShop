namespace InternetShop.BAL.DTOs.Order
{
    public class OrderDTO
    {
        public decimal TotalPrice { get; set; }
        public IEnumerable<OrderProductDTO> Products { get; set; }
    }
}
