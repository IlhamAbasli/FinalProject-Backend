using Service.DTOs.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ICommentService
    {
        Task Create(AddCommentDto model);
        Task<List<CommentDto>> GetProductComments(int productId);
        Task<List<CommentDto>> GetAllComments();
        Task DeleteComment(int commentId);
    }
}
