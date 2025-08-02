using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Domain.Entities;

namespace MovieLibrary.UI.Pages.Actors
{
    public class DeleteModel : PageModel
    {
        private readonly MovieLibrary.Data.ApplicationDbContext _context;

        public DeleteModel(MovieLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Actor Actor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.FirstOrDefaultAsync(m => m.Id == id);

            if (actor == null)
            {
                return NotFound();
            }
            else
            {
                Actor = actor;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.FindAsync(id);
            if (actor != null)
            {
                Actor = actor;
                _context.Actors.Remove(Actor);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
