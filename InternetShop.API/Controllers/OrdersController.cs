using Microsoft.AspNetCore.Mvc;
using InternetShop.BAL.DTOs.Order;
using InternetShop.DAL.Entities;
using InternetShop.BAL.Contracts;
using InternetShop.DAL.QueryParams;
using InternetShop.API.Validation;

namespace InternetShop.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : CustomControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [RoleAuthorize(Role = Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery]PaginationParameters pagingParameters,
            [FromQuery]SortingParameters sortingParameters,
            [FromQuery]OrderSearchParameters searchParameters)
        {
            var result = await _orderService
                .GetOrdersAsync(searchParameters, sortingParameters, pagingParameters);
            return CustomResult(result);
        }

        [RoleAuthorize(Role = Role.Admin)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var result = await _orderService.GetByIdAsync(id);
            return CustomResult(result);
        }

        [RoleAuthorize(Role = Role.User)]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDTO model)
        {
            var result = await _orderService.CreateAsync(model);
            return CustomResult(result);
        }

        [RoleAuthorize(Role = Role.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.UpdateAsync(id, model);
            return CustomResult(result);
        }

        [RoleAuthorize(Role = Role.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteAsync(id);
            return CustomResult(result);
        }
    }
}
