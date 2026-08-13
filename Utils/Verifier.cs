using System.Text.RegularExpressions;
using ProgramExceptions;

namespace Utils;

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

    public static string CheckPass(string pass)
    {
        pass = pass.Trim();


        if(!Regex.IsMatch(pass, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{7,}$"))
        {
            throw new NotPatternException("\nThe password does not match the stablished pattern. Please, enter a valid Password");
        }

        return pass;
    }
}