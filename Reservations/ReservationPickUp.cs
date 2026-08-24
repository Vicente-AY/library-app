using Users;
using Utils;
using Items;
using Data;

namespace Reservations;

public class ReservationPickUp
{
    public void PickUpReservation(User user)
    {

        LibraryContext db = new LibraryContext();

        List<WaitEntry> userReservationReady = user.userWaitList.Where(w => w.notifiedAt != null).ToList();

        if(userReservationReady is null)
        {
            Console.WriteLine("You dont have any Reservation ready to Pick Up");
            return;
        }

        WaitEntrySelection wEntryS = new WaitEntrySelection();
        Console.WriteLine("Select the items you want to pick up (Rembember that if you dont pick up a ready iten it will be discarded)");
        List<WaitEntry>? userPickUpSelection = wEntryS.SelectWaitEntry(user);

        if(userPickUpSelection is null)
        {
            return;
        }

        if(userPickUpSelection.All(p => userReservationReady.Contains(p)))
        {
            PickUpItems(user, userPickUpSelection, db);
        }
        else
        {
            GetSelection(user, userPickUpSelection, userReservationReady, db);
        }

        db.SaveChanges();
    }

    private void PickUpItems(User user, List<WaitEntry> selection, LibraryContext db)
    {

        LoanWaitlistBuilder loanBuilder = new LoanWaitlistBuilder();

        foreach(var s in selection)
        {
            LibraryItem item = s.item;

            user.userWaitList.Remove(s);
            item.waitList.Remove(s);
            s.pickUpDate = DateTime.Now;
            s.active = false;

            loanBuilder.LoanCreation(user, item, db);
        }
    }

    private void GetSelection(User user, List<WaitEntry> selection, List<WaitEntry> userReservationReady, LibraryContext db)
    {
        
        CheckNextReservedUser orderWaitList = new CheckNextReservedUser();
        List<WaitEntry> nonSelected = userReservationReady.Except(selection).ToList();

        PickUpItems(user, selection, db);

        foreach(var w in nonSelected)
        {
            LibraryItem item = w.item;
            user.userWaitList.Remove(w);
            item.waitList.Remove(w);

            orderWaitList.CheckNextUser(item);
        }
    }
}