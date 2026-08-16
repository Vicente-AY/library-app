using Utils;

namespace Items;

public class MusicAlbum : LibraryItem
{
    public string band {get; set;} = "";
    public List<string> listOfSongs {get; set;} = new List<string>();
    public int duration {get; set;} = 0; //in minutes
    public string recordingStudio {get; set;} = "";
    public string label {get; set;} = "";

    public MusicAlbum(int id, string title, int year, string genre, string imageRoute, string band, List<string> listOfSongs,
                    int duration, string recordingStudio, string label) : base(id, title, year, genre, imageRoute){

        this.band = band;
        this.listOfSongs = listOfSongs;
        this.duration = duration;
        this.recordingStudio = recordingStudio;
        this.label = label;
    }
    private MusicAlbum(){} //solo para EF Core

    public override void getData()
    {
        
        string songLog = "";

        for(int i = 0; i < listOfSongs.Count; i++)
        {
            songLog += listOfSongs[i];

            if(!(i + 1 == listOfSongs.Count))
            {
                songLog += ", ";
            }
        }

        Console.WriteLine(
            "Item Id: " + this.id + ". " + "\n" +
            "Title: " + this.title + ". " + " \n" +
            "Release year: " + this.year + ". " + "\n" +
            "Genre: " + this.genre + ". " + "\n" +
            "Band: " + this.band + ". " + "\n" +
            "Songs: " + songLog + ". " + "\n" +
            "Duration: " + this.duration + " minutes" + ". " + "\n" +
            "Studio: " + this.recordingStudio + ". " + "\n" +
            "Lablel: " + this.label + ". " + "\n");        
    }
}