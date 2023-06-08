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
        
    public class User
    {
        [Required] public string Name { get; set; }

        [Required] public string Family { get; set; }

        [Required][EmailAddress] public string Email { get; set; }

        [Key] public string Username { get; set; }

        [Required] public string Password { get; set; }
        // public string UserOTP { get; set; }
        [Required] public bool verified { get; set; }


        public User(string name, string family,string email, string password, string username)
        {
            this.Name = name;
            this.Family = family;
            this.Password = password;
            this.Username = username;
            // this.UserOTP =otp;
            this.Email = email;
            this.verified = false;
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