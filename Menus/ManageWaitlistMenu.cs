using Users;
using Utils;
using ProgramExceptions;
using Data;
using Loans;
using Microsoft.EntityFrameworkCore;
using Items;

namespace Menus;

public class ManageWaitlistMenu
{
    int minOption = 1;
    int maxOption = 4;

    public void ShowWaitlistMenu(User user)
    {

        if(user.userWaitList is null)
        {
            Console.WriteLine("\nSorry, you dont have any Item reservation. Returning to User's Menu");
            return;
        }

        Console.WriteLine("\nWelcome to Reservation Management Menu");   
        Console.WriteLine("----------------------------------------\n");

        bool iterate = true;
        string? input = "";
        int option = 0;

        while(iterate){
            try{
                Console.WriteLine("Please select an option");
                Console.WriteLine("1. Consult Reservations | 2. Cancel Reservations | 3. Pick up Reservation");
                Console.WriteLine("4. Exit");

                input = Console.ReadLine();

                option = InputValidation.CheckInput(input, minOption, maxOption);

                switch (option)
                {
                    case 1: 
                        ShowReservations(user);
                        break;
                    case 2:
                        CancelReservation(user);
                        break;
                    case 3:

                        break;
                    case 4:
                        Console.WriteLine("\nReturning to User Main Manu");
                        iterate = false;
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

    private void ShowReservations(User user)
    {
        List<WaitEntry> userWaitL = user.userWaitList;
        string? text = "";

        Console.WriteLine("Your Reserve List");

        foreach(var w in userWaitL)
        {
            LibraryItem item = w.item;
            int position = item.waitList.FindIndex(a => a.user == user) + 1;

            if(position == 0)
            {
                continue;
            }

            text = w.notifiedAt is null ? $" position in the waitlist: {position}": $" is ready for pick up";

            Console.WriteLine($"Item: {item.id} {item.title}" + text);
        }
    }

    private void CancelReservation(User user)
    {
        
    }
}