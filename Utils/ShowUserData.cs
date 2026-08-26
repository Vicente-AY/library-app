using Users;
using Data;

namespace Utils;

public class ShowUserData
{
    public static void ShowUsers(List<User> selectedUsers)
    {

        List<User> users = selectedUsers.Where(u => !(u is Librarian) && u.name != null).ToList();
        List<Librarian> librarians = selectedUsers.OfType<Librarian>().ToList();

        foreach(var u in users)
        {
            Console.WriteLine($"User ID: {u.id} Name: {u.name} {u.surnames} Is Suspended: {u.suspended}");
        }

        Console.WriteLine("\nList of Librarians: \n");

        foreach(var l in librarians)
        {
            Console.WriteLine($"User ID: {l.id} Name: {l.name} {l.surnames} Is Suspended: {l.suspended}");
        }
    }
}