using Utils;

namespace Items;

public class Film : LibraryItem
{
    public String[] director {get; set;} = Array.Empty<String>();
    public String screenWriter {get; set;} = "";
    public int duration {get; set;} = 0; //in minutes
    public String productionCompany {get; set;} = "";
    public String[] versionLanguages {get; set;} = Array.Empty<String>();
    public String format {get; set;} = ""; //VHS, DVD

    public Film(int id, String title, int year, String genre, String imageRoute, int numberOfCopies, 
                String[] director, String screenWriter, int duration, String productionCompany, String[] versionLanguages, String format)
                : base(id, title, year, genre, imageRoute, numberOfCopies)
    {
        this.director = director;
        this.screenWriter = screenWriter;
        this.duration = duration;
        this.productionCompany = productionCompany;
        this.versionLanguages = versionLanguages;
        this.format = format;
    }

    public override String getData()
    {
        return "Films";
    }
}