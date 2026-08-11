namespace Users;

public class Librarian : User
{

    public Librarian(User user) : base(user.id, user.login, user.password)
    {

        if(!(user.name == "" && user.surnames == "" && user.address == ""))
        {
            Console.WriteLine("Please fill the form with the user data");
            return;
        }
        if(!(user.blocked == false && user.delay == false))
        {
            Console.WriteLine("The user is blocked or still in a delay suspension period. Cannot be promoted");
            return;
        }

        this.id = user.id;
        this.login = user.login;
        this.name = user.name;
        this.surnames = user.surnames;
        this.password = user.password;
        this.address = user.address;
        this.suspended = user.suspended;
        this.blcoked = user.blocked;
        this.delay = user.delay;
    }

    public void UnSuspendUser(User user)
    {
        if(!(user.name == "" && user.surnames == "" && user.address == ""))
        {
            Console.WriteLine("Please fill the form with the user data");
            return;
        }
        if(!(user.blocked == false && user.delay == false))
        {
            Console.WriteLine("The user is blocked or still in a delay suspension period");
            return;
        }
        if(user.suspended = false)
        {
            Console.WriteLine("The user is not suspended");
            return;
        }

        Console.WriteLine("User " + user.name + " can now take loans");
        user.suspended = false;
    }
}



    public int id {get; set;} = 0;
    public string login {get; set;} = "";
    public string name {get; set;} = "";
    public string surnames {get; set;} = "";
    public string password {get; set;} = "";
    public string address {get; set;} = "";
    public bool suspended {get; set;} = true; //every user is suspended at the begining
    public bool blocked {get; set;} = false;
    public bool delay {get; set;} = false;