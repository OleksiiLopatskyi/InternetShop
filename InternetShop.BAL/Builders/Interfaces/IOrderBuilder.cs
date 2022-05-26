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
        IOrderBuilder WithDetails(OrderDTO dto);
        IOrderBuilder WithDate();
        IOrderBuilder WithUser(User user);
        Order Build();
    }
}
