using Users;
using Utils;
using Items;
using Data;

namespace Reservations;

public class ReservationCancelation
{
    public void CancelReservation(User user)
    {

        LibraryContext db = new LibraryContext();

        NotificacionGenerator notGen = new NotificacionGenerator();

        WaitEntrySelection selectWaitList = new WaitEntrySelection();
        Console.WriteLine("\nPlease, insert the ids for the items you want to cancel the reservation separated by a comma, or just the id if is just one item");
        List<WaitEntry>? cancelWaitList = selectWaitList.SelectWaitEntry(user);

        if(cancelWaitList is null)
        {
            return;
        }

        foreach(var c in cancelWaitList)
        {
            LibraryItem item = c.item;
            user.userWaitList.Remove(c);
            item.waitList.Remove(c);

            notGen.GenerateNotification(user, $"Item {item.id} {item.title} removed from your Reservation List");
        }
        
        db.SaveChanges();
        Console.WriteLine("Items removed successfully");
    }
}