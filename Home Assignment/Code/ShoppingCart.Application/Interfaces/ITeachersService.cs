using System;
using System.ComponentModel.DataAnnotations;
using ShoppingCart.Application.ViewModels;

namespace ShoppingCart.Application.Interfaces
{
    public interface ITeachersService
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