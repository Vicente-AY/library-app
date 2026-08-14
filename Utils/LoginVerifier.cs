using Users;
using Data;

namespace Utils;

public class LoginVerifier
{

    public User? CheckLogin(string? login)
    {

        if (string.IsNullOrWhiteSpace(login))
        {
            return null;
        }

        using(var db = new LibraryContext())
        {
            return db.Users.FirstOrDefault(u => u.login == login);
        }
    }

    public bool CheckPass(string pass, User? user)
    {
        if(string.IsNullOrWhiteSpace(pass) || user is null || !pass.Equals(user.password))
        {
            return false;
        }
        return true;
    }
}