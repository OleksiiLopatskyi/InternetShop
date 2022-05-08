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
        public int QuantityInStock { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public ICollection<Rating> Rating { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
