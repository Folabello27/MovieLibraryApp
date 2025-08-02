// MovieLibrary.Data/DesignTimeDbContextFactory.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MovieLibrary.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        
        // Match your runtime database: SQLite
        var connectionString = "Data Source=app.db";
        optionsBuilder.UseSqlite(connectionString); // UseSqlite, not UseSqlServer

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}