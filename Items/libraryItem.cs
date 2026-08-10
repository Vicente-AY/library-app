using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using utils.Availability;

public abstract class libraryItem{
    
    int id {get; set; } = 0;
    String title {get; set;} = "";
    int year {get; set;} = 0;
    Availability availability {get; set;} = Availability.Available;
    String genre {get; set;} = "";
    String imageRoute {get; set;} = "";
    int numberOfCopies {get; set;} = 0;

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
