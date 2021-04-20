using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Application.ViewModels
{
    public class AssignmentViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please Enter Assignment Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Enter Assignment Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please Enter Publish Date")]
        public String PublishedDate { get; set; }

        [Required(ErrorMessage = "Please Enter Deadline date")]
        public String Deadline { get; set; }
        
        public TeacherViewModel Teacher { get; set; }

    }
}
