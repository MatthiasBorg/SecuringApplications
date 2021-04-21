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
    public class StudentsService : IStudentsService
    {
        private IMapper _mapper;
        private IStudentsRepository _studentsRepository;

        public StudentsService(IStudentsRepository studentsRepository
            ,  IMapper mapper
        )
        {
            _mapper = mapper;
            _studentsRepository = studentsRepository;
        }
        
        public IQueryable<StudentViewModel> GetStudents()
        {
            var students = _studentsRepository.GetStudents().ProjectTo<StudentViewModel>(_mapper.ConfigurationProvider);
            return students;
        }

        public Guid AddStudent(StudentViewModel s)
        {
            var newStudent = _mapper.Map<Student>(s);
            _studentsRepository.AddStudent(newStudent);
            return newStudent.Id;
        }
    }
}