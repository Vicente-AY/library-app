using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using Utils;

namespace Items;

public abstract class LibraryItem{
    
    public int id {get; set; } = 0;
    public string title {get; set;} = "";
    public int year {get; set;} = 0;
    public Availability availability {get; set;} = Availability.Available;
    public string genre {get; set;} = "";
    public string imageRoute {get; set;} = "";
    public int numberOfCopies {get; set;} = 0;

    public LibraryItem(int id, string title, int year, string genre, string imageRoute, int numberOfCopies)
    {
        this.id = id;
        this.title = title;
        this.year = year;
        this. genre = genre;
        this.imageRoute = imageRoute;
        this.numberOfCopies = numberOfCopies;
    }

    public abstract void getData();
}
