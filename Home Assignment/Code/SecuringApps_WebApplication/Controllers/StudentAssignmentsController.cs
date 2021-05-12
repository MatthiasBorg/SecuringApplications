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
using Microsoft.Extensions.Logging;
using ShoppingCart.Application.Helpers;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Data.Context;
using WebApplication.ActionFilters;

namespace WebApplication.Controllers
{
    public class StudentAssignmentsController : Controller
    {
        private readonly IStudentsService _studentsService;
        private readonly IStudentAssignmentsService _studentAssignmentsService;
        private IWebHostEnvironment _env;
        private readonly ILogger<StudentAssignmentsController> _logger;

        public StudentAssignmentsController( IStudentAssignmentsService studentAssignmentsService, IStudentsService studentsService, IWebHostEnvironment environment, ILogger<StudentAssignmentsController> logger)
        {
            _studentsService = studentsService;
            _studentAssignmentsService = studentAssignmentsService;
            _env = environment;
            _logger = logger;
        }

        // Gets all the assignments related to a student
        [Authorize(Roles = "Teacher, Student")]
        public ActionResult Index()
        {
            _logger.LogInformation($"User {User.Identity.Name} Accessed All Assignments For Student - Time: {DateTime.Now} - IP Address: {HttpContext.Connection.RemoteIpAddress}");
            var student = _studentsService.GetStudentByEmail(User.Identity.Name);
            return View(_studentAssignmentsService.GetStudentAssignmentsById(student.Id));
        }

        // Gets all details of a specifc assignment related to student
        [Authorize(Roles = "Teacher, Student")]
        [AuthorizationFilter]
        public ActionResult Details(String id)
        {
            byte[] encoded = Convert.FromBase64String(id);
            Guid realId = new Guid(System.Text.Encoding.UTF8.GetString(encoded));

            _logger.LogInformation($"User {User.Identity.Name} Tried To Access Assignment With Id: {realId} - Time: {DateTime.Now} - IP Address: {HttpContext.Connection.RemoteIpAddress}");

            HttpContext.Session.SetString("StudentAssignment", id.ToString());
            return View(_studentAssignmentsService.GetStudentAssignment(realId));
        }

        [Authorize(Roles = "Student")]
        public ActionResult SubmitAction(String id)
        {
            byte[] encoded = Convert.FromBase64String(id);
            Guid realId = new Guid(System.Text.Encoding.UTF8.GetString(encoded));
            return View(_studentAssignmentsService.GetStudentAssignment(realId));
        }

        // Submits an assignment
        [Authorize(Roles = "Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitAction(StudentAssignmentViewModel studentAssignment, IFormFile f)
        {
            _logger.LogInformation($"User {User.Identity.Name} Tried To Submit File - Time: {DateTime.Now} - IP Address: {HttpContext.Connection.RemoteIpAddress}");
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

                            if (byte1 == 37 && byte2 == 80) // checks for valid pdf
                            {
                            }
                            else {
                                TempData["warning"] = "Assignment not submitted as only PDF files are accepted";
                                return View(_studentAssignmentsService.GetStudentAssignment(studentAssignment.Id));
                            }
                        }

                        if (System.IO.Path.GetExtension(f.FileName) != ".pdf") { // checks for valid pdf by extension
                            TempData["warning"] = "Assignment not submitted as only PDF files are accepted";
                            return View(_studentAssignmentsService.GetStudentAssignment(studentAssignment.Id));
                        }

                        string newFilename = Guid.NewGuid() + System.IO.Path.GetExtension(f.FileName); // creates new file name using guid (to prevent same files with the same name to be overwritten)
                        string newFilenameWithAbsolutePath = _env.ContentRootPath + @"\Assignments\" + newFilename; // absolute file path

                        byte[] fileInBytes;
                        using (var mainStream = System.IO.File.Create(newFilenameWithAbsolutePath))
                        {
                            // genearting keys - private / public & IV keys
                            Tuple<byte[], byte[]> keyIVPair = CryptographicHelper.GenerateKeys();
                            var keyPair = CryptographicHelper.GenerateAsymmetricKeys(); // key 1 - public; key 2 - private

                            studentAssignment.PublicKey = keyPair.Item1;
                            studentAssignment.PrivateKey = keyPair.Item2;

                            f.CopyTo(mainStream);

                            mainStream.Position = 0;

                            MemoryStream memoryStream = new MemoryStream();
                            mainStream.CopyTo(memoryStream);
                            fileInBytes = memoryStream.ToArray(); // getting all bytes of file

                            byte[] fileToEncryptSE = CryptographicHelper.SymmetricEncrypt(fileInBytes, keyIVPair);  // symetric encryption of file
                            mainStream.Position = 0;
                            mainStream.Write(fileToEncryptSE);

                            var signiture = CryptographicHelper.CreateSigniture(fileInBytes, keyPair.Item2); // creating signiture on main file (not encrypted)

                            studentAssignment.Signiture = Convert.ToBase64String(signiture);

                            var keyAE = CryptographicHelper.AsymetricEncrypt(keyIVPair.Item1, studentAssignment.PublicKey); // asymetric encryption on key

                            studentAssignment.Key = Convert.ToBase64String(keyAE);

                            var ivAE = CryptographicHelper.AsymetricEncrypt(keyIVPair.Item2, studentAssignment.PublicKey); // asymetric encryption on iv

                            studentAssignment.Iv = Convert.ToBase64String(ivAE);
                        }


                        studentAssignment.File = @"\Assignments\" + newFilename; // file path to store in db
                    }
                }

