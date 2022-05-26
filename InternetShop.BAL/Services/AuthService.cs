using InternetShop.BAL.Models;
using InternetShop.DAL.Contracts;
using InternetShop.BAL.Contracts;
using InternetShop.BAL.DTOs.User;
using InternetShop.DAL.Entities;

using StatusCodes = InternetShop.BAL.Models.StatusCodes;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using InternetShop.BAL.Options;
using Microsoft.Extensions.Options;

namespace InternetShop.BAL.AuthService
{
    public class AuthService : IAuthService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private readonly JwtOptions _jwtOptions;
        public AuthService(IRepositoryWrapper repositoryWrapper,
            IOptions<JwtOptions> jwtOptions)
        {
            _repositoryWrapper = repositoryWrapper;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<Result<string>> Login(string email, string password)
        {
            var user = await _repositoryWrapper.UserRepository
                .FindEntityAsync(u => u.Email == email);
            if (user == null)
            {
                return new Result<string>
                {
                    Message = "Invalid login or password",
                    StatusCode = StatusCodes.BadRequest
                };
            }

            var verify = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!verify)
            {
                return new Result<string>
                {
                    Message = "Invalid login or password"
                };
            }
            
            return new Result<string>
            {
                Data = GenerateToken(user),
            };
        }

        public async Task<Result> Register(RegisterUserDTO model)
        {
            var user = await _repositoryWrapper.UserRepository
                .FindEntityAsync(u => u.Email == model.Email);
            if (user != null)
            {
                return new Result
                {
                    Message = "User already exists",
                    StatusCode = StatusCodes.BadRequest
                };
            }
            try
            {
                var createdUser = new User
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Role = Role.User,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
                };
                await _repositoryWrapper.UserRepository.CreateAsync(createdUser);
                await _repositoryWrapper.SaveAsync();
                return new Result<User> { Message = "User has been created", StatusCode = StatusCodes.Success };
            }
            catch (Exception ex)
            {
                return new Result
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.InternalServerError
                };
            }
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_jwtOptions.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim("Id", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
