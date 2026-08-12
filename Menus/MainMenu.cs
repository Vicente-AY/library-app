namespace Menus;

public class MainMenu
{
    public void MainMenuOptions()
    {

        bool iterate = true;

        Console.WriteLine("Welcome to Library-App");

        while(iterate){
            Console.WriteLine("Please select an option");
            Console.WriteLine("1. Login | 2. Register | 3. Exit");

            string input = Console.ReadLine();

            //excepciones

            int option = int.Parse(input);

            switch (option)
            {
                case 1: 
                    //login
                    break;
                case 2:
                    //register
                    break;
                case 3:
                    Console.WriteLine("Bye!");
                    iterate = false;
                    break;
            }
        }

        return;
    }
}