using InternetShop.DAL.Contracts;
using InternetShop.BAL.DTOs.Product;
using InternetShop.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.BAL.Builders.Interfaces
{
    internal interface IProductBuilder
    {
        IProductBuilder Map(ProductDTO productDto);
        IProductBuilder WithImages(IEnumerable<string> images);
        Product Build();
    }
}
