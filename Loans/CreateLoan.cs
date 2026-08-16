using Users;
using Data;
using Items;
using Utils;

namespace Loans;

public class CreateLoan
{
    public void LoanCreation(User user)
    {
        SelectItems itemSelection = new SelectItems();
        List<LibraryItem>? selectedItems = new List<LibraryItem>();
        selectedItems = itemSelection.ItemSelection(user);

        


    }
}