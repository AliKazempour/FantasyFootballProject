using FantasyFootballProject;
using System.Text.RegularExpressions;
using FantasyFootballProject.Logic;
using FantasyFootballProject.Business;

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

            app.Run("http://localhost:7171");
        }
    }
}