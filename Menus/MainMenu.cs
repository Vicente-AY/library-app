using ProgramExceptions;

namespace Menus;

public class MainMenu
{
    int minOption = 1;
    int maxOption = 3;
    public void MainMenuOptions()
    {

        bool iterate = true;

        Console.WriteLine("Welcome to Library-App");

        while(iterate){
            try{
                Console.WriteLine("\n" + "Please select an option");
                Console.WriteLine("1. Login | 2. Register | 3. Exit");

                string? input = Console.ReadLine();

                int option = CheckInput(input);

                switch (option)
                {
                    case 1: 
                        //login
                        Console.WriteLine("Aqui va el login");
                        break;
                    case 2:
                        //register
                        Console.WriteLine("Aqui va el Registro");
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

    public int CheckInput(string? input)
    {
        int option = 0;

        if(string.IsNullOrWhiteSpace(input))
        {
            throw new EmptyException("Please enter a number");
        }
        if (!int.TryParse(input, out option))
        {
            throw new FormatException("Please enter a number");
        }
        if(option < minOption || option > maxOption)
        {
            throw new NumberOutOfRangeException("Plese enter a valid option");
        }

        return option;
    }
}