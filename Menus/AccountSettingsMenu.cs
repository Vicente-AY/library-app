using Users;
using ProgramExceptions;
using Utils;
using System.IO;

namespace Menus;

public class AccountSettingsMenu
{
    int minOption = 0;
    int maxOption = 1000;

    public void SettingMenu(User user)
    {
        Console.WriteLine("Welcome to Account Settings Menu");
        Console.WriteLine("--------------------------------");

        bool cont = true;

        string? input = "";
        int option = 0;

        while(cont){
            try{
                Console.WriteLine("\nPlease select an option");
                Console.WriteLine("1. Check account info");

                input = Console.ReadLine();

                option = InputValidation.CheckInput(input, minOption, maxOption);

                switch (option)
                {
                    case 1: 
                        user.CheckData();
                        break;
                    case 2:
                        ;
                        break;
                    case 3:
                        Console.WriteLine("Bye!");
                        cont = false;
                        break;
                    default:
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
}