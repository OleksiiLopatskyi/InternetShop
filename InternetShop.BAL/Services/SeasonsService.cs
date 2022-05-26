using InternetShop.BAL.Contracts;
using InternetShop.BAL.Models;
using InternetShop.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.BAL.Services
{
    public class SeasonsService : ISeasonsService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public SeasonsService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result<IEnumerable<string>>> GetSeasonsAsync()
        {
            try
            {
                var products = await _repositoryWrapper.ProductRepository.FindAllAsync();
                var seasons = products.Select(i => i.Season).Distinct();
                return new Result<IEnumerable<string>>
                {
                    Data = seasons
                };
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
