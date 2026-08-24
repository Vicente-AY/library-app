using Reservations;
using Items;


namespace Utils;

public class CheckNextReservedUser
{
        public List<WaitEntry>? CheckNextUser(LibraryItem item)
    {

        List<WaitEntry> waitList = item.waitList; 

        NotificacionGenerator notGen = new NotificacionGenerator();

        if(waitList.Count == 0)
        {
            return waitList;
        }

        DateTime availablePeriodEnd = DateTime.Now.AddDays(2);

        if (waitList.All(u => u.user.suspended && u.user.suspensionUntil > availablePeriodEnd))
        {

            List<WaitEntry> cancelEntries = waitList.Where(w => w.user.suspended && w.user.suspensionUntil > availablePeriodEnd).ToList();

            foreach(var w in cancelEntries)
            {

                WaitEntry? userEntry = w.user.userWaitList.FirstOrDefault(e => e.item.id == item.id); 

                if(userEntry != null)
                {
                    w.user.userWaitList.Remove(userEntry);
                }

                notGen.GenerateNotification(w.user, $"Your reserve for the Item: ID: {w.item.id} | {w.item.title} has been cancell due to your extended supension period");
            }

            waitList.Clear();
            return waitList;
        }

        int waitListChecked = 0;
        int totalItems = waitList.Count();

        while(waitListChecked < totalItems)
        {
            WaitEntry nextWait = waitList[0];

            bool longSuspension = nextWait.user.suspended && nextWait.user.suspensionUntil > availablePeriodEnd;

            if (longSuspension)
            {
                waitList.RemoveAt(0);
                waitList.Add(nextWait);

                notGen.GenerateNotification(nextWait.user, $"Your reserve for the Item: ID: {nextWait.item.id} | {nextWait.item.title} has been modified due to your extended supension period");

                nextWait.notifiedAt = DateTime.Now;
                nextWait.notifiedAt = DateTime.Now.AddDays(2);            

                waitListChecked++;
            }
            else
            {
                break;
            }
        }

        return waitList;
    }
}