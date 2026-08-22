using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using Utils;
using Users;
using Loans;

namespace Items;

public abstract class LibraryItem{
    
    public int id {get; set; } = 0;
    public string media {get; set;} = "";
    public string title {get; set;} = "";
    public int year {get; set;} = 0;
    public Availability availability {get; set;} = Availability.Available;
    public DateTime? maintenanceEntry {get; set;} = null;
    public DateTime? mainteneanceExit {get; set;} = null;
    public string genre {get; set;} = "";
    public string imageRoute {get; set;} = "";
    public List<WaitEntry> waitList {get; set;} = new List<WaitEntry>();
    public bool lost {get; set;} = false;

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
