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
using WebApplication.ActionFilters;

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
        [AuthorizationFilter]
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
                            Tuple<byte[], byte[]> keyIVPair = CryptographicHelper.GenerateKeys();
                            var keyPair = CryptographicHelper.GenerateAsymmetricKeys(); // key 1 - public; key 2 - private

                            studentAssignment.PublicKey = keyPair.Item1;
                            studentAssignment.PrivateKey = keyPair.Item2;

                            // Stage 1 - SymmetricEncryption
                            //byte[] fileToEncrypt = System.IO.File.ReadAllBytes(newFilenameWithAbsolutePath);
                            ///byte[] fileToEncryptSE = CryptographicHelper.SymmetricEncrypt(fileToEncrypt);

                            // Stage 2 - AsymetricEncryption
                            //byte[] fileToEncryptAE = CryptographicHelper.AsymetricEncrypt(fileToEncryptSE, keyPair.Item1);


                            f.CopyTo(mainStream);

                            mainStream.Position = 0;

                            MemoryStream memoryStream = new MemoryStream();
                            mainStream.CopyTo(memoryStream);
                            fileInBytes = memoryStream.ToArray();

                            // Stage 1 - SymmetricEncryption
                            //byte[] fileToEncrypt = System.IO.File.ReadAllBytes(newFilenameWithAbsolutePath);
                            byte[] fileToEncryptSE = CryptographicHelper.SymmetricEncrypt(fileInBytes, keyIVPair);
                            mainStream.Position = 0;
                            mainStream.Write(fileToEncryptSE);
                            //fileToEncryptSE.CopyTo()
                            

                            // Stage 2 - AsymetricEncryption
                            //byte[] fileToEncryptAE = CryptographicHelper.AsymetricEncrypt(fileToEncryptSE, keyPair.Item1);

                            var signiture = CryptographicHelper.CreateSigniture(fileInBytes, keyPair.Item2);

                            //var signitureAE = CryptographicHelper.AsymetricEncrypt(signiture, studentAssignment.PublicKey);

                            studentAssignment.Signiture = Convert.ToBase64String(signiture);

                            var keyAE = CryptographicHelper.AsymetricEncrypt(keyIVPair.Item1, studentAssignment.PublicKey);

                            studentAssignment.Key = Convert.ToBase64String(keyAE);

                            var ivAE = CryptographicHelper.AsymetricEncrypt(keyIVPair.Item2, studentAssignment.PublicKey);

                            studentAssignment.Iv = Convert.ToBase64String(ivAE);
                        }


                        studentAssignment.File = @"\Assignments\" + newFilename;
                    }
                }

                _studentAssignmentsService.SubmitAssignment(studentAssignment.File, studentAssignment.Id, studentAssignment.Signiture, studentAssignment.PublicKey, studentAssignment.PrivateKey, studentAssignment.Key, studentAssignment.Iv);

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
        [AuthorizationFilter]
        public IActionResult DownloadFile(String id)
        {

            byte[] encoded = Convert.FromBase64String(id);
            Guid realId = new Guid(System.Text.Encoding.UTF8.GetString(encoded));

            var studentAssignment = _studentAssignmentsService.GetStudentAssignment(realId);
            string filePath = _env.ContentRootPath + studentAssignment.File;

            IList<StudentAssignmentViewModel> allStudentAssignments = _studentAssignmentsService.GetStudentAssignments().Where(x => x.File != null).Where(x => x.Id != realId).ToList();

            byte[] fileBytesEnc = System.IO.File.ReadAllBytes(filePath);

            //var signiture = CryptographicHelper.AsymmetricDecrypt(Convert.FromBase64String(studentAssignment.Signiture), studentAssignment.PrivateKey);
            var key = CryptographicHelper.AsymmetricDecrypt(Convert.FromBase64String(studentAssignment.Key), studentAssignment.PrivateKey);
            var iv = CryptographicHelper.AsymmetricDecrypt(Convert.FromBase64String(studentAssignment.Iv), studentAssignment.PrivateKey);

            Tuple<byte[], byte[]> keyPair = new Tuple<byte[], byte[]>(key, iv);

            var fileBytes = CryptographicHelper.SymmetricDecrypt(fileBytesEnc, keyPair);

            var isValid = CryptographicHelper.VerifySigniture(fileBytes, Convert.FromBase64String(studentAssignment.Signiture), studentAssignment.PublicKey);

            if (isValid == false)
            {
                TempData["warning"] = "This Assignment Has Been Temepered With!";
                return RedirectToAction("Details", new { id = id });
            }

            foreach (var studentAssignmentByStudent in allStudentAssignments)
            {
                string filePathOthers = _env.ContentRootPath + studentAssignmentByStudent.File;
                byte[] bytesEncOthers = System.IO.File.ReadAllBytes(filePathOthers);

                var keyOthers = CryptographicHelper.AsymmetricDecrypt(Convert.FromBase64String(studentAssignmentByStudent.Key), studentAssignmentByStudent.PrivateKey);
                var ivOthers = CryptographicHelper.AsymmetricDecrypt(Convert.FromBase64String(studentAssignmentByStudent.Iv), studentAssignmentByStudent.PrivateKey);

                Tuple<byte[], byte[]> keyPairOthers = new Tuple<byte[], byte[]>(key, iv);

                var fileBytesOthers = CryptographicHelper.SymmetricDecrypt(bytesEncOthers, keyPair);

                if (fileBytes.SequenceEqual(fileBytesOthers))
                {
                    TempData["warning"] = "You Are Downloading A Copied Assignment!";
                    return RedirectToAction("Details", new { id = id });
                }
            }

            string filename = studentAssignment.Assignment.Title + " - " + studentAssignment.Student.FirstName + " " + studentAssignment.Student.LastName;

            var net = new System.Net.WebClient();
            var data = CryptographicHelper.SymmetricDecrypt(fileBytesEnc, keyPair);//net.DownloadData(filePath);
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
