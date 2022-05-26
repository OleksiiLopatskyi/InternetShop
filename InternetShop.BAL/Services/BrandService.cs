using InternetShop.DAL.Contracts;
using InternetShop.BAL.Contracts;
using InternetShop.BAL.DTOs.Brand;
using InternetShop.DAL.Entities;
using InternetShop.BAL.Models;

namespace InternetShop.BAL.Services.GroupService
{
    public class BrandService : IBrandService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public BrandService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result<IEnumerable<string>>> GetAllAsync()
        {
            try
            {
                var products = await _repositoryWrapper.ProductRepository.FindAllAsync();
                return new Result<IEnumerable<string>> { Data = products.Select(b=>b.Brand).Distinct()};
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<string>>
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.InternalServerError
                };
            }
        }
    }
}