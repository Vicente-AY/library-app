using Data;
using Users;
using Utils;
using Items;

namespace Loans;

public class LoanExtension
{
    public void ExtendLoan(User user)
    {
        LibraryContext db = new LibraryContext();

        if (user.suspended)
        {
            Console.WriteLine("\nSorry, you are unelegible for any extension until your suspension expires");
            return;
        }

        LoanSelection loanSelection = new LoanSelection();
        Console.WriteLine("\nPlease, insert the ids for the items you want to ask for an extension, separated by a comma, or just the id if is just one item");
        List<Loan>? loanExtension = loanSelection.SelectLoans(user);

        if(loanExtension is null)
        {
            return;
        }

        foreach(var l in loanExtension)
        {
            if (l.extended)
            {
                Console.WriteLine($"\nSorry, the loan for {l.item.title} has already been extended");
                continue;
            }
            if(DateTime.Now > l.expectedReturn)
            {
                Console.WriteLine($"\nThe loan for {l.item.title} has pass his return date and is no elegible for an extension. Please return the item");
                continue;
            }
            if(l.item.waitList.Count > 0)
            {
                Console.WriteLine($"\nSorry, the loan for {l.item.title} cannot be extended because has other users waiting for it.");
                continue;
            }

            DateTime now = DateTime.Now;
            TimeSpan duration = now - l.expectedReturn;
            double totalDays = duration.TotalDays;
            
            if(totalDays <= 0 || totalDays >= 2)
            {
                Console.WriteLine("\nSorry, the loan can only be extended within the last 2 days");
                continue;
            }

            int extension = l.item is Book ? 15 : 7;

            l.extended = true;
            l.expectedReturn = l.expectedReturn.AddDays(extension);

            user.notifications.Add($"Item {l.item.title} loan extended successfully. New return date: {l.expectedReturn.ToString("dd/MM/yyyy")}");
            Console.WriteLine($"\nSuccessfull loan extension for {l.item.title}!");
        }
    }
}