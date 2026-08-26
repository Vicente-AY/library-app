using Users;
using ProgramExceptions;
using Utils;
using Data;

namespace Menus;

public class ManageUsersMenu
{
    int minOption = 1;
    int maxOption = 1000;

    public void ShowManageUsersMenu()
    {

        LibraryContext db = new LibraryContext();

        Console.WriteLine($"\nWelcome to the Manage Users Menu");
        Console.WriteLine("------------------");

        bool iterate = true;
        string? input = "";
        int option = 0;

        while(iterate){
            try{
                Console.WriteLine("\nPlease select an option");
                Console.WriteLine("1. Show Users List | 2. Item List | 3. Exit");

                input = Console.ReadLine();

                option = InputValidation.CheckInput(input, minOption, maxOption);

                switch (option)
                {
                    case 1:
                        SelectUsers(db);
                        break;
                    case 2:

                        break;
                    case 3:
                        iterate = false;
                        break;
                    default:
                        Console.WriteLine("\nUnrecognice option. Please select a valid one");
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
                Console.WriteLine($"Unexpected error: {e.Message}");
            }
        }

        return;
    }

    private void SelectUsers(LibraryContext db)
    {

        bool iterate = true;

        while (iterate)
        {
            Console.WriteLine("\nType the type of users you want to see");
            Console.WriteLine("1. Show all Users | 2. Show only suspended Users | 3. Show only blocked users");
            Console.WriteLine("4. Back to Menu");

            string? input = Console.ReadLine();

            int option = InputValidation.CheckInput(input, 1, 4);

            List<User> selectedUsers = new List<User>();

            switch (option)
            {
                case 1:
                    selectedUsers = db.Users.ToList();
                    Console.WriteLine("\nShowing all Users");
                    ShowUserData.ShowUsers(selectedUsers);
                    break;
                case 2:
                    selectedUsers = db.Users.Where(u => u.suspended).ToList();
                    Console.WriteLine("\nShowing suspended Users");
                    ShowUserData.ShowUsers(selectedUsers);
                    break;
                case 3:
                    selectedUsers = db.Users.Where(u => u.blocked).ToList();
                    Console.WriteLine("\nShowing blocked Users");
                    ShowUserData.ShowUsers(selectedUsers);
                    break;
                case 4:
                    Console.WriteLine("\nBack to Menu");
                    iterate = false;
                    break;
                default:
                    Console.WriteLine("\nUnrecognice option. Please select a valid one");
                    break;
            }
        }
    }
}