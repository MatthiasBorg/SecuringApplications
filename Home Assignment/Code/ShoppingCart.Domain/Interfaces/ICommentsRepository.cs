﻿using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface ICommentsRepository
    {
        Comment GetComment(Guid id);
        
        IQueryable<Comment> GetComments();
        
        IQueryable<Comment> GetCommentsByAssignment(Guid id);

        Guid AddComment(Comment c);

    }
}