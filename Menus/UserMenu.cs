using Users;
using Utils;
using ProgramExceptions;

namespace Menus;

public class UserMenu
{
    int minOption = 1;
    int userMaxOption = 1000;
    int librarianMaxOption = 1001;

    public void OpenUserMenu(User user)
    {

        int maxOption = GetMaxOption(user);

        if(string.IsNullOrWhiteSpace(user.name)){
            Console.WriteLine($"\nWelcome {user.login}");
        }
        else
        {
            Console.WriteLine($"\nWelcome {user.name}");
        }
        
        Console.WriteLine("------------------");

        bool iterate = true;
        string? input = "";
        int option = 0;

        while(iterate){
            try{
                Console.WriteLine("\nPlease select an option");
                Console.WriteLine("1. Make a Loan | 2. Return an Item | 3. ");

                input = Console.ReadLine();

                option = InputValidation.CheckInput(input, minOption, maxOption);

                switch (option)
                {
                    case 1: 
                        NewLoanMenu loanMenu = new NewLoanMenu();
                        loanMenu.OpenLoanMenu(user);
                        break;
                    case 2:

                        break;
                    case 3:
                        iterate = false;
                        break;
                    default:
                        break;
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(EmptyException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(NumberOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
            }
        }
        return;
    }

    public int GetMaxOption(User user)
    {
        return user is Librarian ? librarianMaxOption : userMaxOption;
    }
}