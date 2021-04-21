using AutoMapper;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.AutoMapper
{
    public class DomainToViewModelProfile: Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<Assignment, AssignmentViewModel>();
            CreateMap<Student, StudentViewModel>();
            CreateMap<StudentAssignment, StudentAssignmentViewModel>();
            CreateMap<Teacher, TeacherViewModel>();
            CreateMap<Comment, CommentViewModel>();
        }

    }
}
