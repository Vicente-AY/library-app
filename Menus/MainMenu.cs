using ProgramExceptions;
using Access;
using Utils;

namespace Menus;

public class MainMenu
{
    int minOption = 1;
    int maxOption = 3;
    public void MainMenuOptions()
    {

        bool iterate = true;

        string? input = "";
        int option = 0;

        Console.WriteLine("\nWelcome to Library-App");
        Console.WriteLine("------------------------");

        while(iterate){
            try{
                Console.WriteLine("\nPlease select an option");
                Console.WriteLine("1. Login | 2. Register | 3. Exit");

                input = Console.ReadLine();

                option = InputValidation.CheckInput(input, minOption, maxOption);

                switch (option)
                {
                    case 1: 
                        Login login = new Login();
                        login.LogUser();
                        break;
                    case 2:
                        Register register = new Register();
                        register.RegisterNewUser();
                        break;
                    case 3:
                        Console.WriteLine("Bye!");
                        iterate = false;
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
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        return;
    }
}