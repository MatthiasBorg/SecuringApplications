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

        public Guid AddAssignment(Assignment a)
        {
            a.Teacher = null;
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
            return _context.Assignments.SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<Assignment> GetAssignments()
        {
            return _context.Assignments;
        }

        // public IQueryable<Assignment> GetAssignmentsByTeacher(Guid id)
        // {
        //     //return _context.Assignments.Fin
        //     throw new NotImplementedException();
        // }
    }
}
