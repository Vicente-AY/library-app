using ProgramExceptions;

namespace Utils;

public class InputValidation
{
    public static int CheckInput(string? input, int minOption, int maxOption)
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