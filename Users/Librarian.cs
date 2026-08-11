namespace Users;

public class Librarian : User
{
    public void UnSuspendUser(User user)
    {
        if(user.name != "" && user.surnames != "" && user.address != "" && user.blocked == false && user.delay == false)
        {
            user.suspended = false;
        }
    }
}