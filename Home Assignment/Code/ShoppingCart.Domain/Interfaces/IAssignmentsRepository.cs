using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IAssignmentsRepository
    {
        IQueryable<Assignment> GetAssignments();

        Assignment GetAssignment(Guid id);
        
        Guid AddAssignment(Assignment a);
    }
}
