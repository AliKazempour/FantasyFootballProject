using FantasyFootballProject.DataBase;
using FantasyFootballProject.Data_Access;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace FantasyFootballProject.Business
{
    public class memberLogic
    {
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
            var User = Handle_temp_user.userVerify(user);
            var temp_user = Handle_temp_user.veifyTempUser(user);

            if (temp_user != null)
            {
                User.verified = true;
                Handle_temp_user.remove_tempUser(temp_user);

                return "complete verify progress ";
            }

            return "please input correct code !!!";
        }

        public static string genteratePassword()
        {
            var newRand = new Random();
            var securityPassword = newRand.Next(100000, 1000000);
            return securityPassword.ToString();
        }


        public static void SendMail(string email, string code)
        {
            using (MailMessage mail = new MailMessage())
            {
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

        public static void addMember(User user)
        {
            if (IsValidEmail(user.Email) && IsValidPassword(user.Password) && IsValidUsername(user.Username))
            {
                if (Handle_member_data.findUserByEmail(user.Email))
                {
                    throw new Exception("This Email is already taken.");
                }
                else if (Handle_member_data.findUserByUsername(user.Username))
                {
                    throw new Exception("This username is already taken");
                }
                else
                {
                    var code = genteratePassword();
                    SendMail(user.Email, code);
                    tempUser tempUser1 = new tempUser
                        { code = code, username = user.Username, time = DateTime.Now.AddMinutes(5) };
                    Handle_temp_user.tempUserAdd(tempUser1);
                    User u1 = new User(user.Name, user.Family, user.Email, user.Password, user.Username);
                    Handle_member_data.UserAdd(u1);
                }
            }
            else
            {
                throw new Exception("Error! Please fill in the required terms more carefully");
            }
        }
    }
}