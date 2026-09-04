using Users;
using Data;
using Items;
using Utils;
using Reservations;

namespace Loans;

public class CreateLoan
{
    public void LoanCreationFromSelection(User user)
    {

        LibraryContext db = new LibraryContext();

        SelectItems itemSelection = new SelectItems();
        List<LibraryItem>? selectedItems = itemSelection.ItemSelection(user);
        LoanWaitlistBuilder waitlistLoanBuilder = new LoanWaitlistBuilder();

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
                //waitlistLoanBuilder.LoanCreation(user, sItem, db);
            }
            else
            {
                if (!sItem.waitList.Any(w => w.user == user))
                {
                    sItem.waitList.Add(waitlistLoanBuilder.WaitListCreation(user, sItem));
                }
            }
        }

        db.SaveChanges();
    }

    public void LoanCreationFromSingleItem(User user, LibraryItem item)
    {
        
        LibraryContext db = new LibraryContext();
        LoanWaitlistBuilder waitlistLoanBuilder = new LoanWaitlistBuilder();

        Console.WriteLine($"\nSelected Item: {item.id} | {item.title}");

        if(item.availability == Availability.Lent || item.availability == Availability.Maintenance)
        {
            if(!item.waitList.Any(w => w.user ==user)){
                Console.WriteLine("The selected Item is currently on a Loan or on Maintenance. Do you want to enter the wait list? (Type 0 or blank to decline)");
                string? waitlist = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(waitlist) || waitlist.Equals("0"))
                {
                    Console.WriteLine("Canceling wait list operation");
                    return;
                }

                WaitEntry newWEntry = waitlistLoanBuilder.WaitListCreation(user, item);
                item.waitList.Add(newWEntry);
                user.userWaitList.Add(newWEntry);
                db.SaveChanges();
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

            //waitlistLoanBuilder.LoanCreation(user, item, db);
        }

        db.SaveChanges();
    }


}