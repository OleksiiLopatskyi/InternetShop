using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int QuantityInStock { get; set; }
        public double Size { get; set; }
        public string Name { get; set; }
        public string FullSize { get; set; }
        public string Color { get; set; }
        public string Season { get; set; }
        public decimal Price { get; set; }
        public decimal NewPrice { get; set; }
        public double Rating { get; set; }
        public string Brand { get; set; }
        public ICollection<Image> Images { get; set; }

        public Product()
        {
            Images = new List<Image>();
        }
    }
}
