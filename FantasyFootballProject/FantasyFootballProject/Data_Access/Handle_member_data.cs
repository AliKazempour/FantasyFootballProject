using FantasyFootballProject.DataBase;

namespace FantasyFootballProject.Data_Access
{
    public class Handle_member_data
    {
        public static bool findUserByEmail(string email)
        {
            using (var db = new Database())
            {
                return db.Users.Any(u => u.Email == email);
            }
        }

        public static List<User> getUsers()
        {
            using (var db = new Database())
            {
                return db.Users.ToList();
            }
        }

        public static User getUserByUsername(string username)
        {
            using (var db = new Database())
            {
                return db.Users.FirstOrDefault(u => u.Username == username);
            }
        }

        public static bool findUserByUsername(string username)
        {
            using (var db = new Database())
            {
                return db.Users.Any((u => u.Username == username));
            }
        }

        public static void UserAdd(User user)
        {
            using (var db = new Database())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public static bool checkUser(string username, string password)
        {
            using (var db = new Database())
            {
                return db.Users.Any((u => u.Username == username && u.Password == password));
            }
        }

        public static void editUser(User user)
        {
            using (var db = new Database())
            {
                var record = db.Users.FirstOrDefault(user => user.Username == user.Username);
                record = user;
                db.SaveChanges();
            }
        }
    }
}