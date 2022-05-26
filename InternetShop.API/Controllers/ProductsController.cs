using Microsoft.AspNetCore.Mvc;
using InternetShop.BAL.Contracts;
using InternetShop.BAL.DTOs.Rating;
using InternetShop.DAL.QueryParams;
using InternetShop.API.Validation;
using InternetShop.DAL.Entities;
using InternetShop.BAL.DTOs.Product;

namespace InternetShop.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : CustomControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] PaginationParameters pagingParameters,
            [FromQuery] SortingParameters sortingParameters,
            [FromQuery] ProductSearchParameters searchParameters)
        {
            var result = await _productService
                .GetProductsAsync(searchParameters, sortingParameters, pagingParameters);
            return CustomResult(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            return CustomResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductDTO dto)
        {
            var result = await _productService.CreateAsync(dto);
            return CustomResult(result);
        }

        [RoleAuthorize(Role = Role.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDTO dto)
        {
            var result = await _productService.UpdateAsync(id, dto);
            return CustomResult(result);
        }

        [RoleAuthorize(Role = Role.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteAsync(id);
            return CustomResult(result);
        }

        [RoleAuthorize(Role = Role.User)]
        [HttpPut("rate")]
        public async Task<IActionResult> Rate(RatingDTO model)
        {
            var result = await _productService.CreateRating(model);
            return CustomResult(result);
        }

    }
}
