using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using Utils;
using Users;

namespace Items;

public abstract class LibraryItem{
    
    public int id {get; set; } = 0;
    public string media {get; set;} = "";
    public string title {get; set;} = "";
    public int year {get; set;} = 0;
    public Availability availability {get; set;} = Availability.Available;
    public string genre {get; set;} = "";
    public string imageRoute {get; set;} = "";
    public List<User> waitList {get; set;} = new List<User>();

    public LibraryItem(int id, string title, int year, string genre, string imageRoute)
    {
        this.id = id;
        this.title = title;
        this.year = year;
        this. genre = genre;
        this.imageRoute = imageRoute;
    }

    protected LibraryItem(){}  //solo para EF Core

    public abstract void getData();
}
