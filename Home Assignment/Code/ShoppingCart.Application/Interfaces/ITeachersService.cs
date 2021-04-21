using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Application.Interfaces
{
    public interface ITeachersService
    {
        IQueryable<TeacherViewModel> GetTeachers();
        
        Guid AddTeacher(TeacherViewModel t);
    }
}