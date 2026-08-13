using Items;
using Menus;
using Data;
using Users;

public class Program{
    static void Main(string[] args)
    {
        using (var db = new LibraryContext())
        {
            db.Database.EnsureCreated();
        }

        MainMenu mm = new MainMenu();
        mm.MainMenuOptions();
    }
}