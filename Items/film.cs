using Utils;

namespace Items;

public class Film : LibraryItem
{
    public List<string> director {get; set;} = new List<string>();
    public string screenWriter {get; set;} = "";
    public int duration {get; set;} = 0; //in minutes
    public string productionCompany {get; set;} = "";
    public List<string> versionLanguages {get; set;} = new List<string>();
    public string format {get; set;} = ""; //VHS, DVD

    public Film(int id, string title, int year, string genre, string imageRoute, List<string> director, string screenWriter, int duration,
                string productionCompany, List<string> versionLanguages, string format) : base(id, title, year, genre, imageRoute)
    {
        this.director = director;
        this.screenWriter = screenWriter;
        this.duration = duration;
        this.productionCompany = productionCompany;
        this.versionLanguages = versionLanguages;
        this.format = format;
    }

    private Film() : base(){}  //solo para EF Core
    public override void getData()
    {
        
        string directorLog = "";

        for(int i = 0; i < director.Count; i++)
        {
            directorLog += director[i];

            if(!(i + 1 == director.Count))
            {
                directorLog += ", ";
            }
        }

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
            "Director/s: " + directorLog + ". " + "\n" +
            "Screenwriter/s " + this.screenWriter + ". " + "\n" +
            "Duration: " + this.duration + " minutes" + ". " + "\n" +
            "Production Company " + this.productionCompany + ". " + "\n" +
            "Available Languages: " + languagesLog + ". " + "\n" +
            "Available Format : " + this.format + ". " + "\n");
    }
}