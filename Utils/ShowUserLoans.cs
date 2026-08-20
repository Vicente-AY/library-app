using Users;

namespace Utils;

public class ShowUserLoans
{
    public void ShowLoans(User user)
    {

        DateTime now = DateTime.Now;

        Console.WriteLine("Your Loans");
        Console.WriteLine("----------\n");

        foreach(var l in user.loanList)
        {

            TimeSpan daysLeft = l.expectedReturn - now;
            int days = daysLeft.Days;

            Console.WriteLine($"Item: {l.id} {l.item.title} Media: {l.item.media}" + 
                $" Expected return: {l.expectedReturn!.ToString("dd/MM/yyyy")} Days left: {days}");
        }
    }
}