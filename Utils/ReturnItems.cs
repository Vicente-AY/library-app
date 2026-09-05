using Data;
using Items;
using Loans;
using Reservations;
using System;
using System.Collections.Generic;
using System.Text;
using Users;
using Utils;

namespace library_app.Utils
{
    public class ReturnItems
    {
        public void ReturnLoan(Loan l, User user)
        {

            using (var db = new LibraryContext())
            {

                db.Loans.Attach(l);
                db.Users.Attach(user);

                //Metodos en standby hasta seguir con la GUI
                //CheckItemStatus(l, l.item, user);
                //CheckLoanTimeSpanExceed(l, user);

                LibraryItem item = l.item;

                CheckNextReservedUser cleanList = new CheckNextReservedUser();
                List<WaitEntry> cleanWaitList = cleanList.CheckNextUser(item)!;

                if (cleanWaitList.Count > 0 && item.availability != Availability.Maintenance)
                {
                    WaitEntry next = cleanWaitList[0];

                    User nextUser = next.user;
                    NotificacionGenerator notGen = new NotificacionGenerator();
                    notGen.GenerateNotification(nextUser, $"Available to pick up: ID: {item.id} - {item.title}. The reserve lasts until {DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")}");

                    next.notifiedAt = DateTime.Now;
                    next.expirationDate = DateTime.Now.AddDays(2);
                }
                if (l.item.waitList.Count == 0 && l.item.availability != Availability.Maintenance)
                {
                    l.item.availability = Availability.Available;
                }

                l.itemReturned = DateTime.Now;

                TimeSpan loanDuration = l.itemReturned - l.loanCreated;
                double duration = loanDuration.TotalDays;

                l.loanExtension = (int)Math.Round(duration);
                l.active = false;

                user.loanList.Remove(l);

                db.SaveChanges();
            }
        }

        private void CheckItemStatus(Loan loan, LibraryItem item, User user)
        {
            Random ran = new Random();

            int rInt = ran.Next(1, 20);
            if (rInt == 1)
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

            if (DateTime.Now > loan.expectedReturn)
            {
                TimeSpan duration = DateTime.Now - loan.expectedReturn;
                double totalDays = duration.TotalDays;
                int days = (int)Math.Round(totalDays);
                loan.delayed = true;

                user.delayPoints += days;
            }
        }
    }
}
