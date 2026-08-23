using Data;
using Utils;
using Users;
using Items;

namespace Loans;

public class LoanTermination
{
    public void ReturnItems(User user)
    {
        
        LibraryContext db = new LibraryContext();

        DateTime cancelationTime = DateTime.Now;

        LoanSelection selectLoans = new LoanSelection();
        Console.WriteLine("\nPlease, insert the ids for the items you want to return by a comma, or just the id if is just one item");
        List<Loan>? cancelLoans = selectLoans.SelectLoans(user);

        if(cancelLoans is null)
        {
            return;
        }

        foreach(var l in cancelLoans)
        {

            CheckItemStatus(l, l.item, user);
            CheckLoanTimeSpanExceed(l, user);

            LibraryItem item = l.item;

            List<WaitEntry> cleanWaitList = CheckNextUser(item.waitList)!;

            if(cleanWaitList.Count > 0 && item.availability != Availability.Maintenance)
            {
                WaitEntry next = cleanWaitList[0];    

                User nextUser = next.user;
                NotificacionGenerator notGen = new NotificacionGenerator();
                notGen.GenerateNotification(nextUser, $"Available to pick up: ID: {item.id} - {item.title}. The reserve lasts until {cancelationTime.AddDays(2).ToString("dd/MM/yyyy")}");

                next.notifiedAt = cancelationTime;
                next.expirationDate = cancelationTime.AddDays(2);
            }
            if(l.item.waitList.Count == 0 && l.item.availability != Availability.Maintenance)
            {
                l.item.availability = Availability.Available;
            }

            l.itemReturned = cancelationTime;

            TimeSpan loanDuration = cancelationTime - l.loanCreated;
            double duration = loanDuration.TotalDays;

            l.loanExtension = (int)Math.Round(duration);
            l.active = false;

            user.loanList.Remove(l);
        }

        db.SaveChanges();
    }

    private void CheckItemStatus(Loan loan, LibraryItem item, User user)
    {
        Random ran = new Random();

        int rInt = ran.Next(1, 20);
        if(rInt == 1)
        {
            item.availability = Availability.Maintenance;
            item.maintenanceEntry = DateTime.Now;
            item.mainteneanceExit = DateTime.Now.AddDays(ran.Next(3, 15));
            
            loan.brokenReturn = true;

            user.suspended = true;
            user.suspensionStart = DateTime.Now;
        }
    }

    private void CheckLoanTimeSpanExceed(Loan loan, User user)
    {
        DateTime now = DateTime.Now;
        if(now > loan.expectedReturn)
        {
            TimeSpan duration = now - loan.expectedReturn;
            double totalDays = duration.TotalDays;
            int days = (int) Math.Round(totalDays);
            loan.delayed = true;

            user.delayPoints += days;
        }
    }

    private List<WaitEntry>? CheckNextUser(List<WaitEntry> waitList)
    {

        NotificacionGenerator notGen = new NotificacionGenerator();

        if(waitList.Count == 0)
        {
            return waitList;
        }

        DateTime availablePeriodEnd = DateTime.Now.AddDays(2);

        if (waitList.All(u => u.user.suspended && u.user.suspensionUntil > availablePeriodEnd))
        {
            foreach(var w in waitList.Where(u => u.user.suspended && u.user.suspensionUntil > availablePeriodEnd).ToList())
            {
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