using Users;
using Utils;

namespace Reservations;

public class ReservationCancelation
{
    public void CancelReservation(User user)
    {
        WaitEntrySelection selectWaitList = new WaitEntrySelection();
        Console.WriteLine("\nPlease, insert the ids for the items you want to cancel the reservation separated by a comma, or just the id if is just one item");
        List<WaitEntry>? cancelWaitList = selectWaitList.SelectWaitEntry(user);


    }
}