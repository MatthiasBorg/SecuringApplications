using System;
using System.Linq;
using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Data.Repositories
{
    public class TeachersRepository : ITeachersRepository
    {
        ShoppingCartDbContext _context;
        public TeachersRepository(ShoppingCartDbContext context)
        {
            _context = context;

        }
        
        public IQueryable<Teacher> GetTeachers()
        {
            return _context.Teachers;
        }

        public Guid AddTeacher(Teacher t)
        {
            _context.Teachers.Add(t);
            _context.SaveChanges();
            return t.Id;
        }
    }
}