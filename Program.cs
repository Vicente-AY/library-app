using Items;
using Menus;

public class Program{
    static void Main(string[] args)
    {


        Book book = new Book(
            1,
            "Don Quijote de la Mancha",
            1605,
            "Novela",
            "images/donquijote.jpg",
            5,
            863,
            new List<String>
            {
                "Miguel de Cervantes"
            },
            1,
            "978-8424118171",
            "Editorial Juventud",
            "Español",
            "Español"
        );

        Film film = new Film(
            2,
            "Inception",
            2010,
            "Ciencia ficción",
            "images/inception.jpg",
            3,
            new List<String>
            {
                "Christopher Nolan"
            },
            "Christopher Nolan",
            148,
            "Warner Bros",
            new List<String>
            {
                "Español",
                "Inglés",
                "Francés"
            },
            "DVD"
        );

        MusicAlbum musicAlbum = new MusicAlbum(
            3,
            "Random Access Memories",
            2013,
            "Electronic",
            "images/random_access_memories.jpg",
            4,
            "Daft Punk",
            new List<String>
            {
                "Give Life Back to Music",
                "Get Lucky",
                "Instant Crush",
                "Lose Yourself to Dance"
            },
            74,
            "Gang Recording Studio",
            "Columbia Records"
        );

        Videogame videogame = new Videogame(
            4,
            "The Witcher 3: Wild Hunt",
            2015,
            "RPG",
            "images/witcher3.jpg",
            2,
            "CD Projekt Red",
            "CD Projekt",
            "PC",
            "REDengine 3",
            new List<String>
            {
                "Español",
                "Inglés",
                "Francés",
                "Alemán"
            }
        );

        MainMenu mm = new MainMenu();
        mm.MainMenuOptions();
    }
}