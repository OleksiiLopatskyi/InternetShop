using InternetShop.BAL.Contracts;
using InternetShop.BAL.Models;
using InternetShop.DAL.Contracts;
using InternetShop.BAL.DTOs.Comment;
using InternetShop.BAL.DTOs.Product;
using InternetShop.DAL.Entities;
using InternetShop.DAL.QueryParams;

namespace InternetShop.BAL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CommentService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result> Create(CommentDTO model)
        {
            try
            {
                var product = await _repositoryWrapper.ProductRepository
                    .FindEntityAsync(p => p.Id == model.ProductId, ProductProperties.Comments);
                if (product == null)
                {
                    return new Result
                    {
                        Message = "Product doesn't exists",
                        StatusCode = StatusCodes.NotFound
                    };
                }
                var comment = new Comment
                {
                    ProductId = product.Id,
                    Author = model.Author,
                    Likes = 0,
                    Text = model.Text
                };
                await _repositoryWrapper.CommentRepository.CreateAsync(comment);
                await _repositoryWrapper.SaveAsync();
                return new Result<Comment> { Data = comment };
            }
            catch (Exception ex)
            {
                return new Result
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.InternalServerError
                };
            }
        }

        public async Task<Result> Delete(int commentId)
        {
            try
            {
                var comment = await _repositoryWrapper.CommentRepository
                    .FindEntityAsync(c => c.Id == commentId);
                if (comment == null)
                {
                    return new Result
                    {
                        Message = "Product you want to delete doesn't exists",
                        StatusCode = StatusCodes.NotFound
                    };
                }
                _repositoryWrapper.CommentRepository.Delete(comment);
                await _repositoryWrapper.SaveAsync();
                return new Result { StatusCode = StatusCodes.Success };
            }
            catch (Exception ex)
            {
                return new Result
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.InternalServerError
                };
            }
        }

        public async Task<Result> Like(int commentId)
        {
            try
            {
                var comment = await _repositoryWrapper.CommentRepository
                    .FindEntityAsync(i => i.Id == commentId);
                if (comment == null)
                {
                    return new Result
                    {
                        Message = "Comment doesn't exists",
                        StatusCode = StatusCodes.NotFound
                    };
                }
                comment.Likes += 1;
                _repositoryWrapper.CommentRepository.Update(comment);
                await _repositoryWrapper.SaveAsync();
                return new Result { StatusCode = StatusCodes.Success };
            }
            catch (Exception ex)
            {
                return new Result
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.InternalServerError
                };
            }
        }
    }
}
