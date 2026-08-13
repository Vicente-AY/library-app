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
        Console.WriteLine("The Username must start with uppercase and be 7 characters long. No spaces allowed");

        string? login = Console.ReadLine();
        if (string.IsNullOrEmpty(login) || login.Equals("0"))
        {
            Console.WriteLine("Leaving Register Menu");
            return;
        }
        
        login = Verifier.CheckLogin(login);

        Console.WriteLine("\nPlease introduce a Password for the Account (Type 0 or blank to cancell the whole operation and exit to the main menu)");
        Console.WriteLine("The Password must have 7 caracters an upperletter a lowercase a number and a special character");

        string? pass = Console.ReadLine();
        if(string.IsNullOrEmpty(login) || login.Equals("0"))
        {
            Console.WriteLine("Cancelling register operation");
            return;
        }

        login = Verifier.CheckPass(pass);
    }

    
    
}