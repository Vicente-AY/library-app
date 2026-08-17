using Users;
using ProgramExceptions;
using Utils;
using Data;
using Items;
using Loans;

namespace Menus;

public class LoanMenu
{
    int minOption = 1;
    int maxOption = 5;
    List<LibraryItem> mediaItems = new List<LibraryItem>();
    CreateLoan cLoan = new CreateLoan();

    public void OpenLoanMenu(User user)
    {
        LibraryContext db = new LibraryContext();
        List<LibraryItem> items = db.LibraryItems.ToList();

        Console.WriteLine("\nWelcome to the loan Menu");
        Console.WriteLine("------------------------\n");

        bool iterate = true;
        string? input = "";
        int option = 0;

        Type? selectedType = null;

        while(iterate){
            try{
                Console.WriteLine("Please select an option (Type 0 or blank to cancell the operation)");
                Console.WriteLine("1. Show all Items | 2. Show items by Media | 3. Show Items by Genre");
                Console.WriteLine("4. Search by Name | 5. Search by Id");

                input = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(input) || input.Equals("0"))
                {
                    Console.WriteLine("Cancelling Loan Operation");
                    return;
                }

                option = InputValidation.CheckInput(input, minOption, maxOption);

                switch (option)
                {
                    case 1: 
                        ShowAllItems(user, items);
                        iterate = false;
                        break;
                    case 2:
                        MediaSelectionMenu mediaMenu = new MediaSelectionMenu();
                        selectedType = mediaMenu.SelectMedia();
                        if(selectedType != null)
                        {
                            ShowItems(user, items, selectedType);
                            iterate = false;
                        }
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

    public void ShowAllItems(User user, List<LibraryItem> items)
    {
        ShowItemsList.ShowItems(items);

        cLoan.LoanCreationFromSelection(user);
    }

    public void ShowItems(User user, List<LibraryItem> items, Type media)
    {
        mediaItems.Clear();

        mediaItems.AddRange(items.Where(i => i.GetType() == media));

        ShowItemsList.ShowItems(mediaItems);

        cLoan.LoanCreationFromSelection(user);
    }

}