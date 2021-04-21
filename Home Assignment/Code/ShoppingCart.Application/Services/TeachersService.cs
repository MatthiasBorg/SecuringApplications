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
    public class TeachersService : ITeachersService
    {
        
        private IMapper _mapper;
        private ITeachersRepository _teachersRepository;

        public TeachersService(ITeachersRepository teachersRepository
            ,  IMapper mapper
        )
        {
            _mapper = mapper;
            _teachersRepository = teachersRepository;
        }
        
        public IQueryable<TeacherViewModel> GetTeachers()
        {
            var teachers = _teachersRepository.GetTeachers().ProjectTo<TeacherViewModel>(_mapper.ConfigurationProvider);
            return teachers;
        }

        public Guid AddTeacher(TeacherViewModel t)
        {
            var newTeacher = _mapper.Map<Teacher>(t);
            _teachersRepository.AddTeacher(newTeacher);
            return newTeacher.Id;
        }
    }
}