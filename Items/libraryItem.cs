using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using Utils;

namespace Items;

public abstract class LibraryItem{
    
    public int id {get; set; } = 0;
    public String title {get; set;} = "";
    public int year {get; set;} = 0;
    public Availability availability {get; set;} = Availability.Available;
    public String genre {get; set;} = "";
    public String imageRoute {get; set;} = "";
    public int numberOfCopies {get; set;} = 0;

    public LibraryItem(int id, String title, int year, String genre, String imageRoute, int numberOfCopies)
    {
        this.id = id;
        this.title = title;
        this.year = year;
        this. genre = genre;
        this.imageRoute = imageRoute;
        this.numberOfCopies = numberOfCopies;
    }

    public abstract String getData();
}
