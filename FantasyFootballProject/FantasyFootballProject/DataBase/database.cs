using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballProject.DataBase
{
    public class Database : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<tempUser> temp_users { get; set; }
        public DbSet<Player> players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder contextOptionsBuilder)
        {
            contextOptionsBuilder.UseSqlite("Data source=database.db");
        }
    }
    public class Player
    {
        [Key]
        public int id { get; set; }
        public string first_name { get; set; }
        public string second_name { get; set; }
        public int element_type { get; set; }
        public int now_cost { get; set; }
        public int team { get; set; }
        public double total_points { get; set; }
    }
    public class Response
    {
        public List<Player> elements { get; set; }
    }

    public class Team
    {
        [Key]
        public int teamCode { get; set; }
        public List<Player> mainTeam { get; set; }
        public List<Player> reserveTeam { get; set; }


        public void AddPlayer(Player player, int budget)
        {
            int counter = 0;
            foreach (Player p in mainTeam)
            {
                if (player.team == p.team)
                {
                    counter++;
                }
            }
            foreach (Player p in reserveTeam)
            {
                if (player.team == p.team)
                {
                    counter++;
                }
            }
            if (counter > 3)
            {
                throw new Exception("Can't select more than 3 players from the same team.");
            }
            if (player.element_type == 1 && mainTeam.FindAll(p => p.element_type == 1).Count >= 2)
            {
                throw new Exception("Can't select more than 2 goalkeepers.");
            }
            if (player.element_type == 2 && mainTeam.FindAll(p => p.element_type == 2).Count >= 5)
            {
                throw new Exception("Can't select more than 5 defenders.");
            }
            if (player.element_type == 3 && mainTeam.FindAll(p => p.element_type == 3).Count >= 5)
            {
                throw new Exception("Can't select more than 5 midfielders.");
            }
            if (player.element_type == 4 && mainTeam.FindAll(p => p.element_type == 4).Count >= 3)
            {
                throw new Exception("Can't select more than 3 forwards.");
            }
            if (player.now_cost > budget)
            {
                throw new Exception("Not enough budget to buy this player.");
            }
            if (mainTeam.Count == 11)
            {
                reserveTeam.Add(player);
                return;
            }
            mainTeam.Add(player);
        }
        public void deletePlayer(Player player)
        {
            foreach (Player p in mainTeam)
            {
                if (p == player)
                {
                    mainTeam.Remove(p);
                }
            }
            foreach (Player p in reserveTeam)
            {
                if (p == player)
                {
                    reserveTeam.Remove(p);
                }
            }
        }

    }

    public class User
    {
        [Required] public string Name { get; set; }
        [Required] public string Family { get; set; }

        [Required][EmailAddress] public string Email { get; set; }

        [Key] public string Username { get; set; }

        [Required] public string Password { get; set; }
        [Required] public bool verified { get; set; }

        public double score { get; set; }
        public int money { get; set; }

        public Team team { get; set; }
        ICollection<User> players { get; set; }
        ICollection<User> followers { get; set; }
        public User(string name, string family, string email, string password, string username)
        {
            this.Name = name;
            this.Family = family;
            this.Password = password;
            this.Username = username;
            this.Email = email;
            this.verified = false;
            this.score = 0;
            this.money = 1000;
            this.players = null;
            this.followers = null;

        }
        public User() { }



    }
    public class tempUser
    {


        [Key] public string username { get; set; }
        [Required] public string code { get; set; }
        [Required] public DateTime time { get; set; }
    }
}