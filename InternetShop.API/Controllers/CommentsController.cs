using InternetShop.BAL.Contracts;
using InternetShop.BAL.DTOs.Comment;
using InternetShop.API.Validation;
using InternetShop.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.API.Controllers
{
    [RoleAuthorize(Role = Role.User)]
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : CustomControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(int productId, CommentDTO model)
        {
            var result = await _commentService.Create(model);
            return CustomResult(result);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var result = await _commentService.Delete(commentId);
            return CustomResult(result);
        }

        [HttpPut("{commentId}/like")]
        public async Task<IActionResult> LikeComment(int commentId)
        {
            var result = await _commentService.Like(commentId);
            return CustomResult(result);
        }
    }
}
