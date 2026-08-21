using Users;
using ProgramExceptions;
using Utils;
using Loans;
using Data;
using Items;

namespace Menus;

public class ManageLoansMenu
{
    int minOption = 1;
    int maxOption = 1000;
    public void OpenManageLoansMenu(User user)
    {
        Console.WriteLine("\nWelcome to he Loan Management Menu");   
        Console.WriteLine("------------------\n");

        if(user.loanList is null)
        {
            Console.WriteLine("You dont have any current Loans. Closing Loan Management Menu");
            return;
        }

        bool iterate = true;
        string? input = "";
        int option = 0;

        while(iterate){
            try{
                Console.WriteLine("Please select an option (Type 0 or blank to close the menu)");
                Console.WriteLine("1. Consult Loans | 2. Return Item/s");

                input = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(input) || input.Equals("0"))
                {
                    Console.WriteLine("\nReturning to User Main Manu");
                    return;
                }

                option = InputValidation.CheckInput(input, minOption, maxOption);

                switch (option)
                {
                    case 1: 
                        ShowUserLoans userLoans = new ShowUserLoans();
                        userLoans.ShowLoans(user);
                        break;
                    case 2:
                        ReturnItems(user);
                        break;
                    case 3:
                        
                        break;
                    default:
                        Console.WriteLine("\nUnrecogniced Option. Plese select a valid one");
                        break;
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(EmptyException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(NumberOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine($"\nUnexpected error: {e.Message}");
            }
        }
        return;
    }

    public void ReturnItems(User user)
    {
        
        LibraryContext db = new LibraryContext();

        DateTime cancelationTime = DateTime.Now;

        Console.WriteLine("\nPlease, insert the ids for the items you want to return separated by a comma, r just the id if is just one item");
        Console.WriteLine("Type 0 or blank to cancell the operation");
        string? input = Console.ReadLine();

        if(string.IsNullOrWhiteSpace(input) || input.Equals("0"))
        {
            Console.WriteLine("\nCancelling return operation");
            return;
        }

        List<string> inputString = input.Split(',').Select(s => s.Trim()).ToList();
        List<int> itemsIds = StringToIntConvertor.ConvertStringToInt(inputString).Distinct().ToList();

        var cancelLoans = user.loanList.Where(i => i.active && itemsIds.Contains(i.item.id)).ToList();

        if(cancelLoans.Count == 0)
        {
            Console.WriteLine("No active loans matched the provided Id/s");
            return;
        }

        foreach(var l in cancelLoans)
        {

            CheckItemStatus(l.item, user);

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
            float duration = loanDuration.Days;

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
            user.suspended = true;
        }
    }
}