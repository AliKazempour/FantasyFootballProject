using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballProject.DataBase
{
    public class Database : DbContext
    {
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
        public string UserOTP { get; set; }
        public User(string name, string family,string email, string password, string username, string otp)
        {
            this.Name = name;
            this.Family = family;
            this.Password = password;
            this.Username = username;
            this.UserOTP ="";
            this.Email = email;
        }
        public User() { }

    }
}