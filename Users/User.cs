namespace Users;

public class User
{
    int id {get; set;} = 0;
    string login {get; set;} = "";
    string name {get; set;} = "";
    string surnames {get; set;} = "";
    string password {get; set;} = "";
    string address {get; set;} = "";
    bool suspended {get; set;} = true; //every user is suspended at the begining
    bool blocked {get; set;} = false;
    bool delay {get; set;} = false;

    public User(int id, string login, string password)
    {
        this.id = id;
        this.login = login;
        this.password = password;
    }

    public void fillForm(string name, string surnames, string address)
    {
        this.name = name;
        this.surnames = surnames;
        this.address = address;
    }
}