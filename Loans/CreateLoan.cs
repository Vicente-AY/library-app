using Users;
using Data;
using Items;
using Utils;

namespace Loans;

public class CreateLoan
{
    public void LoanCreationFromSelection(User user)
    {
        SelectItems itemSelection = new SelectItems();
        List<LibraryItem>? selectedItems = new List<LibraryItem>();
        selectedItems = itemSelection.ItemSelection(user);

        


    }

    public void LoanCreationFromSingleItem(User user, LibraryItem item)
    {

        if(item.availability == Availability.Lent)
        {
            Console.WriteLine("The selected Item is currently on a Loan. Do you want to enter the wait list? (Type 0 or blank to decline)");
            string? waitlist = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(waitlist) || waitlist.Equals("0"))
            {
                Console.WriteLine("Canceling Loan operation");
                return;
            }

            item.waitList.Add(user);
        }
        else
        {
            if(user is User && user.loanList.Count > 4)
            {
                Console.WriteLine("You have the maximum number of Loans posible for your account");
                return;
            }
            if(user is Librarian && user.loanList.Count > 9)
            {
                Console.WriteLine("You have the maximum number of Loans posible for your account");
                return;
            }

            Console.WriteLine("Do you want to Loan the selected Item? (Type 0 or blank to decline)");
            string? acceptLoan = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(acceptLoan) || acceptLoan.Equals("0"))
            {
                Console.WriteLine("Canceling Loan operation");
                return;
            }

            Loan loan = LoanCreation(user, item);

            user.loanList.Add(loan);
            item.availability = Availability.Lent;
        }
    }

    private Loan LoanCreation(User user, LibraryItem item)
    {

        int id = 0;
        DateTime loanCreated = DateTime.Now;
        DateTime expectedReturn = new DateTime();
        int days = 15;

        
        LibraryContext db = new LibraryContext();

        var loans = db.Loans.ToList();

        foreach(var l in loans)
        {
            if (l.id > id)
            {
                id = l.id;
            }
        }

        id++;

        if(!(item is Book))
        {
            days = 7;
        }

        expectedReturn.AddDays(days);

        return new Loan(id, item, loanCreated, expectedReturn, user);
    }
}