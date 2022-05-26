using InternetShop.DAL.Entities;
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
            _order.Details = new List<OrderDetail>();
        }

        public Order Build()
        {
            return _order;
        }

        public IOrderBuilder WithDate()
        {
            _order.Date = DateTime.Now;
            _order.ReceiveDate = DateTime.Now.AddHours(2);
            return this;
        }

        public IOrderBuilder WithDetails(OrderDTO dto)
        {
            foreach (var item in dto.Products)
            {
                var detail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    Count = item.Count
                };
                _order.Details.Add(detail);
            }
            _order.TotalPrice = dto.TotalPrice;
            return this;
        }

        public IOrderBuilder WithUser(User user)
        {
            _order.ReceiverEmail = user.Email;
            _order.ReceiverName = $"{user.FirstName} {user.LastName}";
            return this;
        }
    }
}
