using InternetShop.BAL.Models;
using InternetShop.BAL.DTOs.Product;
using InternetShop.DAL.QueryParams;
using InternetShop.BAL.DTOs.Rating;
using InternetShop.DAL.Entities;

namespace InternetShop.BAL.Contracts
{
    public interface IProductService
    {
        Task<PaginatedResult<Product>> GetProductsAsync(ProductSearchParameters searchParameters,
            SortingParameters sortingParameters,
            PaginationParameters pagingParameters);

        Task<Result> CreateAsync(ProductDTO dto);

        Task<Result<Product>> GetByIdAsync(int productId);

        Task<Result> UpdateAsync(int productId, ProductDTO dto);

        Task<Result> DeleteAsync(int productId);

        Task<Result> CreateRating(RatingDTO ratingDto);

        Task<Result<IEnumerable<double>>> GetSizesAsync();
    }
}
