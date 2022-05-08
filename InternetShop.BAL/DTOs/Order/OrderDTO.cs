namespace InternetShop.BAL.DTOs.Order
{
    public class OrderDTO
    {
        public OrderReceiverDTO Receiver { get; set; }
        public OrderDateDTO Date { get; set; }
        public IEnumerable<OrderProductDTO> Products { get; set; }
    }
}
