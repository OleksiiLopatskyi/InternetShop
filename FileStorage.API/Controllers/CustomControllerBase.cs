using FileStorage.BAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    public class CustomControllerBase:ControllerBase
    {
        protected IActionResult CustomResult(Result result)
        {
            switch (result.StatusCode)
            {
                case BAL.Models.StatusCode.Success:
                    return Ok(result);
                case BAL.Models.StatusCode.InternalServerError:
                    return StatusCode(StatusCodes.Status500InternalServerError,result);
                case BAL.Models.StatusCode.BadRequest:
                    return BadRequest(result);
                default:
                    return Ok(result);
            }
        }
    }
}
