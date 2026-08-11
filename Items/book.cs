using Utils;

namespace Items;

public class Book : LibraryItem
{
    public int pages {get; set;} = 0;
    public String[] author {get; set;} = Array.Empty<String>();
    public int edition {get; set;} = 0;
    public String isbn {get; set;} = "";
    public String editorial {get; set;} = "";
    public String originalLanguage {get; set;} = "";
    public String versionLanguage {get; set;} = "";

    public Book(int id, String title, int year, String genre, String imageRoute, int numberOfCopies, int pages,
                String[] author, int edition, String isbn, String editorial, String originalLanguage, String versionLanguage) : base(id, 
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
    
    
    public override String getData()
    {
        return "Books";
    }
}