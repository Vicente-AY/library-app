using Users;
using Items;

namespace Loans;

public class WaitEntry
{
    public int id {get; set;} = 0;
    public User user {get; set;} = null!;
    public DateTime? requestDate {get; set;} = null;
    public DateTime? notifiedAt {get; set;} = null;
    public DateTime? expirationDate {get; set;} = null;
    public LibraryItem item {get; set;} = null!;

    private WaitEntry(){}// solo para EF Core
    public WaitEntry(User user, DateTime requestDate)
    {
        this.user = user;
        this.requestDate = requestDate;
    }
}