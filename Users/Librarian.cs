namespace Users;

public class Librarian : User
{

    public Librarian(User user) : base(user.login, user.password)
    {

        if(user.name == "" && user.surnames == "" && user.address == "" && user.bDate == null)
        {
            Console.WriteLine("Please fill the form with the user data");
            return;
        }
        if(user.blocked == true || user.suspended == true)
        {
            Console.WriteLine("The user is blocked or still in a suspension period. Cannot be promoted");
            return;
        }

        this.id = user.id;
        this.login = user.login;
        this.name = user.name;
        this.surnames = user.surnames;
        this.password = user.password;
        this.address = user.address;
        this.suspended = user.suspended;
        this.blocked = user.blocked;
        this.delayPoints = user.delayPoints;

        //borrar el user
        //añadir el nuevo librarian
    }

    private Librarian(){} //solo para EF Core

    public void UnSuspendUser(User user)
    {
        if(user.name == "" && user.surnames == "" && user.address == "")
        {
            Console.WriteLine("Please fill the form with the user data");
            return;
        }
        if(user.blocked == true)
        {
            Console.WriteLine("The user is blocked");
            return;
        }
        if(user.suspended == false)
        {
            Console.WriteLine("The user is not suspended");
            return;
        }

        Console.WriteLine("User " + user.name + " can now take loans");
        user.suspended = false;
    }

    public void PromoteUser(User user)
    {
        Librarian librarian = new Librarian(user);
    }

    public void DemoteUser(Librarian librarian)
    {
        User user = new User(librarian);
    }

    public void UnBlockUser(User user)
    {
        user.blocked = false;
    }
}