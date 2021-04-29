using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;

namespace WebApplication.Controllers
{
    public class StudentAssignmentsController : Controller
    {

        private readonly IAssignmentsService _assignmentsService;
        //private readonly ITeachersService _teachersService;
        private readonly IStudentsService _studentsService;
        private readonly IStudentAssignmentsService _studentAssignmentsService;
        private IWebHostEnvironment _env;


        public StudentAssignmentsController( IStudentAssignmentsService studentAssignmentsService, IStudentsService studentsService, IWebHostEnvironment environment, IAssignmentsService assignmentsService)
        {
            _assignmentsService = assignmentsService;
            //_teachersService = teachersService;
            _studentsService = studentsService;
            _studentAssignmentsService = studentAssignmentsService;
            _env = environment;
        }

        // GET: StudentAssignmentsController
        [Authorize(Roles = "Teacher, Student")]
        public ActionResult Index()
        {
            var student = _studentsService.GetStudentByEmail(User.Identity.Name);
            return View(_studentAssignmentsService.GetStudentAssignmentsById(student.Id));
        }

        [Authorize(Roles = "Teacher, Student")]
        // GET: StudentAssignmentsController/Details/5
        public ActionResult Details(Guid id)
        {
            //ViewBag.StudentAssignment = id;
            HttpContext.Session.SetString("StudentAssignment", id.ToString());
            return View(_studentAssignmentsService.GetStudentAssignment(id));
        }

        [Authorize(Roles = "Student")]
        // GET: StudentAssignmentsController/Create
        public ActionResult SubmitAction(Guid id)
        {
            return View(_studentAssignmentsService.GetStudentAssignment(id));
        }

        [Authorize(Roles = "Student")]
        // POST: StudentAssignmentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitAction(StudentAssignmentViewModel studentAssignment, IFormFile f)
        {
            try
            {
                //var assignment = _sService.GetAssignment(studentAssignment.Id);
                var assignment = _studentAssignmentsService.GetStudentAssignment(studentAssignment.Id).Assignment;

                int result = DateTime.Compare(DateTime.Parse(assignment.Deadline).Date, DateTime.Now.Date);

                if (DateTime.Parse(assignment.Deadline).Date < DateTime.Now.Date) {
                    TempData["warning"] = "Assignment not submitted as deadline was exceeded";
                    return View(_studentAssignmentsService.GetStudentAssignment(studentAssignment.Id));
                }

                if (f != null)
                {
                    if (f.Length > 0)
                    {
                        //C:\Users\Ryan\source\repos\SWD62BEP\SWD62BEP\Solution3\PresentationWebApp\wwwroot
                        if (System.IO.Path.GetExtension(f.FileName) != ".pdf") {
                            TempData["warning"] = "Assignment not submitted as only PDF files are accepted";
                            return View(_studentAssignmentsService.GetStudentAssignment(studentAssignment.Id));
                        }

                        string newFilename = Guid.NewGuid() + System.IO.Path.GetExtension(f.FileName);
                        string newFilenameWithAbsolutePath = _env.ContentRootPath + @"\Assignments\" + newFilename;

                        using (var stream = System.IO.File.Create(newFilenameWithAbsolutePath))
                        {
                            f.CopyTo(stream);
                        }

                        studentAssignment.File = @"\Assignments\" + newFilename;
                    }
                }

                _studentAssignmentsService.SubmitAssignment(studentAssignment.File, studentAssignment.Id);

                TempData["feedback"] = "Assignment was submitted successfully";
            }
            catch (Exception ex)
            {
                //log error
                //_logger.LogError(ex.Message);
                TempData["warning"] = "Subbmittion was not added!";
            }

            return View(_studentAssignmentsService.GetStudentAssignment(studentAssignment.Id));
        }

        //// GET: StudentAssignmentsController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: StudentAssignmentsController/Edit/5
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

        //// GET: StudentAssignmentsController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: StudentAssignmentsController/Delete/5
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

        // post: studentassignmentscontroller/edit/5
        [HttpGet]
        [Authorize(Roles = "Student")]
        public IActionResult DownloadFile(Guid id)
        {

            var studentAssignment = _studentAssignmentsService.GetStudentAssignment(id);
            string filePath = _env.ContentRootPath + studentAssignment.File;

            string filename = studentAssignment.Assignment.Title + " - " + studentAssignment.Student.FirstName + " " + studentAssignment.Student.LastName;

            var net = new System.Net.WebClient();
            var data = net.DownloadData(filePath);
            var content = new System.IO.MemoryStream(data);
            var contentType = "application/pdf";
            var fileName = $"{filename}.pdf";
            return File(content, contentType, fileName);

            //Response.ContentType = "application/pdf";
            //Response.A;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=MyFile.pdf");
            //Response.TransmitFile(Server.MapPath("~/Files/MyFile.pdf"));
            //Response.End();

            //try
            //{
            //    return redirecttoaction(nameof(index));
            //}
            //catch
            //{
            //    return view();
            //}
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult GetAllSubmittedAssignments() {

            return View(_studentAssignmentsService.GetStudentAssignments());
        }
    }
}
