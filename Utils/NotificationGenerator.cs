using Users;

namespace Utils;

public class NotificacionGenerator
{
    public void GenerateNotification(User user, string notification)
    {
        string format = "dd/MM/yyyy - HH:mm:ss";
        DateTime today = DateTime.Now;
        string todayString = today.ToString(format);

        user.notifications.Add(todayString + " | " + notification);
    }
}