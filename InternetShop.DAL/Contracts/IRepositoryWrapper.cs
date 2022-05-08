using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL.Contracts
{
    public interface IRepositoryWrapper
    {
        IUserRepository UserRepository { get; }
        IProductRepository ProductRepository { get; }
        IGroupRepository GroupRepository { get; }
        IOrderRepository OrderRepository { get; }
        IDetailRepository DetailRepository { get; }
        ICommentRepository CommentRepository { get; }
        Task SaveAsync();
        void Save();
    }
}
