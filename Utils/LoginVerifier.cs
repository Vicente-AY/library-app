using Users;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Utils;

public class LoginVerifier
{

    public User? CheckLogin(string? login)
    {

        if (string.IsNullOrWhiteSpace(login))
        {
            return null;
        }

        using(LibraryContext db = new LibraryContext())
        {
            return db.Users.Include(u => u.notifications)
                .Include(w => w.userWaitList).ThenInclude(i => i.item).ThenInclude(w => w.waitList)
                .Include(l => l.loanList).ThenInclude(i => i.item)
                .ThenInclude(w => w.waitList).ThenInclude(u => u.user)
                .FirstOrDefault(u => u.login == login);
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