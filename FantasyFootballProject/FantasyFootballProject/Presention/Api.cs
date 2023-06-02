using FantasyFootballProject;
using System.Text.RegularExpressions;
using FantasyFootballProject.Logic;

namespace FantasyFootballProject.Presention
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapPost("/user/signup/", logic.addMemberApi);
            app.MapPost("/user/veritification/", logic.vertification);

            app.Run("http://localhost:7171");
        }
    }
}