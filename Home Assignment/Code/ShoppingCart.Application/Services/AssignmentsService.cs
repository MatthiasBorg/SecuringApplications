using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Application.Services
{
    public class AssignmentsService : IAssignmentsService
    {
        private IMapper _mapper;
        private IAssignmentsRepository _assignmentsRepo;

        public AssignmentsService(IAssignmentsRepository assignmentsRepo
            ,  IMapper mapper
        )
        {
            _mapper = mapper;
            _assignmentsRepo = assignmentsRepo;
        }
        
        public void AddAssignment(AssignmentViewModel assignment)
        {
            var newAssignment = _mapper.Map<Assignment>(assignment);
            _assignmentsRepo.AddAssignment(newAssignment);
        }
        
        public IQueryable<AssignmentViewModel> GetAssignments()
        {
            var assignments = _assignmentsRepo.GetAssignments().ProjectTo<AssignmentViewModel>(_mapper.ConfigurationProvider);

            return assignments;
        }
        
        public AssignmentViewModel GetAssignment(Guid id)
        {
            var assignment = _assignmentsRepo.GetAssignment(id);
            var result = _mapper.Map<AssignmentViewModel>(assignment);
            return result;
        }

        public IQueryable<AssignmentViewModel> GetAssignmentsByTeacher(Guid id)
        {
            var assignments = _assignmentsRepo.GetAssignments().Where(x => x.Teacher.Id == id).ProjectTo<AssignmentViewModel>(_mapper.ConfigurationProvider);
            return assignments;
        }
        
    }
}
