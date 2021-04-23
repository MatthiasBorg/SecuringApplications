using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface IAssignmentsService
    {
        IQueryable<AssignmentViewModel> GetAssignments();

        AssignmentViewModel GetAssignment(Guid id);

        Guid AddAssignment(AssignmentViewModel model);
        
        IQueryable<AssignmentViewModel> GetAssignmentsByTeacher(Guid id);

    }
}
