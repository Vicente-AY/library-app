using Items;
using Utils;
using ProgramExceptions;

namespace Menus;

public class MediaSelectionMenu
{
    int minOption = 1;
    int maxOption = 4;
    public Type? SelectMedia()
    {
        bool iterate = true;
        string? input = "";
        int option = 0;

        while(iterate){
            try{
                Console.WriteLine("\nType the option of the media you want to select (Type 0 or blank to cancell)");
                Console.WriteLine("1. Book | 2. Film | 3. Music Album | 4. Videogame");

                input = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(input) || input.Equals("0"))
                {
                    Console.WriteLine("Cancelling the operation");
                    return null;
                }

                option = InputValidation.CheckInput(input, minOption, maxOption);

                return option switch
                {
                    1 => typeof(Book),
                    2 => typeof(Film),
                    3 => typeof(MusicAlbum),
                    4 => typeof(Videogame),
                    _ => null
                };
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
                Console.WriteLine(e.Message);
            }
        }

        return null;
    }
}