using Utils;

namespace Items;

public class Book : LibraryItem
{
    public int pages {get; set;} = 0;
    public List<string> author {get; set;} = new List<string>();
    public string isbn {get; set;} = "";
    public string editorial {get; set;} = "";
    public string originalLanguage {get; set;} = "";
    public string versionLanguage {get; set;} = "";

    public override string creator => this.author;

    public Book(int id, string title, int year, string genre, string imageRoute, int pages,
                List<string> author, string isbn, string editorial, string originalLanguage, string versionLanguage) : base(id, 
                title, year, genre, imageRoute)
    {
        this.pages = pages;
        this.author = author;
        this.isbn = isbn;
        this.editorial = editorial;
        this.originalLanguage = originalLanguage;
        this.versionLanguage = versionLanguage;
    }

    private Book() : base(){}  //solo para EF Core
    
    
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
            "Pages: " + this.pages + "\n" +
            "Author/s: " + authorLog + "\n" +
            "ISBN:" + this.isbn + "\n" +
            "Editorial: " + this.editorial + "\n" +
            "Original Language: " + this.originalLanguage + "\n" +
            "Version Language: " + this.versionLanguage + "\n");
    }
}