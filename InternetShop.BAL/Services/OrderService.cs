using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InternetShop.BAL.Contracts;
using System.Threading.Tasks;
using InternetShop.DAL.Entities;
using InternetShop.BAL.DTOs.Order;
using InternetShop.DAL.Contracts;
using InternetShop.BAL.Models;
using InternetShop.DAL.QueryParams;
using InternetShop.BAL.Builders.Implementations;

namespace InternetShop.BAL.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public OrderService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<PaginatedResult<Order>> GetOrdersAsync(OrderSearchParameters searchParameters,
            SortingParameters sortingParameters,
            PaginationParameters pagingParameters)
        {
            try
            {
                var orders = await _repositoryWrapper.OrderRepository
                    .FindAllAsync(searchParameters, sortingParameters, pagingParameters);
                return new PaginatedResult<Order>
                {
                    Items = orders,
                    Total = orders.Count
                };
            }
            catch (Exception ex)
            {
                return new PaginatedResult<Order>
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.InternalServerError
                };
            }
        }

        public async Task<Result<Order>> GetByIdAsync(int orderId)
        {
            try
            {
                var order = await _repositoryWrapper.OrderRepository
                    .FindEntityAsync(o => o.Id == orderId);
                if (order == null)
                {
                    return new Result<Order>
                    {
                        Message = "Order doesn't exists",
                        StatusCode = StatusCodes.NotFound
                    };
                }
                return new Result<Order> { Data = order };
            }
            catch (Exception ex)
            {
                return new Result<Order>
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.InternalServerError
                };
            }
        }

        public async Task<Result> CreateAsync(int userId, OrderDTO orderDto)
        {
            try
            {
                var user = await _repositoryWrapper.UserRepository.FindEntityAsync(u => u.Id == userId);
                var order = new OrderBuilder()
                    .WithDate()
                    .WithUser(user)
                    .WithDetails(orderDto)
                    .Build();
                await _repositoryWrapper.OrderRepository.CreateAsync(order);
                foreach (var product in orderDto.Products)
                {
                    var foundProduct = await _repositoryWrapper.ProductRepository
                        .FindEntityAsync(p => p.Id == product.ProductId);
                    if (product.Count > foundProduct.QuantityInStock)
                    {
                        return new Result
                        {
                            Message = "Недостатня кількість на складі",
                            StatusCode = StatusCodes.BadRequest
                        };
                    }
                    foundProduct.QuantityInStock -= product.Count;
                }
                await _repositoryWrapper.SaveAsync();
                return new Result<Order> { Data = order };
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

        public async Task<Result> UpdateAsync(int orderId, OrderDTO orderDto)
        {
            try
            {
                var order = await _repositoryWrapper.OrderRepository
                    .FindEntityAsync(o => o.Id == orderId);
                if (order == null)
                {
                    return new Result
                    {
                        Message = "Order doesn't exist",
                        StatusCode = StatusCodes.NotFound
                    };
                }
                var updatedOrder = new OrderBuilder()
                    .WithDetails(orderDto)
                    .Build();
                _repositoryWrapper.OrderRepository.Update(updatedOrder);
                await _repositoryWrapper.SaveAsync();
                return new Result<Order> { Data = order };
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

        public async Task<Result> DeleteAsync(int orderId)
        {
            try
            {
                var order = await _repositoryWrapper.OrderRepository
                    .FindEntityAsync(o => o.Id == orderId);
                if (order == null)
                {
                    return new Result
                    {
                        Message = "Order doesn't exists",
                        StatusCode = StatusCodes.NotFound
                    };
                }
                _repositoryWrapper.OrderRepository.Delete(order);
                await _repositoryWrapper.SaveAsync();
                return new Result<Order> { Data = order };
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

        public async Task<PaginatedResult<Order>> GetUserOrdersAsync(int userId,PaginationParameters paginationParameters)
        {
            try
            {
                var user = await _repositoryWrapper.UserRepository.FindEntityAsync(u => u.Id == userId);
                var orders = await _repositoryWrapper.OrderRepository
                    .FindByConditionAsync(o => o.ReceiverEmail == user.Email,paginationParameters);
                return new PaginatedResult<Order>
                {
                    Total = orders.Count,
                    Items = orders
                };
            }
            catch (Exception ex)
            {
                return new PaginatedResult<Order>
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.InternalServerError
                };
            }
        }
    }
}
