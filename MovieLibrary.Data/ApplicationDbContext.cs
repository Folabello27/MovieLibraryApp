using Microsoft.EntityFrameworkCore;
using MovieLibrary.Domain.Entities;

namespace MovieLibrary.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Actor> Actors => Set<Actor>();
    public DbSet<Director> Directors => Set<Director>();
    public DbSet<Rating> Ratings => Set<Rating>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure inheritance for Person
        modelBuilder.Entity<Actor>().ToTable("Actors");
        modelBuilder.Entity<Director>().ToTable("Directors");

        // Seed Genres
        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Action" },
            new Genre { Id = 2, Name = "Comedy" },
            new Genre { Id = 3, Name = "Drama" }
        );

        // Optional: Seed Movies (will be done via migration later)
    }
}