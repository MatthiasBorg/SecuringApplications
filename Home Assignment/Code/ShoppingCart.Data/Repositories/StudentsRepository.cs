using System;
using System.Linq;
using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Data.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        ShoppingCartDbContext _context;
        public StudentsRepository(ShoppingCartDbContext context)
        {
            _context = context;

        }
        
        public IQueryable<Student> GetStudents()
        {
            return _context.Students;
        }

        public Student GetStudentByEmail(string email)
        {
            return _context.Students.SingleOrDefault(x => x.Email == email);
        }

        public Guid AddStudent(Student s)
        {
            s.Teacher = null;
            _context.Students.Add(s);
            _context.SaveChanges();
            return s.Id;
        }
    }
}