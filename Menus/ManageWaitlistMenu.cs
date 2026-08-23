using Users;
using Utils;
using ProgramExceptions;
using Data;
using Loans;
using Microsoft.EntityFrameworkCore;

namespace Menus;

public class ManageWaitlistMenu
{
    int minOption = 1;
    int maxOption = 1000;

    public void ShowWaitlistMenu(User user)
    {

        LibraryContext db = new LibraryContext();
        List<WaitEntry> userWaitList = db.WaitLists.Include(i => i.item).Include(l => l.user).Where(w => w.user == user).ToList();

        if(userWaitList is null)
        {
            Console.WriteLine("\nSorry, you dont have any Item reservation. Returning to User's Menu");
            return;
        }

        Console.WriteLine("\nWelcome to Waitlist Management Menu");   
        Console.WriteLine("---------------------------------\n");

        bool iterate = true;
        string? input = "";
        int option = 0;

        while(iterate){
            try{
                Console.WriteLine("\nPlease select an option");
                Console.WriteLine("1. Consult Reservations | 2. Cancel Reservations | 3. Pick up Reservation");
                Console.WriteLine("4. Exit");

                input = Console.ReadLine();

                option = InputValidation.CheckInput(input, minOption, maxOption);

                switch (option)
                {
                    case 1: 

                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:
                        Console.WriteLine("\nReturning to User Main Manu");
                        return;
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
}