using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.DTOs.Comments;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IMapper _mapper;
        public CommentService(ICommentRepository commentRepository,IMapper mapper)
        {
            _commentRepo = commentRepository;
            _mapper = mapper;
        }

        public async Task Create(AddCommentDto model)
        {
            await _commentRepo.Create(_mapper.Map<Comment>(model));
        }

        public async Task<List<CommentDto>> GetProductComments(int productId)
        {
            var datas = await _commentRepo.FindBy(m=>m.ProductId == productId, m=>m.User).ToListAsync();
            var comments = datas.Select(m => new CommentDto { Username = m.User.UserName, Comment = m.UserComment, CreatedDate = m.CreatedDate.ToString("dd MMMM yyyy"), Subject = m.Subject }).ToList();

            return comments;
        }

        public async Task<List<CommentDto>> GetAllComments()
        {
            var datas = await _commentRepo.FindAllWithIncludes(m => m.User,m=>m.Product).ToListAsync();
            var comments = datas.Select(m => new CommentDto {Id = m.Id, Username = m.User.UserName, Comment = m.UserComment, CreatedDate = m.CreatedDate.ToString("dd MMMM yyyy"), Subject = m.Subject,ProductName = m.Product.ProductName }).ToList();
            return comments;
        }

        public async Task DeleteComment(int commentId)
        {
            var existComment = await _commentRepo.GetById(commentId);
            await _commentRepo.Delete(existComment);
        }
    }
}
