using InternetShop.BAL.DTOs.Brand;
using InternetShop.BAL.Models;
using InternetShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.BAL.Contracts
{
    public interface IBrandService
    {
        Task<Result<IEnumerable<string>>> GetAllAsync();
    }
}
