using ProgramExceptions;
using Utils;

namespace Menus;

public class Register
{

    public void RegisterNewUser()
    {
        Console.WriteLine("Welcome to the Register Menu");
        Console.WriteLine("-----------------------");
        Console.WriteLine("\nPlease introduce a Username (Type 0 or blank to exit this menu)");
        Console.WriteLine("The Username must start with uppercase be 7 characters long with no spaces");

        string? login = Console.ReadLine();
        if (string.IsNullOrEmpty(login) || login.Equals("0"))
        {
            Console.WriteLine("Leaving Register Menu");
            return;
        }
        
        login = Verifier.CheckLogin(login);

        Console.WriteLine("Hercho");
    }

    
    
}