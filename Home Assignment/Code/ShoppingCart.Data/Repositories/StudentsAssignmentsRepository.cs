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
            return _context.StudentAssignments.SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<StudentAssignment> GetStudentAssignments()
        {
            return _context.StudentAssignments;
        }

        // public IQueryable<StudentAssignment> GetStudentAssignmentsById(Guid id)
        // {
        //     throw new NotImplementedException();
        // }

        public Guid AddStudentAssignment(StudentAssignment sa)
        {
            _context.StudentAssignments.Add(sa);
            _context.SaveChanges();
            return sa.Id;
        }

        public bool SubmitAssignment(string filePath, Guid id)
        {
            //throw new NotImplementedException();

            var assignemnt = GetStudentAssignment(id);
            assignemnt.File = filePath;
            assignemnt.Submitted = !assignemnt.Submitted;
            _context.StudentAssignments.Update(assignemnt);
            _context.SaveChanges();

            return assignemnt.Submitted;
        }
    }
}