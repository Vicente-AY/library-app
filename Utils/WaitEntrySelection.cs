using Loans;
using Users;

namespace Utils;

public class WaitEntrySelection
{
    public List<WaitEntry>? SelectWaitEntry(User user)
    {
        
        List<int>? itemsIds = IdItemSelection.ItemIdSelection();

        if(itemsIds is null)
        {
            return null;
        }

        List<WaitEntry> waitList = user.userWaitList.Where(i => itemsIds.Contains(i.item.id)).ToList();

        if(waitList.Count == 0)
        {
            Console.WriteLine("\nNo active reservations matched the provided Id/s");
            return null;
        }

        return waitList;
    }
}