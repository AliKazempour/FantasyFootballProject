using FantasyFootballProject.DataBase;
using System.Net.Mail;
using System.Text.RegularExpressions;
using FantasyFootballProject.Business;
using FantasyFootballProject.Data_Access;
using FantasyFootballProject.Logic;

namespace FantasyFootballProject.Logic
{
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

        public static string AddPlayer(string token, Player player)
        {
            User user = memberLogic.getUserByToken(token);
            Team.AddPlayer(player, user.money, user.Username);
            user.money -= player.now_cost;
            Handle_member_data.editUser(user);
            return "Player added to team";
        }

        public static string DeletePlayer(string token, Player player)
        {
            User user = memberLogic.getUserByToken(token);
            Team.deletePlayer(player, user.Username);
            user.money += player.now_cost;
            Handle_member_data.editUser(user);
            return "Player removed from team";
        }
    }
}