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
    List<LibraryItem> searchedItems = new List<LibraryItem>();
    CreateLoan cLoan = new CreateLoan();

    public void OpenLoanMenu(User user)
    {
        LibraryContext db = new LibraryContext();
        List<LibraryItem> items = db.LibraryItems.Where(i => !i.lost).ToList();

        Console.WriteLine("\nWelcome to the loan Menu");
        Console.WriteLine("------------------------\n");

        bool iterate = true;
        string? input = "";
        int option = 0;

        Type? selectedType = null;

        while(iterate){
            try{
                Console.WriteLine("Please select an option (Type 0 or blank to cancell the operation)");
                Console.WriteLine("1. Show all Items | 2. Show items by Media | 3. Search Items by Name");
                Console.WriteLine("4. Search Items by Genre | 5. Search Item by Id");

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
                        Console.WriteLine("\nEnter the Name of the item you are looking for");
                        string? name = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            Console.WriteLine("Please enter a valid value");
                            break;
                        }
                        SearchByName(user, items, name);
                        iterate = false;
                        break;
                    case 4:
                        Console.WriteLine("\nEnter the Genre you are looking for");
                        string? genre = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(genre))
                        {
                            Console.WriteLine("Please enter a valid value");
                            break;
                        }
                        SearchByGenre(user, items, genre);
                        iterate = false;
                        break;
                    case 5:
                        Console.WriteLine("\nEnter the Id of the Item your are looking for");
                        string? id = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(id))
                        {
                            Console.WriteLine("Please enter a valid value");
                            break;
                        }
                        SearchById(user, items, id);
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
                Console.WriteLine(e.Message);
            }
        }
        return;
    }

    public void ShowAllItems(User user, List<LibraryItem> items)
    {

        if(items is null)
        {
            Console.WriteLine("Sorry, there is no items yet");
            return;
        }

        ShowItemsList.ShowItems(items);

        cLoan.LoanCreationFromSelection(user);
    }

    public void ShowItems(User user, List<LibraryItem> items, Type media)
    {
        searchedItems.Clear();
        searchedItems.AddRange(items.Where(i => i.GetType() == media));

        if(searchedItems is null)
        {
            Console.WriteLine("Sorry, there is no item with selected media");
            return;
        }

        ShowItemsList.ShowItems(searchedItems);

        cLoan.LoanCreationFromSelection(user);
    }

    public void SearchByName(User user, List<LibraryItem> items, string name)
    {
        searchedItems.Clear();
        searchedItems.AddRange(items.Where(i => i.title.Contains(name, StringComparison.OrdinalIgnoreCase)));

        if(searchedItems is null)
        {
            Console.WriteLine("Sorry, there is no item with that name");
            return;
        }

        ShowItemsList.ShowItems(searchedItems);

        if(searchedItems.Count == 1)
        {
            LibraryItem item = searchedItems[0];

            cLoan.LoanCreationFromSingleItem(user, item);
        }
        else
        {
            cLoan.LoanCreationFromSelection(user);
        }
    }

    public void SearchByGenre(User user, List<LibraryItem> items, string genre)
    {
        searchedItems.Clear();
        searchedItems.AddRange(items.Where(i => i.genre.Contains(genre, StringComparison.OrdinalIgnoreCase)));

        if(searchedItems is null)
        {
            Console.WriteLine("Sorry, there is no item of that genre");
            return;
        }

        ShowItemsList.ShowItems(searchedItems);

        if(searchedItems.Count == 1)
        {
            LibraryItem item = searchedItems[0];

            cLoan.LoanCreationFromSingleItem(user, item);
        }
        else
        {
            cLoan.LoanCreationFromSelection(user);
        }
    }

    public void SearchById(User user, List<LibraryItem> items, string id)
    {
        if(!int.TryParse(id, out int intId))
        {
            throw new FormatException("Please enter a valid Id");
        }
        
        searchedItems.AddRange(items.Where(i => i.id == intId));
        
        if(searchedItems is null)
        {
            Console.WriteLine("There is no item with that Id");
            return;
        }

        ShowItemsList.ShowItems(searchedItems);
        LibraryItem item = searchedItems[0];

        cLoan.LoanCreationFromSingleItem(user, item);
    }

}