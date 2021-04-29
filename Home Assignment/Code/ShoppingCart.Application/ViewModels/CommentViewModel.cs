using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Application.ViewModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please Enter Comment Content")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Please Enter Timestamp")]
        public DateTime Timestamp { get; set; }
        
        public TeacherViewModel Teacher { get; set; }

        public StudentViewModel Student { get; set; }

        [Required(ErrorMessage = "Please Enter StudentAssignment")]
        public StudentAssignmentViewModel StudentAssignment { get; set; }
    }
}