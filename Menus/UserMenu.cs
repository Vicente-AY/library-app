using Users;

namespace Menus;

public class UserMenu
{
    public void OpenUserMenu(User user)
    {
        if(user.name is null){
            Console.WriteLine("\nWelcome {user.login}");
        }
        Console.WriteLine("\nWelcome {user.name}");
        Console.WriteLine("------------------");

        
    }
}