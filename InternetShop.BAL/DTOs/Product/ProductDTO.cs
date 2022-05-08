using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace InternetShop.BAL.DTOs.Product
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public int GroupId { get; set; }
        public IFormFileCollection Images { get; set; }
    }
}
