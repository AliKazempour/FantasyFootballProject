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

        public static List<Object>
            inRangePlayers(int lowerBound, int upperBound, bool ascending) //false --->صعودی //true-->نزولی
        {
            var db = new Database();
            var filteredPlayers = db.players.Where(p => lowerBound <= p.now_cost && upperBound >= p.now_cost);

            if (ascending)
            {
                filteredPlayers = filteredPlayers.OrderBy(p => p.now_cost);
            }
            else
            {
                filteredPlayers = filteredPlayers.OrderByDescending(p => p.now_cost);
            }

            List<Object> result = new List<Object>();
            foreach (var p in filteredPlayers)
            {
                result.Add(p);
            }

            db.SaveChanges();
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

        public static List<Object>
            FilterPlayersByPosition(int position, bool ascending) //false --->صعودی //true-->نزولی
        {
            var db = new Database();
            var filteredPlayers = db.players.Where(p => p.element_type.Equals(position)).ToList();
            filteredPlayers.Sort((p1, p2) => p1.now_cost.CompareTo(p2.now_cost) * (ascending ? 1 : -1));
            List<Object> result = new List<Object>();
            foreach (var p in filteredPlayers)
            {
                result.Add(p);
            }

            db.SaveChanges();
            return result;
        }

        public static List<Object> FilterPlayersByTeam(int team)
        {
            var db = new Database();
            var filteredPlayers = db.players.ToList().Where(p => p.team.Equals(team));
            List<Object> result = new List<Object>();
            foreach (var p in filteredPlayers)
            {
                result.Add(p);
            }

            db.SaveChanges();
            return result;
        }

        public static List<Player> ShowPlayers(int page, int pageSize) //SadHezari
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