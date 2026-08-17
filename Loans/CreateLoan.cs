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
        Console.WriteLine($"Do you want to Loan {item.title}? (Y/N)");
    }
}