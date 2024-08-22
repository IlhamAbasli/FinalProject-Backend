using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Comments;
using Service.Services.Interfaces;

namespace FinalProject_Back.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromForm] AddCommentDto request)
        {
            await _commentService.Create(request);
            return Ok(request);
        }

        [HttpGet]
        public async Task<IActionResult> GetComments([FromQuery] int productId)
        {
            var comments = await _commentService.GetProductComments(productId);
            return Ok(comments);    
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            return Ok(await _commentService.GetAllComments());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int commentId)
        {
            await _commentService.DeleteComment(commentId);
            return Ok();
        }
    }
}
