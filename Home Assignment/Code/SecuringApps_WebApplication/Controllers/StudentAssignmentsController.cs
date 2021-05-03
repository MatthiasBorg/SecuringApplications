using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Helpers;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Data.Context;

namespace WebApplication.Controllers
{
    public class StudentAssignmentsController : Controller
    {
        string pass = "Pa$$w0rd";
        byte[] salt = CryptographicHelper.GenerateSalt();
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
        public ActionResult Details(String id)
        {
            byte[] encoded = Convert.FromBase64String(id);
            Guid realId = new Guid(System.Text.Encoding.UTF8.GetString(encoded));
            HttpContext.Session.SetString("StudentAssignment", id.ToString());
            return View(_studentAssignmentsService.GetStudentAssignment(realId));
        }

        [Authorize(Roles = "Student")]
        // GET: StudentAssignmentsController/Create
        public ActionResult SubmitAction(String id)
        {
            byte[] encoded = Convert.FromBase64String(id);
            Guid realId = new Guid(System.Text.Encoding.UTF8.GetString(encoded));
            //HttpContext.Session.SetString("StudentAssignmentToSubmit", id.ToString());
            return View(_studentAssignmentsService.GetStudentAssignment(realId));
        }

        [Authorize(Roles = "Student")]
        // POST: StudentAssignmentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitAction(StudentAssignmentViewModel studentAssignment, IFormFile f)
        {
            try
            {
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

                        using (var stream = f.OpenReadStream()) {
                            stream.Position = 0;

                            int byte1 = stream.ReadByte();
                            int byte2 = stream.ReadByte();

                            if (byte1 == 37 && byte2 == 80)
                            {
                            }
                            else {
                                TempData["warning"] = "Assignment not submitted as only PDF files are accepted";
                                return View(_studentAssignmentsService.GetStudentAssignment(studentAssignment.Id));
                            }
                        }

                        if (System.IO.Path.GetExtension(f.FileName) != ".pdf") {
                            TempData["warning"] = "Assignment not submitted as only PDF files are accepted";
                            return View(_studentAssignmentsService.GetStudentAssignment(studentAssignment.Id));
                        }

                        ////var certLoggedInUser = GetCertificateWithPrivateKeyForIdentity();

                        //var signature = _digitalSignatures.Sign(encryptedText,
                        //Utils.CreateRsaPrivateKey(certLoggedInUser));

                        //var signiture = CryptographicHelper.CreateSigniture();

                        string newFilename = Guid.NewGuid() + System.IO.Path.GetExtension(f.FileName);
                        string newFilenameWithAbsolutePath = _env.ContentRootPath + @"\Assignments\" + newFilename;

                        //var mainStream = System.IO.File.Create(newFilenameWithAbsolutePath);

                        byte[] fileInBytes;

                        // TRY TO DO LOOP

                        //var signiture = CryptographicHelper.CreateSigniture(memoryStream.ToArray());
                        //studentAssignment.Signiture = signiture;

                        using (var mainStream = System.IO.File.Create(newFilenameWithAbsolutePath))
                        {
                            f.CopyTo(mainStream);

                            mainStream.Position = 0;

                            MemoryStream memoryStream = new MemoryStream();
                            mainStream.CopyTo(memoryStream);
                            fileInBytes = memoryStream.ToArray();

                            var keyPair = CryptographicHelper.GenerateAsymmetricKeys(); // key 1 - public; key 2 - private

                            var signiture = CryptographicHelper.CreateSigniture(fileInBytes, keyPair.Item2);

                            studentAssignment.Signiture = signiture;

                            studentAssignment.PubicKey = keyPair.Item1;
                        }


                        studentAssignment.File = @"\Assignments\" + newFilename;
                    }
                }

                _studentAssignmentsService.SubmitAssignment(studentAssignment.File, studentAssignment.Id, studentAssignment.Signiture, studentAssignment.PubicKey);

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

        // post: studentassignmentscontroller/edit/5
        [HttpGet]
        [Authorize(Roles = "Teacher, Student")]
        public IActionResult DownloadFile(String id)
        {

            byte[] encoded = Convert.FromBase64String(id);
            Guid realId = new Guid(System.Text.Encoding.UTF8.GetString(encoded));

            var studentAssignment = _studentAssignmentsService.GetStudentAssignment(realId);
            string filePath = _env.ContentRootPath + studentAssignment.File;

            IList<StudentAssignmentViewModel> allStudentAssignments = _studentAssignmentsService.GetStudentAssignments().Where(x => x.File != null).Where(x => x.Id != realId).ToList();

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            var isValid = CryptographicHelper.VerifySigniture(fileBytes, studentAssignment.Signiture, studentAssignment.PubicKey);

            if (isValid == false)
            {
                TempData["warning"] = "This Assignment Has Been Temepered With!";
                return RedirectToAction("Details", new { id = id });
            }

            foreach (var studentAssignmentByStudent in allStudentAssignments)
            {
                string filePathOthers = _env.ContentRootPath + studentAssignmentByStudent.File;
                byte[] bytes = System.IO.File.ReadAllBytes(filePathOthers);

                if (fileBytes.SequenceEqual(bytes))
                {
                    TempData["warning"] = "You Are Downloading A Copied Assignment!";
                    return RedirectToAction("Details", new { id = id });
                }
            }

            string filename = studentAssignment.Assignment.Title + " - " + studentAssignment.Student.FirstName + " " + studentAssignment.Student.LastName;

            var net = new System.Net.WebClient();
            var data = net.DownloadData(filePath);
            var content = new System.IO.MemoryStream(data);
            var contentType = "application/pdf";
            var fileName = $"{filename}.pdf";
            return File(content, contentType, fileName);
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult GetAllSubmittedAssignments() {

            return View(_studentAssignmentsService.GetStudentAssignments());
        }
    }
}
