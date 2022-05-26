using Microsoft.AspNetCore.Mvc;
using InternetShop.DAL.Entities;
using InternetShop.BAL.Contracts;
using InternetShop.API.Validation;
using InternetShop.BAL.DTOs.Brand;

namespace InternetShop.API.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandsController : CustomControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService groupService)
        {
            _brandService = groupService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _brandService.GetAllAsync();
            return CustomResult(result);
        }
    }
}
