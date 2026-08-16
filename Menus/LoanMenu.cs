using Users;
using ProgramExceptions;
using Utils;
using Data;
using Items;
using Loans;

namespace Menu;

public class LoanMenu
{
    int minOption = 1;
    int maxOption = 4;
    public void OpenLoanMenu(User user)
    {

        LibraryContext db = new LibraryContext();
        List<LibraryItem> items = new List<LibraryItem>();

        Console.WriteLine("\nWelcome to the loan Menu");
        Console.WriteLine("------------------------\n");

        bool iterate = true;
        string? input = "";
        int option = 0;

        while(iterate){
            try{
                Console.WriteLine("Please select an option");
                Console.WriteLine("1. Show all Items | 2. Make selection by Media | 3. Make selection by Genre");
                Console.WriteLine("4. Exit");

                input = Console.ReadLine();

                option = InputValidation.CheckInput(input, minOption, maxOption);

                switch (option)
                {
                    case 1: 
                        items = db.LibraryItems.ToList();
                        ShowItemsList.ShowItems(items);

                        CreateLoan cLoan = new CreateLoan();
                        cLoan.LoanCreation(user);
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
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        return;
    }
}