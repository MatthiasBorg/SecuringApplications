using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using WebApplication.ActionFilters;

namespace WebApplication.Controllers
{
    public class CommentsController : Controller
    {

        private readonly IAssignmentsService _assignmentsService;
        private readonly ITeachersService _teachersService;
        private readonly IStudentsService _studentsService;
        private readonly IStudentAssignmentsService _studentAssignmentsService;
        private IWebHostEnvironment _env;
        private ICommentsService _commentsService;
        private ILogger<CommentsController> _logger;


        public CommentsController(IStudentAssignmentsService studentAssignmentsService, IStudentsService studentsService, IWebHostEnvironment environment, IAssignmentsService assignmentsService, ICommentsService commentsService, ITeachersService teachersService, ILogger<CommentsController> logger)
        {
            _assignmentsService = assignmentsService;
            _teachersService = teachersService;
            _studentsService = studentsService;
            _studentAssignmentsService = studentAssignmentsService;
            _env = environment;
            _commentsService = commentsService;
            _teachersService = teachersService;
            _logger = logger;
        }

        // GET: CommentsController
        [AuthorizationFilter]
        public ActionResult Index(String id)
        {
            byte[] encoded = Convert.FromBase64String(id);
            Guid realId = new Guid(System.Text.Encoding.UTF8.GetString(encoded));

            _logger.LogInformation($"User {User.Identity.Name} Accessed Comment For Assignemt With Id: {realId} - Time: {DateTime.Now} - IP Address: {HttpContext.Connection.RemoteIpAddress}");

            return View(_commentsService.GetCommentsByAssignment(realId));
        }

        // GET: CommentsController/Create
        public ActionResult Create()
        {
            _logger.LogInformation($"User {User.Identity.Name} Tried To Create Comment - Time: {DateTime.Now} - IP Address: {HttpContext.Connection.RemoteIpAddress}");
            return View();
        }

        // POST: CommentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentViewModel comment)
        {
            try
            {
                if (User.IsInRole("Teacher"))
                {
                    var currentUser = _teachersService.GetTeacherByEmail(User.Identity.Name);
                    comment.Teacher = currentUser;
                }
                else {
                    var currentUser = _studentsService.GetStudentByEmail(User.Identity.Name);
                    comment.Student = currentUser;
                }

                string strId = HttpContext.Session.GetString("StudentAssignment");
                byte[] encoded = Convert.FromBase64String(strId);
                Guid id = new Guid(System.Text.Encoding.UTF8.GetString(encoded));

                var studentAssignment = _studentAssignmentsService.GetStudentAssignment(id);
                comment.StudentAssignment = studentAssignment;

                comment.Timestamp = DateTime.Now;
                _commentsService.AddComment(comment);
                return RedirectToAction($"Index", new { id = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(id.ToString())) });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In Comment Creation: " + ex.Message);
                TempData["error"] = "Something Went Wrong During Comment Creation - We Are Looking Into It";
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
