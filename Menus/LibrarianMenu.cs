using Users;

namespace Menus;

public class LibrarianMenu
{
    public void OpenLibrarianMenu(Librarian librarian)
    {
        if(librarian.name is null){
            Console.WriteLine("\nWelcome {librarian.login}");
        }
        Console.WriteLine("\nWelcome {librarian.name}");
        Console.WriteLine("------------------");

        
    }
}