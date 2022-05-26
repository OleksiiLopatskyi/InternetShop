using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShop.DAL.Entities
{
    public class OrderDetail
    {
        private ILazyLoader _lazyLoader;
        private Product _product;

        public OrderDetail()
        {

        }

        public OrderDetail(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [BackingField(nameof(_product))]
        public Product Product 
        {
            get => _lazyLoader.Load(this,ref _product);
            set => _product = value;
        }
        public int Count { get; set; }
    }
}
