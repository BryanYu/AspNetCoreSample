using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.EntityFrameworkCore;
using RazorPageWIthEFCore.Data;
using RazorPageWIthEFCore.Models.SchoolViewModels;

namespace RazorPageWIthEFCore.Pages
{
    public class AboutModel : PageModel
    {
        private readonly SchoolContext _context;

        public AboutModel(SchoolContext context)
        {
            _context = context;
        }
        public List<EnrollmentDateGroup> Students { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<EnrollmentDateGroup> data =
                from student in _context.Students
                group student by student.EnrollmentDate
                into dataGroup
                select new EnrollmentDateGroup
                {
                    EnrollmentDate = dataGroup.Key,
                    StudentCount = dataGroup.Count()
                };

            Students = await data.AsNoTracking().ToListAsync();
        }
    }
}