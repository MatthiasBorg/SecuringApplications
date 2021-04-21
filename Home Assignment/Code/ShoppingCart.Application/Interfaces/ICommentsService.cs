using System;
using System.Linq;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Application.Interfaces
{
    public interface ICommentsService
    {
        CommentViewModel GetComment(Guid id);
        
        IQueryable<CommentViewModel> GetComments();
        
        IQueryable<CommentViewModel> GetCommentsByAssignment(Guid id);

        Guid AddComment(CommentViewModel c);
    }
}