using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Application.Services
{
    public class CommentsService : ICommentsService
    {
        
        private IMapper _mapper;
        private ICommentsRepository _commentsRepo;

        public CommentsService(ICommentsRepository commentsRepo
            ,  IMapper mapper
        )
        {
            _mapper = mapper;
            _commentsRepo = commentsRepo;
        }
        
        public CommentViewModel GetComment(Guid id)
        {
            var comment = _commentsRepo.GetComment(id);
            var result = _mapper.Map<CommentViewModel>(comment);
            return result;
        }

        public IQueryable<CommentViewModel> GetComments()
        {
            var comments = _commentsRepo.GetComments().ProjectTo<CommentViewModel>(_mapper.ConfigurationProvider);

            return comments;
        }

        public IQueryable<CommentViewModel> GetCommentsByAssignment(Guid id)
        {
            var comments = _commentsRepo.GetComments().Where(x => x.StudentAssignment.AssignmentId == id).ProjectTo<CommentViewModel>(_mapper.ConfigurationProvider);
            return comments;
        }

        public Guid AddComment(CommentViewModel c)
        {
            var newComment = _mapper.Map<Comment>(c);
            _commentsRepo.AddComment(newComment);
            return newComment.Id;
        }
    }
}