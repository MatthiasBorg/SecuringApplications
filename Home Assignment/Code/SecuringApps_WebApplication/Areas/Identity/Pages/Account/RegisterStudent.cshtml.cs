using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using PasswordGenerator;
using SecuringApps_WebApplication.Data;
using ShoppingCart.Application.Helpers;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;

namespace SecuringApps_WebApplication.Areas.Identity.Pages.Account
{
    public class RegisterStudentModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ITeachersService _teachersService;
        private readonly IStudentsService _studentsService;

        public RegisterStudentModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ITeachersService teachersService,
            IStudentsService studentsService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _teachersService = teachersService;
            _studentsService = studentsService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        //public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Name")]
            public string Name { get; set; }
            
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "LastName")]
            public string LastName { get; set; }
            
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            public string Password { get; set; }

            [Required]
            public string Address { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, EmailConfirmed = true};
                Input.Password = new Password().Next();
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    
                    var roleResult = await _userManager.AddToRoleAsync(user, "Student");
                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError("",
                            "Error while allocating role!");
                    }

                    _logger.LogInformation($"User {User.Identity.Name} Added A Student - Time: {DateTime.Now} - IP Address: {HttpContext.Connection.RemoteIpAddress}");

                    // add student
                    TeacherViewModel teacher = _teachersService.GetTeacherByEmail(User.Identity.Name);
                    StudentViewModel newStudent = new StudentViewModel();
                    newStudent.Id = new Guid(user.Id);
                    newStudent.Email = Input.Email;
                    newStudent.FirstName = Input.Name;
                    newStudent.LastName = Input.Name;
                    newStudent.Password = user.PasswordHash;
                    string asdasd = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    newStudent.Teacher = teacher;

                    _studentsService.AddStudent(newStudent);

                    string emailSubject = $"Welcome {newStudent.FirstName} {newStudent.LastName}";
                    string emailBody = $"Dear {newStudent.FirstName} {newStudent.LastName},\n" +
                                       $"Your Login Credentials Are:\n" +
                                       $"Email: {newStudent.Email}\n" +
                                       $"Password: {Input.Password}\n";

                    EmailHelper emailHelper = new EmailHelper(newStudent.Email, emailSubject, emailBody);
                    
                    emailHelper.SendEmail();
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                
                
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
