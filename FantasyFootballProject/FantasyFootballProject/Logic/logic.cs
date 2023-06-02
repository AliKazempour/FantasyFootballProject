using FantasyFootballProject.DataBase;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace FantasyFootballProject.Logic;

public class logic
{
    public static Object addMemberApi(User user)
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
                            string OTP = send(user.Email);
                            User u1 = new User(user.Name,user.Family,user.Email,user.Password,user.Username,user.UserOTP);
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
                var addr = new System.Net.Mail.MailAddress(email);
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
            return System.Text.RegularExpressions.Regex.IsMatch(password, pattern);
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

        public static string vertification(string name,string family,string email,string username,string password,string OTP)
        {
            using (var db = new Database())
            {
                foreach (var userOtpCode in db.Users)
                {
                    if (userOtpCode.Email==email && OTP==userOtpCode.UserOTP)
                    {
                        User u1 = new User();
                        u1.Name = userOtpCode.Name;
                        u1.Family = userOtpCode.Family;
                        u1.Email = userOtpCode.Email;
                        u1.Username = userOtpCode.Username;
                        u1.Password = userOtpCode.Password;
                        u1.UserOTP = userOtpCode.UserOTP;
                        db.Add(u1);
                        db.SaveChanges();
                        return "Your veritify is complete";
                    }
                }

                return "please input correct code !!!";
            }
        }
        public static string send(string email)
        {
            using (MailMessage mail = new MailMessage())
            {
                Random newrand = new Random();
                int securityPassword = newrand.Next(100000, 1000000);
                mail.From = new MailAddress("Alikazempoor83@gmail.com");
                mail.To.Add(Convert.ToString(email));
                mail.Subject = "test send mail";
                mail.Body = $"Hello!Welcome to FootBall Fantasy {Convert.ToString(securityPassword)}";
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new System.Net.NetworkCredential("Alikazempoor83@gmail.com", "torhhjolaelihrxa");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                return Convert.ToString(securityPassword);

            }
        }
}