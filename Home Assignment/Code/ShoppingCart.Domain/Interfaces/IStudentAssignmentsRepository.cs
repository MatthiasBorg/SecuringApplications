using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IStudentAssignmentsRepository
    {
        StudentAssignment GetStudentAssignment(Guid id);
        
        IQueryable<StudentAssignment> GetStudentAssignments();
        
        IQueryable<StudentAssignment> GetStudentAssignmentsById(Guid id);

        Guid AddStudentAssignment(Comment c);

        bool SubmitAssignment(String filePath);
    }
}