                // storing in db
                _studentAssignmentsService.SubmitAssignment(studentAssignment.File, studentAssignment.Id, studentAssignment.Signiture, studentAssignment.PublicKey, studentAssignment.PrivateKey, studentAssignment.Key, studentAssignment.Iv);

                TempData["feedback"] = "Assignment was submitted successfully";
            }
            catch (Exception ex)
            {
                TempData["warning"] = "Subbmittion was not added!";
                //log error
                _logger.LogError("Error During Submission: " + ex.Message);
                TempData["error"] = "Something Went Wrong During Submission - We Are Looking Into It";
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Teacher, Student")]
        [AuthorizationFilter]
        public IActionResult DownloadFile(String id)
        {
            _logger.LogInformation($"User {User.Identity.Name} Tried To Download File - Time: {DateTime.Now} - IP Address: {HttpContext.Connection.RemoteIpAddress}");
            byte[] encoded = Convert.FromBase64String(id);
            Guid realId = new Guid(System.Text.Encoding.UTF8.GetString(encoded));

            var studentAssignment = _studentAssignmentsService.GetStudentAssignment(realId);
            string filePath = _env.ContentRootPath + studentAssignment.File;

            // getting all other submitted assignments (to check for copying)
            IList<StudentAssignmentViewModel> allStudentAssignments = _studentAssignmentsService.GetStudentAssignments().Where(x => x.File != null).Where(x => x.Id != realId).ToList();

            byte[] fileBytesEnc = System.IO.File.ReadAllBytes(filePath); // getting the encrypted file from path

            var key = CryptographicHelper.AsymmetricDecrypt(Convert.FromBase64String(studentAssignment.Key), studentAssignment.PrivateKey); // asymetric decryption on key
            var iv = CryptographicHelper.AsymmetricDecrypt(Convert.FromBase64String(studentAssignment.Iv), studentAssignment.PrivateKey); // asymetric decryption on iv

            Tuple<byte[], byte[]> keyPair = new Tuple<byte[], byte[]>(key, iv); // storing key and iv in tupel

            var fileBytes = CryptographicHelper.SymmetricDecrypt(fileBytesEnc, keyPair); // getting the proper file by using symetric decryption on encrypted file

            var isValid = CryptographicHelper.VerifySigniture(fileBytes, Convert.FromBase64String(studentAssignment.Signiture), studentAssignment.PublicKey); // cheks if valid by verifying signiture

            if (isValid == false)
            {
                TempData["warning"] = "This Assignment Has Been Temepered With! It Cannot Be Downloaded!";
                return RedirectToAction("Details", new { id = id });
            }

            // loop to check all other assignments (copying check)
            foreach (var studentAssignmentByStudent in allStudentAssignments)
            {
                string filePathOthers = _env.ContentRootPath + studentAssignmentByStudent.File;
                byte[] bytesEncOthers = System.IO.File.ReadAllBytes(filePathOthers);

                var keyOthers = CryptographicHelper.AsymmetricDecrypt(Convert.FromBase64String(studentAssignmentByStudent.Key), studentAssignmentByStudent.PrivateKey);
                var ivOthers = CryptographicHelper.AsymmetricDecrypt(Convert.FromBase64String(studentAssignmentByStudent.Iv), studentAssignmentByStudent.PrivateKey);

                Tuple<byte[], byte[]> keyPairOthers = new Tuple<byte[], byte[]>(keyOthers, ivOthers);

                var fileBytesOthers = CryptographicHelper.SymmetricDecrypt(bytesEncOthers, keyPairOthers);

                if (fileBytes.SequenceEqual(fileBytesOthers))
                {
                    TempData["warning"] = "This Is A Copied Assignment! It Cannot Be Downloaded!";
                    return RedirectToAction("Details", new { id = id });
                }
            }

            string filename = studentAssignment.Assignment.Title + " - " + studentAssignment.Student.FirstName + " " + studentAssignment.Student.LastName; // generating file name to be downloaded

            // downloading file
            var net = new System.Net.WebClient();
            var data = CryptographicHelper.SymmetricDecrypt(fileBytesEnc, keyPair);
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
