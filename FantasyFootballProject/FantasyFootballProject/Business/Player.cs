using FantasyFootballProject.DataBase;
using ServiceStack;
using FantasyFootballProject.Data_Access;

namespace FantasyFootballProject.Business
{
    public class PlayerHandling
    {
        public static void getFromApi()
        {
            string url = "https://fantasy.premierleague.com/api/bootstrap-static/";
            var response = url.GetJsonFromUrl().FromJson<FPLResponse>();
            HandleplayerData.savePlayer(response.elements);
        }
    }

    public class FPLResponse
    {
        public List<Player> elements { get; set; }
    }
}