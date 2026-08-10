public class film : libraryItem
{
    String director {get; set;} = [];
    String screenWriter {get; set;} = "";
    int duration {get; set;} = 0; //in minutes
    String productionCompany {get; set;} = "";
    String versionLanguages {get; set;} = [];
    String format {get; set;} = ""; //VHS, DVD

    public film(int id, String title, int year, Availability availability, String genre, String imageRoute, int numberOfCopies, 
                String[] director, String screenWriter, int duration, String productionCompany, String versionLanguages, String format)
                : base(id, title, year, availability, genre, imageRoute, numberOfCopies)
    {
        this.director = director;
        this.screenWriter = screenWriter;
        this.duration = duration;
        this.productionCompany = productionCompany;
        this.versionLanguages = versionLanguages;
        this.format = format;
    }

    public String getData()
    {
        //algo se me ocurrirá
    }
}