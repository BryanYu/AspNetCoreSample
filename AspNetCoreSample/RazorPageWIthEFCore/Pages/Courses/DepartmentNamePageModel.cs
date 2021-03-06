﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPageWIthEFCore.Data;

namespace RazorPageWIthEFCore.Pages.Courses
{
    public class DepartmentNamePageModel : PageModel
    {
        public SelectList DepartmentNameSL { get; set; }

        public void PopulateDepartmentsDropDownList(SchoolContext context, object selectDepartment = null)
        {
            var departmentsQuery = from d in context.Departments
                orderby d.Name
                select d;

            DepartmentNameSL =
                new SelectList(departmentsQuery.AsNoTracking(), "DepartmentID", "Name", selectDepartment);
        }
    }
}
