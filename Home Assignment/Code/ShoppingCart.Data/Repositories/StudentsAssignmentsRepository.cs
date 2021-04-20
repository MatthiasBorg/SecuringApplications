using System;
using System.Linq;
using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Data.Repositories
{
    public class StudentsAssignmentsRepository : IStudentAssignmentsRepository
    {
        ShoppingCartDbContext _context;
        public StudentsAssignmentsRepository(ShoppingCartDbContext context)
        {
            _context = context;

        }
        
        public StudentAssignment GetStudentAssignment(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StudentAssignment> GetStudentAssignments()
        {
            throw new NotImplementedException();
        }

        public IQueryable<StudentAssignment> GetStudentAssignmentsById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid AddStudentAssignment(Comment c)
        {
            throw new NotImplementedException();
        }

        public bool SubmitAssignment(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}