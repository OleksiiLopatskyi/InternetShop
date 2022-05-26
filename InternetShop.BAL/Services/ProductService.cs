using InternetShop.BAL.Builders.Implementations;
using InternetShop.BAL.Contracts;
using InternetShop.BAL.Models;
using InternetShop.DAL.Contracts;
using InternetShop.BAL.DTOs.Product;
using InternetShop.DAL.QueryParams;
using InternetShop.BAL.DTOs.Rating;
using InternetShop.DAL.Entities;
using Microsoft.AspNetCore.Http;
using StatusCodes = InternetShop.BAL.Models.StatusCodes;

namespace InternetShop.BAL.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IImageUploader _imageUploader;
        public ProductService(IRepositoryWrapper repositoryWrapper, IImageUploader imageUploader)
        {
            _repositoryWrapper = repositoryWrapper;
            _imageUploader = imageUploader;
        }
        public async Task<PaginatedResult<Product>> GetProductsAsync(
            ProductSearchParameters searchParameters,
            SortingParameters sortingParameters,
            PaginationParameters pagingParameters)
        {
            try
            {
                var products = await _repositoryWrapper.ProductRepository
                    .FindAllAsync(searchParameters,
                    sortingParameters,
                    pagingParameters);
                return new PaginatedResult<Product>
                {
                    Items = products,
                    Total = products.Count,
                };
            }
            catch (Exception ex)
            {
                return new PaginatedResult<Product>
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.InternalServerError
                };
            }
        }

        public async Task<Result> CreateAsync(ProductDTO dto)
        {
            try
            {
                var uploadResult = await _imageUploader.UploadAsync(dto.Images);
                if (uploadResult.Data == null)
                {
                    return uploadResult;
                }
                var product = new ProductBuilder()
                    .Map(dto)
                    .WithImages(uploadResult.Data)
                    .Build();

                await _repositoryWrapper.ProductRepository.CreateAsync(product);
                await _repositoryWrapper.SaveAsync();
                return new Result<Product> { Data = product };
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

        public async Task<Result> DeleteAsync(int productId)
        {
            try
            {
                var product = await _repositoryWrapper.ProductRepository
                    .FindEntityAsync(p => p.Id == productId);
                if (product == null)
                {
                    return new Result
                    {
                        Message = "Product doesn't exists",
                        StatusCode = StatusCodes.NotFound
                    };
                }
                _repositoryWrapper.ProductRepository.Delete(product);
                await _repositoryWrapper.SaveAsync();
                return new Result<Product> { Data = product };
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

        public async Task<Result<Product>> GetByIdAsync(int productId)
        {
            try
            {
                var product = await _repositoryWrapper.ProductRepository
                    .FindEntityAsync(i => i.Id == productId,
                    ProductProperties.Images);
                if (product == null)
                {
                    return new Result<Product>
                    {
                        Message = "Products doesn't exists",
                        StatusCode = StatusCodes.NotFound
                    };
                }
                return new Result<Product> { Data = product };
            }
            catch (Exception ex)
            {
                return new Result<Product>
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.InternalServerError
                };
            }
        }

        public async Task<Result> UpdateAsync(int productId, ProductDTO dto)
        {
            try
            {
                var foundProduct = await _repositoryWrapper.ProductRepository
                    .FindEntityAsync(i => i.Id == productId, ProductProperties.Images);
                if (foundProduct == null)
                {
                    return new Result
                    {
                        Message = "Product doesn't exists",
                        StatusCode = StatusCodes.NotFound
                    };
                }
                var uploadResult = await _imageUploader.UploadAsync(dto.Images);
                if (uploadResult.Data == null)
                {
                    return uploadResult;
                }
                var updatedProduct = new ProductBuilder()
                    .Map(dto)
                    .WithImages(uploadResult.Data)
                    .Build();
                _repositoryWrapper.ProductRepository.Update(updatedProduct);
                await _repositoryWrapper.SaveAsync();
                return new Result<Product> { Data = updatedProduct };
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

        public async Task<Result> CreateRating(RatingDTO ratingDto)
        {
            try
            {
                var product = await _repositoryWrapper.ProductRepository
                    .FindEntityAsync(p => p.Id == ratingDto.ProductId);
                if (product == null)
                {
                    return new Result
                    {
                        Message = "Product doesn't exists",
                        StatusCode = StatusCodes.NotFound
                    };
                }
                _repositoryWrapper.ProductRepository.Update(product);
                await _repositoryWrapper.SaveAsync();
                return new Result<Product> { Data = product };
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

        public async Task<Result<IEnumerable<double>>> GetSizesAsync()
        {
            try
            {
                var products = await _repositoryWrapper.ProductRepository.FindAllAsync();
                var sizes = products.Select(i => i.Size).Distinct();
                return new Result<IEnumerable<double>>
                {
                    Data = sizes
                };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<double>>
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.InternalServerError
                };
            }
        }
    }
}
