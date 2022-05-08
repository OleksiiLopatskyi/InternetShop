using InternetShop.DAL.Entities;
using InternetShop.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InternetShop.BAL.Extensions;
using InternetShop.BAL.DTOs.Order;
using InternetShop.BAL.Builders.Interfaces;

namespace InternetShop.BAL.Builders.Implementations
{
    public class OrderBuilder : IOrderBuilder
    {
        private Order _order;

        public OrderBuilder()
        {
            _order = new Order();
        }

        public Order Build()
        {
            CalculatePrice();
            return _order;
        }

        public IOrderBuilder WithDetails(IEnumerable<OrderProductDTO> details)
        {
            foreach (var item in details)
            {
                var detail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    Count = item.Count
                };
                _order.Details.Add(detail);
            }
            return this;
        }

        public IOrderBuilder Map(OrderDTO dto)
        {
            _order.ReceiverEmail = dto.Receiver.Email;
            _order.ReceiverName = dto.Receiver.Name;
            _order.Date = dto.Date.OrderDate;
            _order.ReceiveDate = dto.Date.ReceiveDate;
            return this;
        }

        private void CalculatePrice()
        {
            _order.TotalPrice = _order.Details.Sum(o => o.Product.Price * o.Count);
        }
    }
}
