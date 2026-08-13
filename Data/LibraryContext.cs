using Microsoft.EntityFrameworkCore;
using Items;
using Users;

namespace Data;

public class LibraryContext : DbContext
{
    public DbSet<Book> books {get; set;}
    public DbSet<Film> films {get; set;}
    public DbSet<MusicAlbum> musicAlbums {get; set;}
    public DbSet<Videogame> videogames {get; set;}
    public DbSet<User> users {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=library.db");
    }

}