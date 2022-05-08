using InternetShop.DAL.Contracts;
using InternetShop.DAL.DataContext;
using InternetShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL.Repository
{
    public class UserRepository:RepositoryBase<User>,IUserRepository
    {
        public UserRepository(DatabaseContext databaseContext):base(databaseContext)
        {
        }
    }
}
