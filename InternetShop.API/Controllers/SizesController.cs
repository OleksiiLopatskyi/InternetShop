using InternetShop.BAL.Contracts;
using InternetShop.BAL.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.API.Controllers
{
    [Route("api/sizes")]
    [ApiController]
    public class SizesController : CustomControllerBase
    {
        private readonly IProductService _productService;

        public SizesController(IProductService productService)
        {
            _productService = productService;
        }



        [HttpGet]
        public async Task<IActionResult> Sizes()
        {
            var result = await _productService.GetSizesAsync();
            return CustomResult(result);
        }
    }
}
