using InternetShop.BAL.Models;
using InternetShop.BAL.DTOs.User;
using InternetShop.DAL.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using InternetShop.BAL.Contracts;

namespace InternetShop.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : CustomControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO model)
        {
            var result = await _authService.Login(model.Email, model.Password);
            return CustomResult(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUser)
        {
            var result = await _authService.Register(registerUser);
            return CustomResult(result);
        }
    }
}
