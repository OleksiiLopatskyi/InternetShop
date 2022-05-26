using InternetShop.BAL.Models;
using InternetShop.BAL.DTOs.Order;
using InternetShop.DAL.QueryParams;
using InternetShop.DAL.Entities;

namespace InternetShop.BAL.Contracts
{
    public interface IOrderService
    {
        Task<PaginatedResult<Order>> GetOrdersAsync(OrderSearchParameters searchParametersParameters,
            SortingParameters sortingParameters,
            PaginationParameters pagingParameters);
        Task<PaginatedResult<Order>> GetUserOrdersAsync(int userId,PaginationParameters paginationParameters);
        Task<Result<Order>> GetByIdAsync(int orderId);
        Task<Result> CreateAsync(int userId, OrderDTO orderDto);
        Task<Result> UpdateAsync(int orderId, OrderDTO orderDto);
        Task<Result> DeleteAsync(int orderId);
    }
}
