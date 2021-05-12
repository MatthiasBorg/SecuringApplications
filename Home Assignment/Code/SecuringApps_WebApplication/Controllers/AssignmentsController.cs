using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;

namespace WebApplication.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly IAssignmentsService _assignmentsService;
        private readonly ITeachersService _teachersService;
        private readonly IStudentsService _studentsService;
        private readonly IStudentAssignmentsService _studentAssignmentsService;
        private readonly ILogger<AssignmentsController> _logger;

        public AssignmentsController(IAssignmentsService assignmentsService, ITeachersService teachersService, IStudentsService studentsService, IStudentAssignmentsService studentAssignmentsService, ILogger<AssignmentsController> logger) {
            _assignmentsService = assignmentsService;
            _teachersService = teachersService;
            _studentsService = studentsService;
            _studentAssignmentsService = studentAssignmentsService;
            _logger = logger;
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult Index()
        {
            // Gets all the assignments by a teacher
            var teacher = _teachersService.GetTeacherByEmail(User.Identity.Name);
            _logger.LogInformation($"User {User.Identity.Name} Accessed All Assignments - Time: {DateTime.Now} - IP Address: {HttpContext.Connection.RemoteIpAddress}");
            return View(_assignmentsService.GetAssignmentsByTeacher(teacher.Id));
        }

        public ActionResult Details(String id)
        {
            // Gets the details of a specific assignment
            byte[] encoded = Convert.FromBase64String(id);
            Guid realId = new Guid(System.Text.Encoding.UTF8.GetString(encoded)); // Gets string from encoded value of id

            _logger.LogInformation($"User {User.Identity.Name} Tried To Access Assignment With Id: {realId} - Time: {DateTime.Now} - IP Address: {HttpContext.Connection.RemoteIpAddress}");

            return View(_assignmentsService.GetAssignment(realId));
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult Create()
        {
            _logger.LogInformation($"User {User.Identity.Name} Accessed Create An Assignment - Time: {DateTime.Now} - IP Address: {HttpContext.Connection.RemoteIpAddress}");
            return View();
        }

        // Creates a new assignment
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(AssignmentViewModel newAssignment)
        {
            try
            {
                var teacher = _teachersService.GetTeacherByEmail(User.Identity.Name);
                newAssignment.Teacher = teacher;
                DateTime deadlineAsDate = DateTime.Parse(newAssignment.Deadline);

                int result = DateTime.Compare(DateTime.Now.Date, DateTime.Parse(newAssignment.Deadline).Date);

                    if (DateTime.Parse(newAssignment.Deadline).Date < DateTime.Now.Date)
                {
                    TempData["warning"] = "Assignment Was Not Created - Deadline Date Was In The Past";
                    return View();
                }

                newAssignment.Deadline = deadlineAsDate.ToString("dd/MM/yyyy");
                newAssignment.PublishedDate = DateTime.Now.ToString("dd/MM/yyyy");
                var assignmentId = _assignmentsService.AddAssignment(newAssignment);


                var students = _studentsService.GetStudentsByTeacher(teacher.Id);

                AssignmentViewModel createdAssignment = _assignmentsService.GetAssignment(assignmentId);
                CreateStudentAssignments(students, createdAssignment);

                TempData["feedback"] = "Assignment Was Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error In Assignment Creation: " + ex.Message);
                TempData["error"] = "Something Went Wrong During Assignment Creation - We Are Looking Into It";
                return RedirectToAction("Error", "Home");
            }
        }

        private void CreateStudentAssignments(IQueryable<StudentViewModel> students, AssignmentViewModel createdAssignment)
        {
            _logger.LogInformation($"User {User.Identity.Name} Assigning Student To Assignment - Time: {DateTime.Now} - IP Address: {HttpContext.Connection.RemoteIpAddress}");

            IList<StudentViewModel> studentsList = students.ToList();

            foreach (var student in studentsList)
            {

                StudentAssignmentViewModel studentAssignment = new StudentAssignmentViewModel();

                studentAssignment.Student = student;
                studentAssignment.Assignment = createdAssignment;
                studentAssignment.File = null;

                if (_studentAssignmentsService.AddStudentAssignment(studentAssignment) == null)
                {
                    TempData["warning"] = "Assignment Was Not Associated to Student Successfuly";
                }
            }
        }
    }
}
