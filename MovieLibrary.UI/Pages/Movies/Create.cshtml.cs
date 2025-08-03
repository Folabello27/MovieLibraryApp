using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieLibrary.Data;
using MovieLibrary.Domain.Entities;

namespace MovieLibrary.UI.Pages.Movies
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "FirstName");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // 🔍 Debug: Check what data is coming in
            if (Movie == null)
            {
                ModelState.AddModelError("", "Movie data is null.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                // 🔍 Log each error
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                    }
                }
                return Page(); // Re-render with errors
            }

            try
            {
                _context.Movies.Add(Movie);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                // 🔥 Log exception
                Console.WriteLine($"Save failed: {ex.Message}");
                ModelState.AddModelError("", "Failed to save movie. Check if Director or Genre exists.");
                return Page();
            }
        }
    }
}
