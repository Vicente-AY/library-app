using Users;
using Utils;
using Items;
using Data;

namespace Loans;

public class SelectItems
{
    public List<LibraryItem>? ItemSelection(User user)
    {
        LibraryContext db = new LibraryContext();

        List<LibraryItem> libraryItems = new List<LibraryItem>();
        LibraryItem? item = null;
        
        List<string> inputString = new List<string>();
        List<int> itemsIds = new List<int>();
        
        while (true)
        {
            try
            {    
                Console.WriteLine("\nPlease, introduce the Ids of the Items you want to make a Loan (Type 0 or blank to cancell the Loan operation)");
                Console.WriteLine("Please, use a comma between Ids");
                string? input = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(input) || input.Equals("0"))
                {
                    Console.WriteLine("Cancelling Loan operation");
                    return null;
                }

                inputString = input.Split(',').Select(s => s.Trim()).ToList();
                itemsIds = StringToIntConvertor.ConvertStringToInt(inputString);

                foreach(var id in itemsIds)
                {
                    item = db.LibraryItems.FirstOrDefault(u => u.id == id);
                    if(item is null)
                    {
                        continue;
                    }
                    libraryItems.Add(item);
                }

                return libraryItems;
            }
        }


            
    }
}