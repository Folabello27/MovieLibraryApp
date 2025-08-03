using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Domain.Entities;
using MovieLibrary.Models.DTOs;

namespace MovieLibrary.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActorsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ActorsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActorDTO>>> GetActors()
    {
        return await _context.Actors
            .Select(a => new ActorDTO
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                BirthDate = a.BirthDate
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActorDTO>> GetActor(int id)
    {
        var actor = await _context.Actors.FindAsync(id);

        if (actor == null)
            return NotFound();

        return new ActorDTO
        {
            Id = actor.Id,
            FirstName = actor.FirstName,
            LastName = actor.LastName,
            BirthDate = actor.BirthDate
        };
    }

    [HttpPost]
    public async Task<ActionResult<ActorDTO>> CreateActor(ActorDTO actorDTO)
    {
        var actor = new Actor
        {
            FirstName = actorDTO.FirstName,
            LastName = actorDTO.LastName,
            BirthDate = actorDTO.BirthDate
        };

        _context.Actors.Add(actor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetActor), new { id = actor.Id }, new ActorDTO
        {
            Id = actor.Id,
            FirstName = actor.FirstName,
            LastName = actor.LastName,
            BirthDate = actor.BirthDate
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateActor(int id, ActorDTO actorDTO)
    {
        if (id != actorDTO.Id)
            return BadRequest();

        var actor = await _context.Actors.FindAsync(id);
        if (actor == null)
            return NotFound();

        actor.FirstName = actorDTO.FirstName;
        actor.LastName = actorDTO.LastName;
        actor.BirthDate = actorDTO.BirthDate;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActor(int id)
    {
        var actor = await _context.Actors.FindAsync(id);
        if (actor == null)
            return NotFound();

        _context.Actors.Remove(actor);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}