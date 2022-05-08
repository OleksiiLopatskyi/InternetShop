using InternetShop.BAL.Contracts;
using InternetShop.BAL.Models;
using InternetShop.DAL.Contracts;
using InternetShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.BAL.Services.OrderDetailService
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public OrderDetailService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result<IEnumerable<OrderDetail>>> GetDetailsAsync(int orderId)
        {
            try
            {
                var order = await _repositoryWrapper.OrderRepository
                    .FindEntityAsync(i => i.Id == orderId);
                if (order == null)
                {
                    return new Result<IEnumerable<OrderDetail>>
                    {
                        Message = "Order doesn't exists",
                        StatusCode = StatusCodes.NotFound
                    };
                }
                return new Result<IEnumerable<OrderDetail>> { Data = order.Details };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<OrderDetail>>
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.InternalServerError
                };
            }
        }

        public async Task<Result> UpdateDetailsAsync(int orderId, IEnumerable<OrderDetail> orderDetails)
        {
            try
            {
                var order = await _repositoryWrapper.OrderRepository.FindEntityAsync(o => o.Id == orderId);
                order.Details = (ICollection<OrderDetail>)orderDetails;
                _repositoryWrapper.OrderRepository.Update(order);
                await _repositoryWrapper.SaveAsync();
                return new Result<ICollection<OrderDetail>> { Data = order.Details };
            }
            catch (Exception ex)
            {
                return new Result { Message = ex.Message, StatusCode = StatusCodes.InternalServerError };
            }
        }

        public async Task<Result> DeleteDetailAsync(int detailId)
        {
            try
            {
                var detail = await _repositoryWrapper.DetailRepository
                    .FindEntityAsync(i => i.Id == detailId);
                if (detail == null)
                {
                    return new Result
                    {
                        Message = "Detail doesn't exist",
                        StatusCode = StatusCodes.NotFound
                    };
                }
                _repositoryWrapper.DetailRepository.Delete(detail);
                await _repositoryWrapper.SaveAsync();
                return new Result<OrderDetail> { Data = detail };
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

        public async Task<Result> CreateDetailAsync(int orderId, OrderDetail orderDetail)
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
                order.Details.Add(orderDetail);
                _repositoryWrapper.OrderRepository.Update(order);
                await _repositoryWrapper.SaveAsync();
                return new Result<OrderDetail> { Data = orderDetail };
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

        public async Task<Result<OrderDetail>> GetDetailByIdAsync(int detailId)
        {
            try
            {
                var detail = await _repositoryWrapper.DetailRepository
                    .FindEntityAsync(i => i.Id == detailId);
                if (detail == null)
                {
                    return new Result<OrderDetail>
                    {
                        Message = "Detail doesn't exists",
                        StatusCode = StatusCodes.NotFound
                    };
                }
                return new Result<OrderDetail>
                {
                    Data = detail
                };
            }
            catch (Exception ex)
            {
                return new Result<OrderDetail>
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.InternalServerError
                };
            }
        }
    }
}