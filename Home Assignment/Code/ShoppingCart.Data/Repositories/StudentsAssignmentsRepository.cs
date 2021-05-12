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

        public Guid AddStudentAssignment(StudentAssignment sa)
        {
            sa.Student = null;
            sa.Assignment = null;
            _context.StudentAssignments.Add(sa);
            _context.SaveChanges();
            return sa.Id;
        }

        public bool SubmitAssignment(string file, Guid id, String signiture, String publicKey, String privateKey, String key, String Iv)
        {
            var assignemnt = GetStudentAssignment(id);
            assignemnt.File = file;
            assignemnt.Submitted = !assignemnt.Submitted;
            assignemnt.Signiture = signiture;
            assignemnt.PublicKey = publicKey;
            assignemnt.PrivateKey = privateKey;
            assignemnt.Key = key;
            assignemnt.Iv = Iv;
            _context.StudentAssignments.Update(assignemnt);
            _context.SaveChanges();

            return assignemnt.Submitted;
        }
    }
}