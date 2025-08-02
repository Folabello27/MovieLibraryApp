using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Domain.Entities;

namespace MovieLibrary.UI.Pages.Directors
{
    public class DetailsModel : PageModel
    {
        private readonly MovieLibrary.Data.ApplicationDbContext _context;

        public DetailsModel(MovieLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Director Director { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Directors.FirstOrDefaultAsync(m => m.Id == id);
            if (director == null)
            {
                return NotFound();
            }
            else
            {
                Director = director;
            }
            return Page();
        }
    }
}
