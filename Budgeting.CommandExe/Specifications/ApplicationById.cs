﻿using System.Linq;
using Budgeting.CommandExe.Models;
using Highway.Data.QueryObjects;

namespace Budgeting.CommandExe.Specifications
{
    public class ApplicationById : Scalar<Application>
    {
        public ApplicationById(int id)
        {
            ContextQuery = m => m.AsQueryable<Application>().FirstOrDefault(a => a.Id == id);
        }
    }
}