using InternetShop.BAL.Models;
using InternetShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.BAL.Contracts
{
    public interface IOrderDetailService
    {
        Task<Result<IEnumerable<OrderDetail>>> GetDetailsAsync(int orderId);
        Task<Result> UpdateDetailsAsync(int orderId, IEnumerable<OrderDetail> orderDTOs);
        Task<Result> DeleteDetailAsync(int detailId);
        Task<Result> CreateDetailAsync(int orderId, OrderDetail orderDetail);
        Task<Result<OrderDetail>> GetDetailByIdAsync(int detailId);
    }
}
