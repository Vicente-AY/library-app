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

            CheckItemStatus(l.item, user);
            CheckLoanTimeSpanExceed(l, user);

            var item = l.item;
            if(l.item.waitList.Count > 0 && l.item.availability != Availability.Maintenance)
            {
                item.waitList[0].user.notifications.Add($"{cancelationTime.ToString("dd/MM/yyyy : hh:mm")} - Available to pick up: {item.id} {item.title}");
                item.waitList[0].notifiedAt = cancelationTime;
                item.waitList[0].expirationDate = cancelationTime.AddDays(2);
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

    private void CheckItemStatus(LibraryItem item, User user)
    {
        Random ran = new Random();

        int rInt = ran.Next(1, 20);
        if(rInt == 1)
        {
            item.availability = Availability.Maintenance;
            item.maintenanceEntry = DateTime.Now;
            item.mainteneanceExit = DateTime.Now.AddDays(ran.Next(3, 15));

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
            loan.penalized = true;
            loan.delayed = true;

            user.delayPoints += days;
        }
    }
}