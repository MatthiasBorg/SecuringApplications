using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;

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


        public CommentsController(IStudentAssignmentsService studentAssignmentsService, IStudentsService studentsService, IWebHostEnvironment environment, IAssignmentsService assignmentsService, ICommentsService commentsService, ITeachersService teachersService)
        {
            _assignmentsService = assignmentsService;
            _teachersService = teachersService;
            _studentsService = studentsService;
            _studentAssignmentsService = studentAssignmentsService;
            _env = environment;
            _commentsService = commentsService;
            _teachersService = teachersService;
        }

        // GET: CommentsController
        public ActionResult Index(String id)
        {
            byte[] encoded = Convert.FromBase64String(id);
            Guid realId = new Guid(System.Text.Encoding.UTF8.GetString(encoded));
            return View(_commentsService.GetCommentsByAssignment(realId));
        }

        // GET: CommentsController/Create
        public ActionResult Create()
        {
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

                Guid id = new Guid(strId);

                var studentAssignment = _studentAssignmentsService.GetStudentAssignment(id);
                comment.StudentAssignment = studentAssignment;

                comment.Timestamp = DateTime.Now;
                _commentsService.AddComment(comment);
                return RedirectToAction($"Index", new { id = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(id.ToString())) });
            }
            catch
            {
                return View();
            }
        }
    }
}
