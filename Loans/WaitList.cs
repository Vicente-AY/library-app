using Users;
using Items;

namespace Loans;

public class WaitList
{
    public int id {get; set;} = 0;
    public User user {get; set;} = null!;
    public LibraryItem item {get; set;} = null!;
    public DateTime? requestDate {get; set;} = null;
    public DateTime? notifiedAt {get; set;} = null;
    public DateTime? expirationDate {get; set;} = null;

    private WaitList(){}// solo para EF Core
    public WaitList(User user, LibraryItem item, DateTime requestDate)
    {
        this.user = user;
        this.item = item;
        this.requestDate = requestDate;
    }
}