﻿using FantasyFootballProject.DataBase;

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
                var record = db.Users.FirstOrDefault(u => u.Username == user.Username);
                if (record != null)
                {
                    record.money = user.money;
                    db.Users.Update(record);
                    db.SaveChanges();
                }
            }
        }

        public static Player GetPlayerBYId(int id)
        {
            using (var db = new Database())
            {
                foreach (var b in db.players)
                {
                    if (b.id == id)
                    {
                        return b;
                    }
                }

                return new Player();
            }
        }

        public static List<Player> mainteam(string username)
        {
            List<Player> mainTeam = new List<Player>();
            using (var db = new Database())
            {
                foreach (var p in db.teams)
                {
                    if (p.username == username)
                    {
                        if (p.playerStatus == 1)
                        {
                            mainTeam.Add(GetPlayerBYId(p.playerId));
                        }
                    }
                }

                return mainTeam;
            }
        }

        public static List<Player> reserveTeam(string username)
        {
            List<Player> reserveTeam = new List<Player>();
            using (var db = new Database())
            {
                foreach (var p in db.teams)
                {
                    if (p.username == username)
                    {
                        if (p.playerStatus == 0)
                        {
                            reserveTeam.Add(GetPlayerBYId(p.playerId));
                        }
                    }
                }

                return reserveTeam;
            }
        }
    }
}