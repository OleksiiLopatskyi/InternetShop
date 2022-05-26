using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL.Entities
{
    public class Order
    {
        private ILazyLoader _lazyLoader;
        private ICollection<OrderDetail> _details;

        public Order()
        {
            
        }

        public Order(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public int Id { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverName { get; set; }
        public DateTime Date { get; set; }
        public DateTime ReceiveDate { get; set; }
        [BackingField(nameof(_details))]
        public ICollection<OrderDetail> Details 
        { 
            get => _lazyLoader.Load(this, ref _details);
            set => _details = value;
        }
        public decimal TotalPrice { get; set; }
    }
}
