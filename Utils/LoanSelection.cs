using Loans;
using Users;

namespace Utils;

public class LoanSelection
{
    public List<Loan>? SelectLoans(User user)
    {
        
        Console.WriteLine("Type 0 or blank to cancell the operation");
        string? input = Console.ReadLine();

        if(string.IsNullOrWhiteSpace(input) || input.Equals("0"))
        {
            Console.WriteLine("\nCancelling operation");
            return null;
        }

        List<string> inputString = input.Split(',').Select(s => s.Trim()).ToList();
        List<int> itemsIds = StringToIntConvertor.ConvertStringToInt(inputString).Distinct().ToList();

        var loans = user.loanList.Where(i => i.active && itemsIds.Contains(i.item.id)).ToList();

        if(loans.Count == 0)
        {
            Console.WriteLine("No active loans matched the provided Id/s");
            return null;
        }

        return loans;
    }
}