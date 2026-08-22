using Users;
using ProgramExceptions;
using Utils;
using Loans;
using Data;
using Items;

namespace Menus;

public class ManageLoansMenu
{
    int minOption = 1;
    int maxOption = 1000;
    public void OpenManageLoansMenu(User user)
    {
        Console.WriteLine("\nWelcome to Loan Management Menu");   
        Console.WriteLine("---------------------------------\n");

        if(user.loanList is null)
        {
            Console.WriteLine("You dont have any current Loans. Closing Loan Management Menu");
            return;
        }

        bool iterate = true;
        string? input = "";
        int option = 0;

        while(iterate){
            try{
                Console.WriteLine("Please select an option");
                Console.WriteLine("1. Consult Loans | 2. Return Item/s | 3. Ask for loan extension");
                Console.WriteLine("4. Exit");

                input = Console.ReadLine();

                option = InputValidation.CheckInput(input, minOption, maxOption);

                switch (option)
                {
                    case 1: 
                        ShowUserLoans userLoans = new ShowUserLoans();
                        userLoans.ShowLoans(user);
                        break;
                    case 2:
                        LoanTermination lTerm = new LoanTermination();
                        lTerm.ReturnItems(user);
                        break;
                    case 3:
                        LoanExtension lExt = new LoanExtension();
                        lExt.ExtendLoan(user);
                        break;
                    case 4:
                        Console.WriteLine("\nReturning to User Main Manu");
                        return;
                    default:
                        Console.WriteLine("\nUnrecogniced Option. Plese select a valid one");
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
                Console.WriteLine($"\nUnexpected error: {e.Message}");
            }
        }
        return;
    }
}