using FantasyFootballProject.DataBase;
using FantasyFootballProject.Business;

namespace FantasyFootballProject.Data_Access
{
    public class Handle_temp_user
    {
        public static void tempUserAdd(tempUser user)
        {
            using (var db = new Database())
            {
                db.temp_users.Add(user);
                db.SaveChanges();
            }
        }

        public static User userVerify(memberLogic.verify_user user)
        {
            using (var db = new Database())
            {
                var User = db.Users.FirstOrDefault(User => User.Username == user.username);
                return User;
            }
        }

        public static tempUser veifyTempUser(memberLogic.verify_user user)
        {
            using (var db = new Database())
            {
                var temp_user = db.temp_users.FirstOrDefault(temp =>
                    temp.username == user.username && temp.code == user.code && temp.time >= DateTime.Now);
                return temp_user;
            }
        }

        public static void remove_tempUser(tempUser temp_user)
        {
            using (var db = new Database())
            {
                db.temp_users.Remove(temp_user);
                db.SaveChanges();
            }
        }
    }
}