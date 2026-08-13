namespace Utils;

using ProgramExceptions;
public class Verifier
{
    public static string CheckLogin(string login)
    {

        login = login.Trim();

        if (int.TryParse(login, out _))
        {
            throw new FormatException("You type only numbers. Please, enter a valid Username");
        }
        if(login.Length < 7)
        {
            throw new ShortStringException("The login is too short. Please, enter a valid Username");
        }
        if(!Char.IsUpper(login[0]))
        {
            throw new NotFirstUppercaseException("The login does not start with an uppercase. Please, enter a valid Username");
        }
        if(login.Contains(" "))
        {
            throw new WhiteSpaceException("The login has spaces. Please, enter a valid Username");
        }

        return login;
    }
}