using FantasyFootballProject.DataBase;

namespace FantasyFootballProject.Data_Access
{
    public class HandleplayerData
    {
        public static void savePlayer(List<Player> players)
        {
            using (var db = new Database())
            {
                foreach (var p in db.players)
                {
                    db.players.Remove(p);
                }

                foreach (var p in players)
                {
                    db.players.Add(p);
                }

                db.SaveChanges();
            }
        }
    }
}