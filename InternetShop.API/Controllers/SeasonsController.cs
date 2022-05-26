using InternetShop.API.Validation;
using InternetShop.BAL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.API.Controllers
{
    [Route("api/seasons")]
    [ApiController]
    public class SeasonsController : CustomControllerBase
    {
        private readonly ISeasonsService _seasonsService;

        public SeasonsController(ISeasonsService seasonsService)
        {
            _seasonsService = seasonsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSeasons()
        {
            var result = await _seasonsService.GetSeasonsAsync();
            return CustomResult(result);
        }
    }
}
