using InternetShop.BAL.Models;
using InternetShop.BAL.DTOs.User;
using InternetShop.DAL.Entities;

namespace InternetShop.BAL.Contracts
{
    public interface IAuthService
    {
        Task<Result<string>>Login(string username, string password);
        Task<Result>Register(RegisterUserDTO user);
    }
}
