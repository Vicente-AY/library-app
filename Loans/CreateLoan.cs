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
        List<LibraryItem>? selectedItems = itemSelection.ItemSelection(user);

        if(selectedItems is null)
        {
            return;
        }

        if(selectedItems.Exists(i => i.availability == Availability.Lent || i.availability == Availability.Maintenance))
        {
            Console.WriteLine("There are Items on your selection that are on Loan or on Maintenance. Do you want to join their waitlist? (Type 0 or blank to decline)");
            string? input = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(input) || input.Equals("0"))
            {
                selectedItems = selectedItems.Where(i => i.availability == Availability.Available).ToList();
            }
        }

        foreach(var sItem in selectedItems)
        {
            if(sItem.availability == Availability.Available){
                LoanCreation(user, sItem);
            }
            else
            {
                if (!sItem.waitList.Contains(user))
                {
                    sItem.waitList.Add(user);
                }
            }
        }
    }

    public void LoanCreationFromSingleItem(User user, LibraryItem item)
    {

        Console.WriteLine($"\nSelected Item: {item.id} | {item.title}");

        if(item.availability == Availability.Lent || item.availability == Availability.Maintenance)
        {
            if(!item.waitList.Contains(user)){
                Console.WriteLine("The selected Item is currently on a Loan or on Maintenance. Do you want to enter the wait list? (Type 0 or blank to decline)");
                string? waitlist = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(waitlist) || waitlist.Equals("0"))
                {
                    Console.WriteLine("Canceling wait list operation");
                    return;
                }

                item.waitList.Add(user);
            }
            else
            {
                Console.WriteLine("Your already on the wait list for the selected Item");
                return;
            }
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
        int days = (item is Book) ? 15 : 7;
        DateTime expectedReturn = loanCreated.AddDays(days);
        
        LibraryContext db = new LibraryContext();

        id = db.Loans.Max(i => i.id) + 1 ;

        expectedReturn.AddDays(days);

        return new Loan(id, item, loanCreated, expectedReturn, user);
    }
}