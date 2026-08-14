using ProgramExceptions;
using Utils;
using Users;
using Data;

namespace Menus;

public class Register
{

    private readonly LibraryContext db = new LibraryContext();

    public void RegisterNewUser()
    {
        Console.WriteLine("\nWelcome to the Register Menu");
        Console.WriteLine("-----------------------");

        string login = CreateLogin();

        if (string.IsNullOrEmpty(login))
        {
            return;
        }

        string pass = CreatePass();

        if (string.IsNullOrEmpty(pass))
        {
            return;
        }

        int id = CreateId();

        User newUser = new User(id, login, pass);

        db.Users.Add(newUser);
        db.SaveChanges();
        Console.WriteLine("User with login " + newUser.login + " added to the Library");
    }

    public static string CreateLogin()
    {
        while(true){
            try{
                Console.WriteLine("\nPlease introduce a Username (Type 0 or blank to exit this menu)");
                Console.WriteLine("The Username must start with uppercase and be 7 characters long. No spaces allowed");

                string? login = Console.ReadLine();

                if (string.IsNullOrEmpty(login) || login.Equals("0"))
                {
                    Console.WriteLine("Leaving Register Menu");
                    return "";
                }
                
                return Verifier.CheckLogin(login);
            }
            catch(FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(ShortStringException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (NotFirstUppercaseException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(WhiteSpaceException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public static string CreatePass()
    {

        while(true){
            try{
                Console.WriteLine("\nPlease introduce a Password for the Account (Type 0 or blank to cancell the whole operation and exit to the main menu)");
                Console.WriteLine("The Password must have 7 caracters an upperletter a lowercase a number and a special character");

                string? pass = Console.ReadLine();
                if(string.IsNullOrEmpty(pass) || pass.Equals("0"))
                {
                    Console.WriteLine("Cancelling register operation");
                    return "";
                }

                pass = Verifier.CheckPass(pass);

                Console.WriteLine("\nPlese write the Password again");
                string? pass2 = Console.ReadLine();

                if (!pass2.Equals(pass) || string.IsNullOrEmpty(pass2))
                {
                    throw new NotMatchException("The passwords dont match. Please try again");
                }

                return pass;
            }
            catch (NotPatternException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(NotMatchException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
    
    public int CreateId()
    {
        
        int newId = 0;

        List<User> users = db.Users.ToList();
        foreach (var u in users)
        {
            if(u.id > newId)
            {
                newId = u.id;
            }
        }

        return newId + 1;
    }
    
}