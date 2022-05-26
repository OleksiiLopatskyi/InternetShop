using InternetShop.BAL.Models;
using Microsoft.AspNetCore.Mvc;
using StatusCodes = InternetShop.BAL.Models.StatusCodes;

namespace InternetShop.API.Controllers
{
    public class CustomControllerBase : ControllerBase
    {
        protected int GetUserId()
        {
            return Int32.Parse(User.Claims.First(u => u.Type == "Id").Value);
        }
        protected IActionResult CustomResult(Result result)
        {
            switch (result.StatusCode)
            {
                case StatusCodes.ValidationError:
                    return BadRequest(result);
                case StatusCodes.NotFound:
                    return NotFound();
                case StatusCodes.InternalServerError:
                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError,result);
                case StatusCodes.BadRequest:
                    return BadRequest(result);
                default:
                    return Ok(result);
            }
        }
    }
}
