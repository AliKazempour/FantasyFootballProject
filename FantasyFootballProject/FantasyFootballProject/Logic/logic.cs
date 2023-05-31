using FantasyFootballProject.DataBase;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace FantasyFootballProject.Logic;

public class logic
{
    // Validate email address
    public static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    // Validate password
    public static bool IsValidPassword(string password)
    {
        string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$";
        return System.Text.RegularExpressions.Regex.IsMatch(password, pattern);
    }

    //// Validate username
    public static bool IsValidUsername(string username)
    {
        if (username.Length < 3 || username.Length > 20)
        {
            return false;
        }

        Regex regex = new Regex("^[a-zA-Z0-9_-]*$");
        if (!regex.IsMatch(username))
        {
            return false;
        }

        return true;
    }
}