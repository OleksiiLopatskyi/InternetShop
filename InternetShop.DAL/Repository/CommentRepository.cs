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
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(DatabaseContext dataContext) : base(dataContext)
        {
        }
    }
}
