using Utils;

namespace Items;

public class Book : LibraryItem
{
    public int pages {get; set;} = 0;
    public List<string> author {get; set;} = new List<string>();
    public int edition {get; set;} = 0;
    public string isbn {get; set;} = "";
    public string editorial {get; set;} = "";
    public string originalLanguage {get; set;} = "";
    public string versionLanguage {get; set;} = "";

    public Book(int id, string title, int year, string genre, string imageRoute, int numberOfCopies, int pages,
                List<string> author, int edition, string isbn, string editorial, string originalLanguage, string versionLanguage) : base(id, 
                title, year, genre, imageRoute, numberOfCopies)
    {
        this.pages = pages;
        this.author = author;
        this.edition = edition;
        this.isbn = isbn;
        this.editorial = editorial;
        this.originalLanguage = originalLanguage;
        this.versionLanguage = versionLanguage;
    }
    
    
    public override void getData()
    {

        String authorLog = "";

        for(int i = 0; i < author.Count; i++)
        {
            authorLog += author[i];

            if(!(i + 1 == author.Count))
            {
                authorLog += ", ";
            }
        }

        Console.WriteLine(
            "Item Id: " + this.id + " \n" +
            "Title: " + this.title + " \n" +
            "Release year: " + this.year + "\n" +
            "Genre: " + this.genre + "\n" +
            "Available copies: " + this.numberOfCopies + "\n" +
            "Pages: " + this.pages + "\n" +
            "Author/s: " + authorLog + "\n" +
            "Edition: " + this.edition + "\n" +
            "ISBN:" + this.isbn + "\n" +
            "Editorial: " + this.editorial + "\n" +
            "Original Language: " + this.originalLanguage + "\n" +
            "Version Language: " + this.versionLanguage + "\n");
    }
}