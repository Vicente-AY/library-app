using Users;
using Data;
using Items;
using Loans;

namespace Utils;

public class ShowItemsList
{
    public static void ShowItems(List<LibraryItem> items)
    {
        foreach(var i in items)
        {
            Console.WriteLine($"Id: {i.id} | {i.title} | {i.media} | {i.availability}");
        }
    }
}