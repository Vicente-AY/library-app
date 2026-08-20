using Users;
using Utils;
using Items;
using Data;
using Microsoft.EntityFrameworkCore;
using ProgramExceptions;

namespace Loans;

public class SelectItems
{
    public List<LibraryItem>? ItemSelection(User user)
    {
        int totalLoans = (user is Librarian) ? 10 : 5;
        int loansLeft = totalLoans - user.loanList.Count();

        LibraryContext db = new LibraryContext();

        List<LibraryItem> libraryItems = new List<LibraryItem>();
        
        List<string> inputString = new List<string>();
        List<int> itemsIds = new List<int>();

        List<LibraryItem> availableLibraryItems = new List<LibraryItem>();
        
        while (true)
        {
            try
            {    
                Console.WriteLine("\nPlease, introduce the Ids of the Items you want to make a Loan (Type 0 or blank to cancell the Loan operation)");
                Console.WriteLine("Please, use a comma between Ids");
                Console.WriteLine($"Remember you have {loansLeft} Loans available");
                string? input = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(input) || input.Equals("0"))
                {
                    Console.WriteLine("Cancelling Loan operation");
                    return null;
                }

                inputString = input.Split(',').Select(s => s.Trim()).ToList();
                itemsIds = StringToIntConvertor.ConvertStringToInt(inputString);

                libraryItems = db.LibraryItems.Include(i => i.waitList).ThenInclude(w => w.user).Where(i => itemsIds.Contains(i.id)).ToList();

                availableLibraryItems = libraryItems.Where(i => i.availability == Availability.Available).ToList();
                if(availableLibraryItems.Count > loansLeft)
                {
                    throw new ExceedingMaxLoanException("You have tried to exceed the maximum loan. Please try again");
                }

                return libraryItems;
            }
            catch(ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(ExceedingMaxLoanException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
            }
        }
    }
}