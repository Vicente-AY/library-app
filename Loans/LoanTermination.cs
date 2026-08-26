using Data;
using Utils;
using Users;
using Items;
using Reservations;

namespace Loans;

public class LoanTermination
{
    public void ReturnItems(User user)
    {
        
        LibraryContext db = new LibraryContext();

        DateTime cancelationTime = DateTime.Now;

        LoanSelection selectLoans = new LoanSelection();
        Console.WriteLine("\nPlease, insert the ids for the items you want to return separated by a comma, or just the id if is just one item");
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

            CheckNextReservedUser cleanList = new CheckNextReservedUser();
            List<WaitEntry> cleanWaitList = cleanList.CheckNextUser(item)!;

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
            loan.finePaid = false;

            user.suspended = true;
            user.suspensionStart = DateTime.Now;
            user.suspensionUntil = DateTime.MaxValue;
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
}