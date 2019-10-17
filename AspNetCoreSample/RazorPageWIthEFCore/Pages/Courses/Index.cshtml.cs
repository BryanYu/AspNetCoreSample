using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPageWIthEFCore.Data;
using RazorPageWIthEFCore.Models;
using RazorPageWIthEFCore.Models.SchoolViewModels;

namespace RazorPageWIthEFCore.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly RazorPageWIthEFCore.Data.SchoolContext _context;

        public IndexModel(RazorPageWIthEFCore.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<CourseViewModel> Courses { get;set; }

        public async Task OnGetAsync()
        {
            Courses = await _context.Courses.Select(p => new CourseViewModel
            {
                CourseID = p.CourseID,
                Title = p.Title,
                Credits = p.Credits,
                DepartmentName = p.Department.Name
            }).ToListAsync();
        }
    }
}
