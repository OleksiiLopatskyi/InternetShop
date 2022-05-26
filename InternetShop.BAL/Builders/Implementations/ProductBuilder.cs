using InternetShop.BAL.Builders.Interfaces;
using InternetShop.BAL.DTOs.Product;
using InternetShop.DAL.Contracts;
using InternetShop.DAL.Entities;
using Microsoft.AspNetCore.Http;

namespace InternetShop.BAL.Builders.Implementations
{
    internal class ProductBuilder : IProductBuilder
    {
        private Product _product;

        public ProductBuilder()
        {
            _product = new Product();
            _product.Images = new List<Image>();
        }

        public IProductBuilder Map(ProductDTO dto)
        {
            _product.Name = dto.Name;
            _product.Brand = dto.Brand;
            _product.FullSize = dto.FullSize;
            _product.Size = dto.Size;
            _product.Season = dto.Season;
            _product.Color = dto.Color;
            _product.Rating = 0;
            _product.Price = dto.Price;
            _product.QuantityInStock = dto.QuantityInStock;
            return this;
        }

        public IProductBuilder WithImages(IEnumerable<string> images)
        {
            if (images == null)
            {
                return this;
            }
            foreach (var item in images)
            {
                var image = new Image
                {
                    Url = item
                };
                _product.Images.Add(image);
            }
            return this;

        }

        public Product Build()
        {
            return _product;
        }
    }
}
