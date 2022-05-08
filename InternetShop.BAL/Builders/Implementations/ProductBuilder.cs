using InternetShop.BAL.Builders.Interfaces;
using InternetShop.BAL.DTOs.Product;
using InternetShop.BAL.Extensions;
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
            _product.Description = dto.Description;
            _product.Price = dto.Price;
            _product.QuantityInStock = dto.QuantityInStock;
            _product.GroupId = dto.GroupId;
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
