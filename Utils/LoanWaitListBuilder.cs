using Users;
using Items;
using Data;
using Loans;
using Reservations;

namespace Utils;

public class LoanWaitlistBuilder
{
    public void LoanCreation(User user, LibraryItem item, LibraryContext db)
    {

        int id = 0;
        DateTime loanCreated = DateTime.Now;
        int days = (item is Book) ? 15 : 7;
        DateTime expectedReturn = loanCreated.AddDays(days);

        id = (db.Loans.Select(i => (int?)i.id).Max() ?? 0) + 1 ;

        Loan loan = new Loan(id, item, loanCreated, expectedReturn, user);

        db.Loans.Add(loan);
        user.loanList.Add(loan);
        NotificacionGenerator notGen = new NotificacionGenerator();
        notGen.GenerateNotification(user, $"Successfuly loaned ID: {item.id} | {item.title}. Return Date: {expectedReturn.ToString("dd/MM/yyyy")}");
        item.availability = Availability.Lent;
    }

    public WaitEntry WaitListCreation(User user, LibraryItem item)
    {
        DateTime waitListRequest = DateTime.Now;
        NotificacionGenerator notGen = new NotificacionGenerator();
        notGen.GenerateNotification(user, $"Successfully added item ID: {item.id} | {item.title} to waitlist");
        return new WaitEntry(user, waitListRequest);
    }
}