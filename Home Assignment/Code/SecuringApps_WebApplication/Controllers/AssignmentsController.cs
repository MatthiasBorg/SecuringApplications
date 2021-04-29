using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public AssignmentsController(IAssignmentsService assignmentsService, ITeachersService teachersService, IStudentsService studentsService, IStudentAssignmentsService studentAssignmentsService) {
            _assignmentsService = assignmentsService;
            _teachersService = teachersService;
            _studentsService = studentsService;
            _studentAssignmentsService = studentAssignmentsService;
        }

        // GET: AssignmentsController
        [Authorize(Roles = "Teacher")]
        public ActionResult Index()
        {
            var teacher = _teachersService.GetTeacherByEmail(User.Identity.Name);
            return View(_assignmentsService.GetAssignmentsByTeacher(teacher.Id));
        }

        // GET: AssignmentsController/Details/5
        public ActionResult Details(Guid id)
        {
            return View(_assignmentsService.GetAssignment(id));
        }

        // GET: AssignmentsController/Create
        [Authorize(Roles = "Teacher")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AssignmentsController/Create
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
            catch
            {
                TempData["warning"] = "Assignment Was Not Created";
                return View();
            }
        }

        private void CreateStudentAssignments(IQueryable<StudentViewModel> students, AssignmentViewModel createdAssignment)
        {
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

        //// GET: AssignmentsController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: AssignmentsController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AssignmentsController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: AssignmentsController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
