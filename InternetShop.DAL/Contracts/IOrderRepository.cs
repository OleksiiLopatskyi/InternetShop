using InternetShop.DAL.QueryParams;
using InternetShop.DAL.Entities;


namespace InternetShop.DAL.Contracts
{
    public interface IOrderRepository:IRepositoryBase<Order>,ISearchable<Order, OrderSearchParameters>
    {
    }
}
