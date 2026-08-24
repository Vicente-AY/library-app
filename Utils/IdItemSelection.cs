namespace Utils;

public class IdItemSelection
{
    public static List<int>? ItemIdSelection()
    {
        Console.WriteLine("Type 0 or blank to cancell the operation");
        string? input = Console.ReadLine();

        if(string.IsNullOrWhiteSpace(input) || input.Equals("0"))
        {
            Console.WriteLine("\nCancelling operation");
            return null;
        }

        List<string> inputString = input.Split(',').Select(s => s.Trim()).ToList();
        return StringToIntConvertor.ConvertStringToInt(inputString).Distinct().ToList();
    }
}