using Users;
using Data;

namespace Utils;

public class LoginVerifier
{

    LibraryContext db = new LibraryContext();
    public User? CheckLogin(string? login)
    {

        User? logUser = null;

        List<User> users = db.Users.ToList();

        foreach(var u in users)
        {
            if (u.login.Equals(login))
            {
                logUser = u;
                break;
            }
        }

        return logUser;
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