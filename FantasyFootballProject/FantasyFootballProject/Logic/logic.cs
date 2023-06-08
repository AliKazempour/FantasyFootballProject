using FantasyFootballProject.DataBase;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace FantasyFootballProject.Logic;

public class logic
{
        public static Object AddMemberApi(User user)
    {
        using (var db = new Database())
        {
            if (IsValidEmail(user.Email) && IsValidPassword(user.Password) && IsValidUsername(user.Username))
            {
                if (db.Users.Any(u => u.Email == user.Email))
                {
                    return "This Email is already taken.";
                }
                else if (db.Users.Any((u => u.Username == user.Username)))
                {
                    return "This username is already taken";
                }
                else
                {
                    try
                    {
                       var code= genteratePassword();
                         SendMail(user.Email,code);
                        db.temp_users.Add(new tempUser { code = code, username = user.Username, time = DateTime.Now.AddMinutes(5) });
                        db.SaveChanges();
                        User u1 = new User(user.Name, user.Family, user.Email, user.Password, user.Username);
                        db.Users.Add(u1);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return ex.ToString();
                    }

                }
            }
            else
            {
                return "Error! Please fill in the required terms more carefully";
            }


        }
        Object response = new
        {
            message = "Member successful added",
        };
        return response;
    }

    // Validate email address
    public static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    // Validate password
    public static bool IsValidPassword(string password)
    {
        string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$";
        return Regex.IsMatch(password, pattern);
    }

    //// Validate username
    public static bool IsValidUsername(string username)
    {
        if (username.Length < 3 || username.Length > 20)
        {
            return false;
        }
        Regex regex = new Regex("^[a-zA-Z0-9_-]*$");
        if (!regex.IsMatch(username))
        {
            return false;
        }

        return true;
    }
    public record verify_user(string username, string code);

    public static string Verification(verify_user user)
    {
        using (var db = new Database())
        {
            var User=db.Users.FirstOrDefault(User=> User.Username==user.username);
            var temp_user = db.temp_users.FirstOrDefault(temp => temp.username == user.username && temp.code == user.code && temp.time>=DateTime.Now);
            if (temp_user != null)
            {
                User.verified= true;
                db.temp_users.Remove(temp_user);
                db.SaveChanges();

                return "complete verify progress ";
            }
                

            return "please input correct code !!!";
        }
    }
    public static string genteratePassword()
    {
        var newRand = new Random();
            var securityPassword = newRand.Next(100000, 1000000);
        return securityPassword.ToString();

    }


    public static void SendMail(string email,string code)
    {
        using (MailMessage mail = new MailMessage())
        {
            /*var newRand = new Random();
            var securityPassword = newRand.Next(100000, 1000000);*/
            mail.From = new MailAddress("Alikazempoor83@gmail.com");
            mail.To.Add(Convert.ToString(email));
            mail.Subject = "test send mail";
            mail.Body = $"Hello!Welcome to FootBall Fantasy {code}";
            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new System.Net.NetworkCredential("Alikazempoor83@gmail.com", "torhhjolaelihrxa");
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
           

        }
    }
}