﻿using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShoppingCart.Data.Context;

namespace ShoppingCart.Data.Repositories
{
    public class AssignmentsRepository : IAssignmentsRepository
    {
        
        ShoppingCartDbContext _context;
        public AssignmentsRepository(ShoppingCartDbContext context)
        {
            _context = context;

        }
        
        public IQueryable<Assignment> GetAssignmentsByStudent(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid AddAssignment(Assignment a)
        {
            _context.Assignments.Add(a);
            _context.SaveChanges();
            return a.Id;
        }

        // public void DeleteAssignment(Guid id)
        // {
        //     throw new NotImplementedException();
        // }

        public Assignment GetAssignment(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Assignment> GetAssignments()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Assignment> GetAssignmentsByTeacher(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
