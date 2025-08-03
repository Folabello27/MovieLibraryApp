using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Domain.Entities;
using MovieLibrary.Models.DTOs;

namespace MovieLibrary.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public GenresController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreDTO>>> GetGenres()
    {
        return await _context.Genres
            .Select(g => new GenreDTO { Id = g.Id, Name = g.Name })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GenreDTO>> GetGenre(int id)
    {
        var genre = await _context.Genres.FindAsync(id);

        if (genre == null)
            return NotFound();

        return new GenreDTO { Id = genre.Id, Name = genre.Name };
    }

    [HttpPost]
    public async Task<ActionResult<GenreDTO>> CreateGenre(GenreDTO genreDTO)
    {
        var genre = new Genre { Name = genreDTO.Name };
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, new GenreDTO { Id = genre.Id, Name = genre.Name });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGenre(int id, GenreDTO genreDTO)
    {
        if (id != genreDTO.Id)
            return BadRequest();

        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
            return NotFound();

        genre.Name = genreDTO.Name;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
            return NotFound();

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}