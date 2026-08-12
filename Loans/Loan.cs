using Items;
using Users;

namespace Loans;

public class Loan
{
    public int id {get; set;} = 0;
    public LibraryItem? item {get; set;} = null;
    public DateTime? loanCreated {get; set;} = null;
    public int loanExtension {get; set;} = 0; //loan time in days
    public User? user {get; set;} = null;
}