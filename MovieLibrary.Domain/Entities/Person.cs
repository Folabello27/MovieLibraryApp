namespace MovieLibrary.Domain.Entities;

using System.ComponentModel.DataAnnotations;

public abstract class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}

public class Actor : Person
{
    public List<Movie> Movies { get; set; } = new();
}


public class Director : Person
{
    public List<Movie> DirectedMovies { get; set; } = new();
}

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Movie> Movies { get; set; } = new();
}




public class Movie
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public int? DirectorId { get; set; }
    public Director? Director { get; set; }

    public int GenreId { get; set; }
    public Genre Genre { get; set; } = null!;

    public DateTime ReleaseDate { get; set; }

    public List<Actor> Actors { get; set; } = new();

    public List<Rating> Ratings { get; set; } = new();
}


public class Rating
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
    public string UserId { get; set; } = string.Empty;
    public int Score { get; set; } // 1-5
}