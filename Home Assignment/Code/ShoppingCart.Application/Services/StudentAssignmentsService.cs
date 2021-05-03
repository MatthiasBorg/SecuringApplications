using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Application.Services
{
    public class StudentAssignmentsService : IStudentAssignmentsService
    {
        
        private IMapper _mapper;
        private IStudentAssignmentsRepository _studentAssignmentsRepo;

        public StudentAssignmentsService(IStudentAssignmentsRepository studentAssignmentsRepo
            ,  IMapper mapper
        )
        {
            _mapper = mapper;
            _studentAssignmentsRepo = studentAssignmentsRepo;
        }
        
        public StudentAssignmentViewModel GetStudentAssignment(Guid id)
        {
            var studentAssignment = _studentAssignmentsRepo.GetStudentAssignment(id);
            var result = _mapper.Map<StudentAssignmentViewModel>(studentAssignment);
            return result;
        }

        public IQueryable<StudentAssignmentViewModel> GetStudentAssignments()
        {
            var studentAssignments = _studentAssignmentsRepo.GetStudentAssignments().ProjectTo<StudentAssignmentViewModel>(_mapper.ConfigurationProvider);
            return studentAssignments;
        }

        public IQueryable<StudentAssignmentViewModel> GetStudentAssignmentsById(Guid id)
        {
            var studentAssignments = _studentAssignmentsRepo.GetStudentAssignments().Where(x => x.Student.Id == id).ProjectTo<StudentAssignmentViewModel>(_mapper.ConfigurationProvider);
            return studentAssignments;
        }

        public Guid AddStudentAssignment(StudentAssignmentViewModel sa)
        {
            var newStudentAssignment = _mapper.Map<StudentAssignment>(sa);
            _studentAssignmentsRepo.AddStudentAssignment(newStudentAssignment);
            return newStudentAssignment.Id;
        }

        public bool SubmitAssignment(string filePath, Guid id, String signiture, String publicKey)
        {
            _studentAssignmentsRepo.SubmitAssignment(filePath, id, signiture, publicKey);
            return true;
        }
    }
}