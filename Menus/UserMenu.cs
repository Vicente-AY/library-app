using Users;

namespace Menus;

public class UserMenu
{
    int minOption = 0;
    int maxOption = 1000;

    public void OpenUserMenu(User user)
    {

        bool librarian = CheckUserHierarchy(user);

        if(string.IsNullOrWhiteSpace(user.name)){
            Console.WriteLine($"\nWelcome {user.login}");
        }
        else
        {
            Console.WriteLine($"\nWelcome {user.name}");
        }
        
        Console.WriteLine("------------------");

        
    }

    public bool CheckUserHierarchy(User user)
    {
        switch (user)
        {
            case Librarian:
                return true;

            case User:
                maxOption--;
                return false;
            default:
                throw new Exception("Unexpected Error");
        }
    }
}