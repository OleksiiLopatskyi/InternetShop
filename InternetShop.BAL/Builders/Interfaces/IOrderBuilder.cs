using InternetShop.BAL.DTOs.Order;
using InternetShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.BAL.Builders.Interfaces
{
    public interface IOrderBuilder
    {
        IOrderBuilder Map(OrderDTO order);
        IOrderBuilder WithDetails(IEnumerable<OrderProductDTO> details);
        Order Build();
    }
}
