using Users;
using Utils;
using ProgramExceptions;

namespace Menus;

public class LibrarianMenu
{
    int minOption = 0;
    int maxOption = 1000;
    public void OpenLibrarianMenu(Librarian librarian)
    {

        Console.WriteLine($"\nWelcome to the Management Menu");
        Console.WriteLine("------------------");

        bool iterate = true;
        string? input = "";
        int option = 0;

        while(iterate){
            try{
                Console.WriteLine("\nPlease select an option");
                Console.WriteLine("1. Login | 2. Register | 3. Exit");

                input = Console.ReadLine();

                option = InputValidation.CheckInput(input, minOption, maxOption);

                switch (option)
                {
                    case 1: 

                        break;
                    case 2:

                        break;
                    case 3:
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
