using Utils;

namespace Items;

public class Videogame : LibraryItem
{
    public String developer {get; set;} = "";
    public String publisher {get; set;} = "";
    public String platform {get; set;} = "";
    public String engine {get; set;} = "";
    public String[] versionLanguages {get; set;} = Array.Empty<String>();

    public Videogame(int id, String title, int year, String genre, String imageRoute, int numberOfCopies, 
                    String developer, String publisher, String platform, String engine, String[] versionLanguages)
                    : base(id, title, year, genre, imageRoute, numberOfCopies)
    {
        this.developer = developer;
        this.publisher = publisher;
        this.platform = platform;
        this.engine = engine;
        this.versionLanguages = versionLanguages;
    }

    public override String getData()
    {
        return "games";
    }
}