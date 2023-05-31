using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballProject.DataBase
{
    public class Database : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserOTP> UsersOTP { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder contextOptionsBuilder)
        {
            contextOptionsBuilder.UseSqlite("Data source=database.db");
        }
    }

    public class UserOTP
    {
        public string Name { get; set; }
        public string Family { get; set; }

        public string Email { get; set; }

        [Key] public string Username { get; set; }
        public string Password { get; set; }
        public string OTP { get; set; }

        /*public UserOTP(string name, string family, string password, string username, string otp)
        {
            this.Name = name;
            this.Family = family;
            this.Password = password;
            this.Username = username;
            this.OTP = null;
        }*/
        public UserOTP(string name, string family, string password, string username)
        {
            this.Name = name;
            this.Family = family;
            this.Password = password;
            this.OTP = null;
            this.Username = username;
        }
    }

    public class User
    {
        [Required] public string Name { get; set; }

        [Required] public string Family { get; set; }

        [Required] [EmailAddress] public string Email { get; set; }

        [Key] public string Username { get; set; }

        [Required] public string Password { get; set; }
    }
}