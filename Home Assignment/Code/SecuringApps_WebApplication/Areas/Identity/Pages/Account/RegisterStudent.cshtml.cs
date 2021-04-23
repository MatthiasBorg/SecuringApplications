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

            // [Required]
            // [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            // [DataType(DataType.Password)]
            // [Display(Name = "Password")]
            public string Password { get; set; }
            //
            // [DataType(DataType.Password)]
            // [Display(Name = "Confirm password")]
            // [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            // public string ConfirmPassword { get; set; }

            [Required]
            public string Address { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
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
                    
                    // var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    // code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    // var callbackUrl = Url.Page(
                    //     "/Account/ConfirmEmail",
                    //     pageHandler: null,
                    //     values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    //     protocol: Request.Scheme);

                    // await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //     $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    
                    // SmtpClient client=new SmtpClient("smtp.live.com", 587);
                    // client.Credentials = new System.Net.NetworkCredential("applicationtestuser123@outlook.com", "HXQszjlQ8E2k");
                    // client.EnableSsl = true;
                    // // smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
                    // client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //
                    // client.Send("applicationtestuser123@outlook.com",user.Email,
                    //     "Confirm your email",
                    //     $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    
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
                    
                    
                    // SmtpClient smtpClient = new SmtpClient("smtp.live.com", 587);
                    //
                    // smtpClient.Credentials = new System.Net.NetworkCredential("applicationtestuser123@outlook.com", "HXQszjlQ8E2k");
                    // // smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
                    // smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    // smtpClient.EnableSsl = true;
                    // MailMessage mail = new MailMessage();
                    //
                    // //Setting From , To and CC
                    // mail.From = new MailAddress("applicationtestuser123@outlook.com", "SA_Secure Website");
                    // mail.To.Add(new MailAddress(newStudent.Email));
                    // mail.Body = emailBody;
                    // mail.Subject = emailSubject;
                    //
                    // smtpClient.Send(mail);
                    

                    // if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    // {
                    //     return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    // }
                    // else
                    // {
                    //     await _signInManager.SignInAsync(user, isPersistent: false);
                    //     return LocalRedirect(returnUrl);
                    // }

                    //return RedirectToPage("Home");
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
