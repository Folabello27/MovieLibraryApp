namespace MovieLibrary.Models.DTOs;

public class MovieDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public DirectorDTO? Director { get; set; }
    public GenreDTO Genre { get; set; } = null!;
    public List<ActorDTO> Actors { get; set; } = new();
    public List<RatingDTO> Ratings { get; set; } = new();
}

public class DirectorDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}

public class GenreDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class ActorDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}

public class RatingDTO
{
    public int Id { get; set; }
    public int Score { get; set; }
    public string UserId { get; set; } = string.Empty;
}