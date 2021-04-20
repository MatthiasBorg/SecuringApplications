using System;
using System.Linq;
using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Data.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        ShoppingCartDbContext _context;
        public CommentsRepository(ShoppingCartDbContext context)
        {
            _context = context;

        }
        
        public Comment GetComment(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Comment> GetComments()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Comment> GetCommentsByAssignment(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid AddComment(Comment c)
        {
            throw new NotImplementedException();
        }
    }
}