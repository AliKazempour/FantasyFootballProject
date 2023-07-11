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

        public static List<Object> SearchPlayers(string searchTerm)
        {
            var db = new Database();
            var filteredPlayers =
                db.players.Where(p => p.first_name.Contains(searchTerm) || p.second_name.Contains(searchTerm));
            List<Object> result = new List<Object>();
            foreach (var p in filteredPlayers)
            {
                result.Add(p);
            }

            return result;
        }

        public static List<Object> SortPlayersByPrice(bool ascending) //false --->صعودی //true-->نزولی
        {
            var db = new Database();
            var sortedPlayers = ascending
                ? db.players.OrderBy(p => p.now_cost).ToList()
                : db.players.OrderByDescending(p => p.now_cost).ToList();
            List<Object> result = new List<Object>();
            foreach (var p in sortedPlayers)
            {
                result.Add(p);
            }

            db.SaveChanges();
            return result;
        }

        public static List<Object> FilterPlayersByPosition(int position)
        {
            var db = new Database();
            var filteredPlayers = db.players.Where(p => p.element_type.Equals(position)).ToList();
            List<Object> result = new List<Object>();
            foreach (var p in filteredPlayers)
            {
                result.Add(p);
            }

            db.SaveChanges();
            return result;
        }

        public static List<Player> ShowPlayers(int page, int pageSize)
        {
            var db = new Database();
            var pagedPlayers = db.players.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            List<Object> result = new List<Object>();
            foreach (var p in pagedPlayers)
            {
                result.Add(p);
            }

            db.SaveChanges();
            return pagedPlayers;
        }
    }
}