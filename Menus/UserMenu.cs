using Users;

namespace Menus;

public class UserMenu
{
    public void OpenUserMenu(User user)
    {
        if(string.IsNullOrWhiteSpace(user.name)){
            Console.WriteLine($"\nWelcome {user.login}");
        }
        else
        {
            Console.WriteLine($"\nWelcome {user.name}");
        }
        
        Console.WriteLine("------------------");

        
    }
}