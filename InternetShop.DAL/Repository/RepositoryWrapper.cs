using InternetShop.DAL.Contracts;
using InternetShop.DAL.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DatabaseContext _dataContext;
        private IUserRepository _userRepository;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private IDetailRepository _detailRepository;

        public RepositoryWrapper(DatabaseContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_dataContext);

                return _userRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_dataContext);

                return _productRepository;
            }
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                    return _orderRepository = new OrderRepository(_dataContext);

                return _orderRepository;
            }
        }

        public IDetailRepository DetailRepository
        {
            get
            {
                if (_detailRepository == null)
                    return new DetailRepository(_dataContext);
                return _detailRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _dataContext.SaveChangesAsync();
        }

        public void Save()
        {
            _dataContext.SaveChanges();
        }
    }
}
