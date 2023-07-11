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
            var client = new RestClient("https://fantasy.premierleague.com");
            var request = new RestRequest("/api/bootstrap-static/", Method.GET);
            var response = client.Execute(request);
            var json = response.Content;
            var data = JsonConvert.DeserializeObject<dynamic>(json);
            var elements = data["elements"];
            var players = JsonConvert.DeserializeObject<List<Player>>(elements.ToString());
            return players;
        }
    }

    public class FPLResponse
    {
        public List<Player> elements { get; set; }
    }
}