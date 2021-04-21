﻿using System;
using System.Linq;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Application.Interfaces
{
    public interface IStudentsService
    {
        IQueryable<StudentViewModel> GetStudents();

        Guid AddStudent(StudentViewModel s);
    }
}