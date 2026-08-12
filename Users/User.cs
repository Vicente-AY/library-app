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

    public User(int id, string login, string password)
    {
        this.id = id;
        this.login = login;
        this.password = password;
    }

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
}