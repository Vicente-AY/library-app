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
        Console.WriteLine("\nWelcome to he Manage Loans Menu");   
        Console.WriteLine("------------------\n");

        bool iterate = true;
        string? input = "";
        int option = 0;

        while(iterate){
            try{
                Console.WriteLine("Please select an option (Type 0 or blank to close the menu)");
                Console.WriteLine("1. See Loans");

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
                        ShowLoans(user);
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

    private void ShowLoans(User user)
    {

        DateTime now = DateTime.Now;
        
        if(user.loanList is null)
        {
            Console.WriteLine("\nYou dont have any current Loan");
            return;
        }

        Console.WriteLine("Your Loans");
        Console.WriteLine("----------\n");

        foreach(var l in user.loanList)
        {

            TimeSpan daysLeft = l.expectedReturn - now;
            int days = daysLeft.Days;

            Console.WriteLine($"Item: {l.id} {l.item.title} Media: {l.item.media}" + 
                $"Expected return: {l.expectedReturn!.ToString("dd/MM/yyyy")} Days left: {days}");
        }
    }
}