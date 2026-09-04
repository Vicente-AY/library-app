using Users;
using Items;
using Data;
using Loans;
using Reservations;

namespace Utils;

public class LoanWaitlistBuilder
{
    public static void LoanCreation(User user, IEnumerable<LibraryItem> items)
    {
        NotificacionGenerator notGen = new NotificacionGenerator();
        using (var db = new LibraryContext())
        {
            int maxId = db.Loans.Select(i => (int?)i.id).Max() ?? 0;
            db.Users.Attach(user);
            foreach (var item in items)
            {

                db.LibraryItems.Attach(item);

                maxId++;
                DateTime loanCreated = DateTime.Now;
                int days = (item is Book) ? 15 : 7;
                DateTime expectedReturn = loanCreated.AddDays(days);

                

                Loan loan = new Loan(maxId, item, loanCreated, expectedReturn, user);

                db.Loans.Add(loan);
                user.loanList.Add(loan);
                notGen.GenerateNotification(user, $"Successfuly loaned ID: {item.id} | {item.title}. Return Date: {expectedReturn.ToString("dd/MM/yyyy")}");
                item.availability = Availability.Lent;
            }
            db.SaveChanges();
        }
    }

    public WaitEntry WaitListCreation(User user, LibraryItem item)
    {
        DateTime waitListRequest = DateTime.Now;
        NotificacionGenerator notGen = new NotificacionGenerator();
        notGen.GenerateNotification(user, $"Successfully added item ID: {item.id} | {item.title} to waitlist");
        return new WaitEntry(user, waitListRequest);
    }
}