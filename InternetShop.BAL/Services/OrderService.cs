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

        public async Task<Result<IEnumerable<Order>>> GetOrdersAsync(OrderSearchParameters searchParameters,
            SortingParameters sortingParameters,
            PaginationParameters pagingParameters)
        {
            try
            {
                var orders = await _repositoryWrapper.OrderRepository
                    .FindAllAsync(searchParameters, sortingParameters, pagingParameters);
                return new Result<IEnumerable<Order>> { Data = orders };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<Order>>
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

        public async Task<Result> CreateAsync(OrderDTO orderDto)
        {
            try
            {
                var order = new OrderBuilder()
                .Map(orderDto)
                .WithDetails(orderDto.Products)
                .Build();
                await _repositoryWrapper.OrderRepository.CreateAsync(order);
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
                    .Map(orderDto)
                    .WithDetails(orderDto.Products)
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
    }
}
