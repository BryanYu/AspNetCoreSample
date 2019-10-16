using System;
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
    public class DeleteModel : PageModel
    {
        private readonly RazorPageWIthEFCore.Data.SchoolContext _context;

        public DeleteModel(RazorPageWIthEFCore.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; }

        public string ErrorMessage { get; set; }

        
        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);

            if (Student == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "Delete failed. Try again";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Students.FindAsync(id);

            if (Student == null)
            {
                return NotFound();
            }
            try
            {
                _context.Students.Remove(Student);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("./Delete",
                    new { id, saveChangesError = true });
            }
        }
    }
}
