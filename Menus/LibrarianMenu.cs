using Users;

namespace Menus;

public class LibrarianMenu
{
    int minOption = 0;
    int maxOption = 1000;
    public void OpenLibrarianMenu(Librarian librarian)
    {
        if(librarian.name is null){
            Console.WriteLine($"\nWelcome {librarian.login}");
        }
        else
        {
            Console.WriteLine($"\nWelcome {librarian.name}");
        }
        
        Console.WriteLine("------------------");

        
    }
}