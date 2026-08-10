using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using utils.Availability;

public abstract class libraryItem{
    
    public int id {get; set; } = 0;
    public String title {get; set;} = "";
    public int year {get; set;} = 0;
    public Availability availability {get; set;} = Availability.Available;
    public String genre {get; set;} = "";
    public String imageRoute {get; set;} = "";
    public int numberOfCopies {get; set;} = 0;

    public libraryItem(int id, String title, int year, Availability availability, String genre, String imageRoute, int numberOfCopies)
    {
        this.id = id;
        this.title = title;
        this.year = year;
        this.availability = availability;
        this. genre = genre;
        this.imageRoute = imageRoute;
        this.numberOfCopies = numberOfCopies;
    }

    public abstract String getData();
}
