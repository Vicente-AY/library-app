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
        bool iterate = true;
        string? input = "";
        int option = 0;
        
        string salute = string.IsNullOrWhiteSpace(user.name) ? $"\nWelcome to User's Menu {user.login}. (Account inacctive, talk to an employee)" 
                        : $"\nWelcome to User's Menu {user.name}";
        while(iterate){
            try{
                Console.WriteLine(salute);
                Console.WriteLine("------------------");

                Console.WriteLine("\nPlease select an option");
                Console.WriteLine("1. Make a Loan | 2. Manage Loans | 3. Manage Reservations");

                input = Console.ReadLine();

                option = InputValidation.CheckInput(input, minOption, maxOption);

                switch (option)
                {
                    case 1: 
                        NewLoanMenu loanMenu = new NewLoanMenu();
                        loanMenu.OpenLoanMenu(user);
                        break;
                    case 2:
                        ManageLoansMenu manageLoans = new ManageLoansMenu();
                        manageLoans.OpenManageLoansMenu(user);
                        break;
                    case 3:
                        ManageWaitlistMenu manageWaitlist = new ManageWaitlistMenu();
                        manageWaitlist.ShowWaitlistMenu(user);
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

    private int GetMaxOption(User user)
    {
        return user is Librarian ? librarianMaxOption : userMaxOption;
    }
}