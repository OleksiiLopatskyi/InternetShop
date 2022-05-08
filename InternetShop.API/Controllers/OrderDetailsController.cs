using InternetShop.BAL.Contracts;
using InternetShop.API.Validation;
using InternetShop.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.API.Controllers
{
    [Route("api/details")]
    [ApiController]
    [RoleAuthorize(Role = Role.Admin)]
    public class OrderDetailsController : CustomControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailsController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetDetails(int orderId)
        {
            var result = await _orderDetailService.GetDetailsAsync(orderId);
            return CustomResult(result);
        }

        [HttpGet("{detailId}")]
        public async Task<IActionResult> GetDetail(int detailId)
        {
            var result = await _orderDetailService.GetDetailByIdAsync(detailId);
            return CustomResult(result);
        }

        [HttpDelete("{detailId}")]
        public async Task<IActionResult> DeleteDetail(int detailId)
        {
            var result = await _orderDetailService.DeleteDetailAsync(detailId);
            return CustomResult(result);
        }
    }
}
