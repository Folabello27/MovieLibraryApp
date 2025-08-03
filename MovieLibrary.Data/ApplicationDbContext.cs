using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Domain.Entities;

namespace MovieLibrary.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Actor> Actors => Set<Actor>();
    public DbSet<Director> Directors => Set<Director>();
    public DbSet<Rating> Ratings => Set<Rating>();

    // 🔴 REQUIRED FOR ROLES TO WORK
    public DbSet<IdentityRole> Roles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // 🔥 MUST call this!

        // Your configurations
        modelBuilder.Entity<Actor>().ToTable("Actors");
        modelBuilder.Entity<Director>().ToTable("Directors");

        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Action" },
            new Genre { Id = 2, Name = "Comedy" },
            new Genre { Id = 3, Name = "Drama" }
        );

        modelBuilder.Entity<Director>().HasData(
            new Director { Id = 1, FirstName = "Christopher", LastName = "Nolan", BirthDate = new DateTime(1970, 7, 30) },
            new Director { Id = 2, FirstName = "Quentin", LastName = "Tarantino", BirthDate = new DateTime(1963, 3, 27) },
            new Director { Id = 3, FirstName = "Martin", LastName = "Scorsese", BirthDate = new DateTime(1942, 11, 17) }
        );
    }
}