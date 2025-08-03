using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Domain.Entities;
using MovieLibrary.Models.DTOs;
using System.Linq;

namespace MovieLibrary.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MoviesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
    {
        var movies = await _context.Movies
            .Include(m => m.Genre)
            .Include(m => m.Director)
            .Include(m => m.Actors)
            .Include(m => m.Ratings)
            .ToListAsync();

        return movies.Select(m => MapToMovieDTO(m)).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDTO>> GetMovie(int id)
    {
        var movie = await _context.Movies
            .Include(m => m.Genre)
            .Include(m => m.Director)
            .Include(m => m.Actors)
            .Include(m => m.Ratings)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
            return NotFound();

        return MapToMovieDTO(movie);
    }

    [HttpPost]
    public async Task<ActionResult<MovieDTO>> CreateMovie(MovieDTO movieDTO)
    {
        var movie = new Movie
        {
            Title = movieDTO.Title,
            ReleaseDate = movieDTO.ReleaseDate,
            GenreId = movieDTO.Genre.Id,
            DirectorId = movieDTO.Director?.Id,
            Actors = movieDTO.Actors.Select(a => new Actor { Id = a.Id }).ToList()
        };

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, MapToMovieDTO(movie));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(int id, MovieDTO movieDTO)
    {
        if (id != movieDTO.Id)
            return BadRequest();

        var movie = await _context.Movies
            .Include(m => m.Actors)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
            return NotFound();

        movie.Title = movieDTO.Title;
        movie.ReleaseDate = movieDTO.ReleaseDate;
        movie.GenreId = movieDTO.Genre.Id;
        movie.DirectorId = movieDTO.Director?.Id;

        // Update actors
        movie.Actors = movieDTO.Actors.Select(a => new Actor { Id = a.Id }).ToList();

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MovieExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
            return NotFound();

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MovieExists(int id) => _context.Movies.Any(e => e.Id == id);

    private static MovieDTO MapToMovieDTO(Movie movie)
    {
        return new MovieDTO
        {
            Id = movie.Id,
            Title = movie.Title,
            ReleaseDate = movie.ReleaseDate,
            Director = movie.Director != null ? new DirectorDTO
            {
                Id = movie.Director.Id,
                FirstName = movie.Director.FirstName,
                LastName = movie.Director.LastName,
                BirthDate = movie.Director.BirthDate
            } : null,
            Genre = new GenreDTO
            {
                Id = movie.Genre.Id,
                Name = movie.Genre.Name
            },
            Actors = movie.Actors.Select(a => new ActorDTO
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                BirthDate = a.BirthDate
            }).ToList(),
            Ratings = movie.Ratings.Select(r => new RatingDTO
            {
                Id = r.Id,
                Score = r.Score,
                UserId = r.UserId
            }).ToList()
        };
    }
}