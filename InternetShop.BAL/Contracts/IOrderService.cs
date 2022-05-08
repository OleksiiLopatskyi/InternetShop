using InternetShop.BAL.Models;
using InternetShop.BAL.DTOs.Order;
using InternetShop.DAL.QueryParams;
using InternetShop.DAL.Entities;

namespace InternetShop.BAL.Contracts
{
    public interface IOrderService
    {
        Task<Result<IEnumerable<Order>>> GetOrdersAsync(OrderSearchParameters searchParametersParameters,
            SortingParameters sortingParameters,
            PaginationParameters pagingParameters);
        Task<Result<Order>> GetByIdAsync(int orderId);
        Task<Result> CreateAsync(OrderDTO orderDto);
        Task<Result> UpdateAsync(int orderId, OrderDTO orderDto);
        Task<Result> DeleteAsync(int orderId);
    }
}
