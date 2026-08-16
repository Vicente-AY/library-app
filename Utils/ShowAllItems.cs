using Users;
using Data;
using Items;
using Loans;

namespace Utils;

public class ShowAllItems
{
    public void ShowEveryItem(User user)
    {
        LibraryContext db = new LibraryContext();

        List<LibraryItem> items = db.LibraryItems.ToList();

        foreach(var i in items)
        {
            Console.WriteLine($"Id: {i.id} | {i.title} | {i.media} | {i.availability}");
        }

        CreateLoan newLoanList = new CreateLoan();

        newLoanList.NewLoan(user);
    }
}