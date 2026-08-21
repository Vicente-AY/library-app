using Items;
using Users;

namespace Loans;

public class Loan
{
    public int id {get; set;} = 0;
    public LibraryItem item {get; set;} = null!;
    public DateTime loanCreated {get; set;} = new DateTime(2000, 01, 01);
    public DateTime expectedReturn {get; set;} = new DateTime(2000, 01, 01);
    public DateTime itemReturned {get; set;} = new DateTime(2000, 01, 01);
    public int loanExtension {get; set;} = 0; //loan time in days
    public bool extended {get; set;} = false;
    public User? user {get; set;} = null;
    public bool penalized {get; set;} = false;
    public bool active {get; set;} = true;
    public bool delayed {get; set;} = false;

    private Loan(){}// para EF Core

    public Loan(int id, LibraryItem item, DateTime loanCreated, DateTime expectedReturn, User user)
    {
        this.id = id;
        this.item = item;
        this.loanCreated = loanCreated;
        this.expectedReturn = expectedReturn;
        this.user = user;
    }

}