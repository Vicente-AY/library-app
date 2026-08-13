using ProgramExceptions;

namespace Menus;

public class Register
{

    public void RegisterNewUser()
    {
        Console.WriteLine("Welcome to the Register Menu");
        Console.WriteLine("-----------------------");
        Console.WriteLine("\n Please introduce a Username (Type 0 or blank to exit this menu)");
        Console.WriteLine("The Username must start with uppercase be 7 characters long with no spaces");

        string? login = Console.ReadLine();
        if (string.IsNullOrEmpty(login) || login.Equals(0))
        {
            Console.WriteLine("Leaving Register Menu");
            return;
        }

        login = CheckLogin(login);
        


    }

    public string CheckLogin(string login)
    {

        login = login.Trim();

        if (int.TryParse(login, out _))
        {
            throw new FormatException("You type only numbers. Please, enter a valid Username");
        }
        if(login.Length < 7)
        {
            throw new ShortStringException("The login is too short. Please, enter a valid Username");
        }
        if(!Char.IsUpper(login[0]))
        {
            throw new NotFirstUppercaseException("The login does not start with an uppercase. Please, enter a valid Username");
        }
        if(login.Contains(" "))
        {
            throw new WhiteSpaceException("The login has spaces. Please, enter a valid Username");
        }

        return login;
    }
    
}