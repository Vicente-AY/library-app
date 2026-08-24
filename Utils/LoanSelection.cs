using Loans;
using Users;

namespace Utils;

public class LoanSelection
{
    public List<Loan>? SelectLoans(User user)
    {
        
        List<int>? itemsIds = IdItemSelection.ItemIdSelection();

        if(itemsIds is null)
        {
            return null;
        }

        var loans = user.loanList.Where(i => i.active && itemsIds.Contains(i.item.id)).ToList();

        if(loans.Count == 0)
        {
            Console.WriteLine("No active loans matched the provided Id/s");
            return null;
        }

        return loans;
    }
}