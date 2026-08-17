using Microsoft.EntityFrameworkCore;
using Items;
using Users;
using Loans;

namespace Data;

public class LibraryContext : DbContext
{
    public DbSet<LibraryItem> LibraryItems {get; set;}
    public DbSet<Book> Books {get; set;}
    public DbSet<Film> Films {get; set;}
    public DbSet<MusicAlbum> MusicAlbums {get; set;}
    public DbSet<Videogame> Videogames {get; set;}
    public DbSet<User> Users {get; set;}
    public DbSet<Librarian> Librarians {get; set;}
    public DbSet<Loan> Loans {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=library.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasDiscriminator<string>("UserType").HasValue<User>("StandardUser").HasValue<Librarian>("Librarian");

        modelBuilder.Entity<LibraryItem>().HasDiscriminator<string>(i => i.media).HasValue<Book>("Book").HasValue<Film>("Film").HasValue<MusicAlbum>("MusicAlbum").HasValue<Videogame>("Videogame");
    }

}