using InternetShop.BAL.Models;
using InternetShop.BAL.DTOs.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.BAL.Contracts
{
    public interface ICommentService
    {
        Task<Result> Like(int productId);
        Task<Result> Create(CommentDTO model);
        Task<Result> Delete(int commentId);
    }
}
