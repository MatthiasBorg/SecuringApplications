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
            return _context.Comments.SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<Comment> GetComments()
        {
            return _context.Comments;
        }

        public Guid AddComment(Comment c)
        {
            c.Student = null;
            c.Teacher = null;
            c.StudentAssignment = null;
            _context.Comments.Add(c);
            _context.SaveChanges();
            return c.Id;
        }
    }
}