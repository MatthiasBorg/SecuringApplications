using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Application.ViewModels
{
    public class TeacherViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Please Enter First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }
    }
}