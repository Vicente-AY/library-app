public class musicAlbum : libraryItem
{
    String band {get; set;} = "";
    String[] listOfSongs {get; set;} = Array.empty<>();
    int duration {get; set;} = 0; //in minutes
    String recordingStudio {get; set;} = "";
    String label {get; set;} = "";

    public musicAlbum(int id, String title, int year, String genre, String imageRoute, int numberOfCopies, String band, String[] listOfSongs,
                    int duration, String recordingStudio, String label) : base(id, title, year, genre, imageRoute, numberOfCopies){
        this.band = band;
        this.listOfSongs = listOfSongs;
        this.duration = duration;
        this.recordingStudio = recordingStudio;
        this.label = label;
    }

    public override String getData()
    {
        // ahahahahah
    }
}