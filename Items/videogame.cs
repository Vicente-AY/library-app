public class videogame : libraryItem
{
    public String developer {get; set;} = "";
    public String publisher {get; set;} = "";
    public String platform {get; set;} = "";
    public String engine {get; set;} = "";
    public String[] versionLanguages {get; set;} = Array.Empty<String>();

    public videogame(int id, String title, int year, Availability availability, String genre, String imageRoute, int numberOfCopies, 
                    String developer, String publisher, String platform, String engine, String[] versionLanguages)
                    : base(id, title, year, availability, genre, imageRoute, numberOfCopies)
    {
        this.developer = developer;
        this.publisher = publisher;
        this.platform = platform;
        this.engine = engine;
        this.versionLanguages = versionLanguages;
    }

    public override String getData()
    {
        //esto ya mañana que tengo hambre
    }
}