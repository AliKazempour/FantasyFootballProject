using FantasyFootballProject.DataBase;
using System.Net.Mail;
using System.Text.RegularExpressions;
using FantasyFootballProject.Business;
using FantasyFootballProject.Data_Access;

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

    public static string AddPlayer(User user, Player player)
    {
        user.team.AddPlayer(player, user.money);
        user.money -= player.now_cost;
        Handle_member_data.editUser(user);
        return "Player added to team";
    }

    public static string DeletePlayer(User user, Player player)
    {
        user.team.deletePlayer(player);
        user.money += player.now_cost;
        Handle_member_data.editUser(user);
        return "Player removed from team";
    }
}