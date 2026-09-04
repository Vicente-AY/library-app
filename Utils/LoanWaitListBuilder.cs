using Users;
using Items;
using Data;
using Loans;
using Reservations;

namespace Utils;

public class LoanWaitlistBuilder
{
    public void LoanCreation(User user, List<LibraryItem> items)
    {
        NotificacionGenerator notGen = new NotificacionGenerator();
        using (var db = new LibraryContext())
        {
            foreach (var item in items)
            {
                int id = 0;
                DateTime loanCreated = DateTime.Now;
                int days = (item is Book) ? 15 : 7;
                DateTime expectedReturn = loanCreated.AddDays(days);

                id = (db.Loans.Select(i => (int?)i.id).Max() ?? 0) + 1;

                Loan loan = new Loan(id, item, loanCreated, expectedReturn, user);

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