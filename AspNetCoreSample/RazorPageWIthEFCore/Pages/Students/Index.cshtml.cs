using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPageWIthEFCore.Data;
using RazorPageWIthEFCore.Models;

namespace RazorPageWIthEFCore.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly RazorPageWIthEFCore.Data.SchoolContext _context;

        public IndexModel(RazorPageWIthEFCore.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<Student> Students { get;set; }

        public string NameSort { get; set; }

        public string DateSort { get; set; }

        public string CurrentFilter { get; set; }

        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            CurrentFilter = searchString;

            IQueryable<Student> studentIQ = from s in _context.Students select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                studentIQ = studentIQ.Where(s =>
                    s.LastName.Contains(searchString) || s.FirstMidName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    studentIQ = studentIQ.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    studentIQ = studentIQ.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    studentIQ = studentIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    studentIQ = studentIQ.OrderBy(s => s.LastName);
                    break;
            }

            Students = await studentIQ.AsNoTracking().ToListAsync();
        }   
    }
}
