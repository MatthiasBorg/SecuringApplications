using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IStudentsRepository
    {
        IQueryable<Student> GetStudents();

        Guid AddStudent(Student s);
    }
}
