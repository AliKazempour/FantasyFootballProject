using FantasyFootballProject.DataBase;
using System.Net.Mail;
using System.Text.RegularExpressions;
using FantasyFootballProject.Business;

namespace FantasyFootballProject.Logic;

public class logic
{
    public static Object AddMemberApi(User user)
    {
        try
        {
            memberLogic.addMember(user);
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }

        Object response = new
        {
            message = "Member successful added",
        };
        return response;
    }
}