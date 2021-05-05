using System;
using System.Linq;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Application.Interfaces
{
    public interface IStudentAssignmentsService
    {
        StudentAssignmentViewModel GetStudentAssignment(Guid id);
        
        IQueryable<StudentAssignmentViewModel> GetStudentAssignments();
        
        IQueryable<StudentAssignmentViewModel> GetStudentAssignmentsById(Guid id);

        Guid AddStudentAssignment(StudentAssignmentViewModel sa);

        bool SubmitAssignment(String filePath, Guid id, String signiture, String publicKey, String privateKey, String key, String iv);
    }
}