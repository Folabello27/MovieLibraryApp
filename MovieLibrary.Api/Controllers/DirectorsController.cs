using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Domain.Entities;
using MovieLibrary.Models.DTOs;

namespace MovieLibrary.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DirectorsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DirectorsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DirectorDTO>>> GetDirectors()
    {
        return await _context.Directors
            .Select(d => new DirectorDTO
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                BirthDate = d.BirthDate
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DirectorDTO>> GetDirector(int id)
    {
        var director = await _context.Directors.FindAsync(id);

        if (director == null)
            return NotFound();

        return new DirectorDTO
        {
            Id = director.Id,
            FirstName = director.FirstName,
            LastName = director.LastName,
            BirthDate = director.BirthDate
        };
    }

    [HttpPost]
    public async Task<ActionResult<DirectorDTO>> CreateDirector(DirectorDTO directorDTO)
    {
        var director = new Director
        {
            FirstName = directorDTO.FirstName,
            LastName = directorDTO.LastName,
            BirthDate = directorDTO.BirthDate
        };

        _context.Directors.Add(director);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDirector), new { id = director.Id }, new DirectorDTO
        {
            Id = director.Id,
            FirstName = director.FirstName,
            LastName = director.LastName,
            BirthDate = director.BirthDate
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDirector(int id, DirectorDTO directorDTO)
    {
        if (id != directorDTO.Id)
            return BadRequest();

        var director = await _context.Directors.FindAsync(id);
        if (director == null)
            return NotFound();

        director.FirstName = directorDTO.FirstName;
        director.LastName = directorDTO.LastName;
        director.BirthDate = directorDTO.BirthDate;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDirector(int id)
    {
        var director = await _context.Directors.FindAsync(id);
        if (director == null)
            return NotFound();

        _context.Directors.Remove(director);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}