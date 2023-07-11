using FantasyFootballProject.DataBase;
namespace FantasyFootballProject.Business
{
    public class updateData
    {
        public static void Update()
        {
            using (var db = new Database())
            {
                List<Player> list = new List<Player>();
                list = PlayerHandling.GetPlayersFromFPL();
                foreach (var p in list)
                {
                    db.players.Add(p);
                }

                db.SaveChanges();
            }
        }
    }
}