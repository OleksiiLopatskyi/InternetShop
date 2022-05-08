using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShop.DAL.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}
