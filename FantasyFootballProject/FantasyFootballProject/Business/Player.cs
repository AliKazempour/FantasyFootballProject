using FantasyFootballProject.DataBase;
using ServiceStack;
using FantasyFootballProject.Data_Access;
using Newtonsoft.Json;
using RestSharp;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace FantasyFootballProject.Business
{
    public class PlayerHandling
    {
        public static List<Player> GetPlayersFromFPL()
        {
            string url = "https://fantasy.premierleague.com/api/bootstrap-static/";
            var response = url.GetJsonFromUrl().FromJson<FPLResponse>();
            return response.elements;
        }
    }

    public class FPLResponse
    {
        public List<Player> elements { get; set; }
    }
}