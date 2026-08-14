using Users;
using Utils;
using Menus;
using ProgramExceptions;

namespace Access;

public class Login
{
    public void LogUser()
    {

        LoginVerifier logVerifier = new LoginVerifier();

        Console.WriteLine("Welcome to the login Menu");
        Console.WriteLine("-------------------------");

        while(true){
            try{
                Console.WriteLine("\nPlease, introduce your Username (Type 0 or blank to cancell the login operation)");
                string? login = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(login) || login.Equals("0"))
                {
                    Console.WriteLine("Cancelling login peration");
                    return;
                }

                User? member = logVerifier.CheckLogin(login);

                Console.WriteLine("\nPlease, introduce your Password (Type 0 or blank to cancell the login operation)");
                string? pass = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(pass) || pass.Equals("0"))
                {
                    Console.WriteLine("Cancelling login operation");
                    return;
                }

                bool validPass = logVerifier.CheckPass(pass, member);

                if(member is null && !validPass)
                {
                    throw new NotValidLoginException("\nThe information provided does not match. Please try again");
                }

                switch (member)
                {
                    case Librarian librarian:
                        LibrarianMenu librarianMenu = new LibrarianMenu();
                        librarianMenu.OpenLibrarianMenu(librarian);
                        break;
                    case User user:
                        UserMenu userMenu = new UserMenu();
                        userMenu.OpenUserMenu(user);
                        break;
                    default:
                        throw new Exception("Unexpected Error");
                }
            }
            catch(NotValidLoginException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
    }
}