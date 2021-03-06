﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPageWIthEFCore.Data;
using RazorPageWIthEFCore.Models;

namespace RazorPageWIthEFCore.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPageWIthEFCore.Data.SchoolContext _context;

        public DetailsModel(RazorPageWIthEFCore.Data.SchoolContext context)
        {
            _context = context;
        }

        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Students
                .Include(item => item.Enrollments)
                .ThenInclude(item2 => item2.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(item3 => item3.ID == id);

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
