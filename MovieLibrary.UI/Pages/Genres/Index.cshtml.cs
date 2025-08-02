using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Domain.Entities;

namespace MovieLibrary.UI.Pages.Genres
{
    public class IndexModel : PageModel
    {
        private readonly MovieLibrary.Data.ApplicationDbContext _context;

        public IndexModel(MovieLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Genre> Genre { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Genre = await _context.Genres.ToListAsync();
        }
    }
}
