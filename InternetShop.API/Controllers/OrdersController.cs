using Microsoft.AspNetCore.Mvc;
using InternetShop.BAL.DTOs.Order;
using InternetShop.DAL.Entities;
using InternetShop.BAL.Contracts;
using InternetShop.DAL.QueryParams;
using InternetShop.API.Validation;

namespace InternetShop.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class OrdersController : CustomControllerBase
    {
        private readonly IOrderService _orderService;
        private const string ORDERS_ENDPOINT = "orders";

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        
        [HttpGet(ORDERS_ENDPOINT)]
        public async Task<IActionResult> GetAllOrders([FromQuery] PaginationParameters pagingParameters,
            [FromQuery] SortingParameters sortingParameters,
            [FromQuery] OrderSearchParameters searchParameters)
        {
            var result = await _orderService
                .GetOrdersAsync(searchParameters, sortingParameters, pagingParameters);
            return CustomResult(result);
        }

        [RoleAuthorize(Role = Role.Admin)]
        [HttpGet(ORDERS_ENDPOINT+"/{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var result = await _orderService.GetByIdAsync(id);
            return CustomResult(result);
        }

        [RoleAuthorize(Role = Role.User)]
        [HttpPost(ORDERS_ENDPOINT)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDTO model)
        {
            var result = await _orderService.CreateAsync(GetUserId(), model);
            return CustomResult(result);
        }

        [RoleAuthorize(Role = Role.User)]
        [HttpGet("user/"+ORDERS_ENDPOINT)]
        public async Task<IActionResult> GetUserOrders([FromQuery]PaginationParameters paginationParameters)
        {
            var result = await _orderService.GetUserOrdersAsync(GetUserId(),paginationParameters);
            return CustomResult(result);
        }

        [RoleAuthorize(Role = Role.Admin)]
        [HttpPut(ORDERS_ENDPOINT+"/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDTO model)
        {
            var result = await _orderService.UpdateAsync(id, model);
            return CustomResult(result);
        }

        [RoleAuthorize(Role = Role.Admin)]
        [HttpDelete(ORDERS_ENDPOINT+"/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteAsync(id);
            return CustomResult(result);
        }
    }
}
