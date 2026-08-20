using Users;
using ProgramExceptions;
using Utils;
using Loans;

namespace Menus;

public class ManageLoansMenu
{
    int minOption = 1;
    int maxOption = 1000;
    public void OpenManageLoansMenu(User user)
    {
        Console.WriteLine("\nWelcome to he Loan Management Menu");   
        Console.WriteLine("------------------\n");

        if(user.loanList is null)
        {
            Console.WriteLine("\nYou dont have any current Loans. Closing Loan Management Menu");
            return;
        }

        bool iterate = true;
        string? input = "";
        int option = 0;

        while(iterate){
            try{
                Console.WriteLine("Please select an option (Type 0 or blank to close the menu)");
                Console.WriteLine("1. Consult Loans | 2. Return Item/s");

                input = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(input) || input.Equals("0"))
                {
                    Console.WriteLine("Returning to User Main Manu");
                    return;
                }

                option = InputValidation.CheckInput(input, minOption, maxOption);

                switch (option)
                {
                    case 1: 
                        ShowUserLoans userLoans = new ShowUserLoans();
                        userLoans.ShowLoans(user);
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
}