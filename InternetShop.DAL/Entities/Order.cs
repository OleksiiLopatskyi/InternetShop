using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverName { get; set; }
        public DateTime Date { get; set; }
        public DateTime ReceiveDate { get; set; }
        public ICollection<OrderDetail> Details { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
