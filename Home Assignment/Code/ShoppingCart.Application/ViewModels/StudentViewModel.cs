using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Application.ViewModels
{
    public class StudentViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Please Enter First Name")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Password")] 
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Teacher")]
        public TeacherViewModel Teacher { get; set; }
    }
}