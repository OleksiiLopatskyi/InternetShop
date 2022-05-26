using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace InternetShop.BAL.DTOs.Product
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string FullSize { get; set; }
        public string Season { get; set; }
        public double Size { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public IFormFileCollection Images { get; set; }
    }
}
