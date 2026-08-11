using Utils;

namespace Items;

public class Videogame : LibraryItem
{
    public string developer {get; set;} = "";
    public string publisher {get; set;} = "";
    public string platform {get; set;} = "";
    public string engine {get; set;} = "";
    public List<string> versionLanguages {get; set;} = new List<string>();

    public Videogame(int id, string title, int year, string genre, string imageRoute, int numberOfCopies, 
                    string developer, string publisher, string platform, string engine, List<string> versionLanguages)
                    : base(id, title, year, genre, imageRoute, numberOfCopies)
    {
        this.developer = developer;
        this.publisher = publisher;
        this.platform = platform;
        this.engine = engine;
        this.versionLanguages = versionLanguages;
    }

    public override void getData()
    {

        string languagesLog = "";

        for(int i = 0; i < versionLanguages.Count; i++)
        {
            languagesLog += versionLanguages[i];

            if(!(i + 1 == versionLanguages.Count))
            {
                languagesLog += ", ";
            }
        }

        Console.WriteLine(
            "Item Id: " + this.id + ". " + "\n" +
            "Title: " + this.title + ". " + " \n" +
            "Release year: " + this.year + ". " + "\n" +
            "Genre: " + this.genre + ". " + "\n" +
            "Available copies: " + this.numberOfCopies + ". " + "\n" +
            "Developer: " + this.developer + ". " + "\n" +
            "Publisher: " + this.publisher + ". " + "\n" +
            "Platform: " + this.platform + ". " + "\n" +
            "Engine: " + this.engine + ". " + "\n" +
            "Available Languages: " + languagesLog + ". " + "\n");                  
    }
}