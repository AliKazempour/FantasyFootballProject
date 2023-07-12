using FantasyFootballProject;
using System.Text.RegularExpressions;
using FantasyFootballProject.Logic;
using FantasyFootballProject.Business;
using FantasyFootballProject.Data_Access;

namespace FantasyFootballProject.Presention
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapPost("/user/signup/", logic.AddMemberApi);
            app.MapPost("/user/veritification/", memberLogic.Verification);
            app.MapGet("/user/login/", Token.LogIn);
            app.MapPost("/user/addPlayer/", logic.AddPlayer);

            app.MapGet("user/searchPlayer/", HandleplayerData.SearchPlayers);
            app.MapGet("user/SortPlayers/", HandleplayerData.SortPlayersByPrice);
            app.MapGet("user/FilterPlayersByPosition/", HandleplayerData.FilterPlayersByPosition);
            app.MapGet("user/inrange/", HandleplayerData.inRangePlayers);
            app.MapGet("user/FilterPlayersByTeam/", HandleplayerData.FilterPlayersByTeam);
            app.MapGet("user/ShowPlayers/", HandleplayerData.ShowPlayers);
            app.MapPut("updateDataBase/", updateData.Update);
            app.Run("http://localhost:7171");
        }
    }
}