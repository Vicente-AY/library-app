using Loans;

namespace Users;

public class User
{
    public int id {get; set;} = 0;
    public string login {get; set;} = "";
    public string name {get; set;} = "";
    public string surnames {get; set;} = "";
    public DateTime? bDate {get; set;} = null;
    public string password {get; set;} = "";
    public string address {get; set;} = "";
    public bool suspended {get; set;} = true; //every user is suspended at the begining
    public bool blocked {get; set;} = false;
    public bool delay {get; set;} = false;
    public List<Loan> loanList {get; set;} = new List<Loan>();
    public List<string> notifications {get; set;} = new List<string>();

    public User(int id, string login, string password)
    {
        this.id = id;
        this.login = login;
        this.password = password;
    }
    protected User(){} //solo para EF Core

    public User(Librarian librarian)
    {
        this.id = librarian.id;
        this.login = librarian.login;
        this.name = librarian.name;
        this.surnames = librarian.surnames;
        this.bDate = librarian.bDate;
        this.password = librarian.password;
        this.address = librarian.address;
        this.suspended = librarian.suspended;
        this.blocked = librarian.blocked;
        this.delay = librarian.delay;

        //delete librarian
        //add user
    }

    public void fillForm(string name, string surnames, DateTime bDate, string address)
    {
        this.name = name;
        this.surnames = surnames;
        this.bDate = bDate;
        this.address = address;
    }

    public void CheckData()
    {
        Console.WriteLine("\nYour Account information");
        Console.WriteLine("------------------------\n");

        Console.WriteLine($"User Id: {this.id}");
        Console.WriteLine($"Username: {this.login}");

        if (!string.IsNullOrWhiteSpace(this.name)) 
            Console.WriteLine($"Name: {this.name}");

        if (!string.IsNullOrWhiteSpace(this.surnames)) 
            Console.WriteLine($"Surnames: {this.surnames}");

        if (this.bDate is not null) 
            Console.WriteLine($"Birthdate: {this.bDate.Value.ToString("dd/MM/yyyy")}");

        if (!string.IsNullOrWhiteSpace(this.address)) 
            Console.WriteLine($"Address: {this.address}");

        if (this.suspended)
        {
            if (string.IsNullOrWhiteSpace(this.name))
            {
                Console.WriteLine("Account status: Suspended (You need to fill the form)");
            }
            else
            {
                Console.WriteLine("Account status: Temporarily suspended");
            }
        }
    }
}