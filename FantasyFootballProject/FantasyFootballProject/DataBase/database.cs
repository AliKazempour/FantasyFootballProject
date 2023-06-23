using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballProject.DataBase
{
    public class Database : DbContext
    {
        public DbSet<tempUser> temp_users { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder contextOptionsBuilder)
        {
            contextOptionsBuilder.UseSqlite("Data source=database.db");
        }
    }

    public class Player
    {
        [Key] public int playerKey { get; set; }
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


    public class User
    {
        [Required] public string Name { get; set; }
        [Required] public string Family { get; set; }

        [Required] [EmailAddress] public string Email { get; set; }

        [Key] public string Username { get; set; }

        [Required] public string Password { get; set; }
        [Required] public bool verified { get; set; }

        public double score { get; set; }
        public double money { get; set; }

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
            this.money = 100;
            this.players = null;
            this.followers = null;
        }

        public User()
        {
        }
    }

    public class tempUser
    {
        [Key] public string username { get; set; }
        [Required] public string code { get; set; }
        [Required] public DateTime time { get; set; }
    }
}