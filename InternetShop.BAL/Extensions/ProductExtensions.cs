using InternetShop.BAL.DTOs.Product;
using InternetShop.DAL.Entities;

namespace InternetShop.BAL.Extensions
{
    public static class ProductExtensions
    {
        public static Product MapFromDto(this Product product, ProductDTO productDto)
        {
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.QuantityInStock = productDto.QuantityInStock;
            product.Images = new List<Image>();
            product.Comments = new List<Comment>();
            product.Rating = new List<Rating>();
            product.GroupId = productDto.GroupId;
            return product;
        }
    }
}
