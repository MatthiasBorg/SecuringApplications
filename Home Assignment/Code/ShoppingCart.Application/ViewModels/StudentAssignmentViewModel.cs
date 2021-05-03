using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Application.ViewModels
{
    public class StudentAssignmentViewModel
    {
        public Guid Id { get; set; }
        
        public string File { get; set; }

        public String Signiture { get; set; }

        public String PubicKey { get; set; }

        [Required(ErrorMessage = "Please Enter If Was Submitted")]
        public bool Submitted { get; set; }
        
        [Required(ErrorMessage = "Please Enter Student")]
        public StudentViewModel Student { get; set; }
        
        [Required(ErrorMessage = "Please Enter Assignment")]
        public AssignmentViewModel Assignment { get; set; }
    }
}