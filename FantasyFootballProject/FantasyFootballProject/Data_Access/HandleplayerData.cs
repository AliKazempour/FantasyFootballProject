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

        public static int difference(string first, string second)
        {
            if (first.Length != second.Length)
            {
                return 10;
            }

            int size = first.Length;
            int answer = 0;
            for (int i = 0; i < size; i++)
            {
                if (first[i] != second[i])
                {
                    answer++;
                }
            }

            return answer;
        }

        public static bool match(string correctName, string wrongName)
        {
            int correctSize = correctName.Length;
            int wrongSize = wrongName.Length;
            if (wrongSize < 5)
            {
                if (difference(correctName, wrongName) < 2)
                {
                    return true;
                }
            }
            else if (difference(correctName, wrongName) < 3)
            {
                return true;
            }

            if (Math.Abs(correctSize - wrongSize) > 1)
            {
                return false;
            }

            if (correctSize < wrongSize)
            {
                string tempString = correctName;
                int tempInt = correctSize;
                correctSize = wrongSize;
                wrongSize = tempInt;
                correctName = wrongName;
                wrongName = tempString;
            }

            for (int i = 0; i < correctSize; i++)
            {
                string temp = correctName[i].ToString();
                correctName = correctName.Remove(i, 1);
                if (correctName == wrongName)
                {
                    correctName = correctName.Insert(i, temp);
                    return true;
                }

                correctName = correctName.Insert(i, temp);
            }

            return false;
        }

        public static string makeValid(string name)
        {
            name = name.ToLower();
            string cur = name[0].ToString();
            cur = cur.ToUpper();
            name = name.Remove(0, 1).Insert(0, cur);
            int spaceIndex = name.IndexOf(' ');
            if (spaceIndex == -1)
            {
                return name;
            }

            cur = name[spaceIndex + 1].ToString();
            cur = cur.ToUpper();
            name = name.Remove(spaceIndex + 1, 1).Insert(spaceIndex + 1, cur);
            return name;
        }

        public static bool isSubsequence(string name, string subsequence)
        {
            int size1 = name.Length;
            int size2 = subsequence.Length;
            if (size2 > size1)
            {
                return false;
            }

            for (int i = 0; i < size2; i++)
            {
                if (name[i] != subsequence[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool validPlayer1(string userPlayerName, Player player)
        {
            userPlayerName = makeValid(userPlayerName);
            string firstName = player.first_name;
            string lastName = player.second_name;
            string fullName = firstName + ' ' + lastName;
            return isSubsequence(firstName, userPlayerName) || isSubsequence(lastName, userPlayerName) ||
                   isSubsequence(fullName, userPlayerName);
        }

        public static bool validPlayer2(string userPlayerName, Player player)
        {
            userPlayerName = makeValid(userPlayerName);
            string firstName = player.first_name;
            string lastName = player.second_name;
            string fullName = firstName + ' ' + lastName;
            if (match(firstName, userPlayerName) || match(lastName, userPlayerName) ||
                match(fullName, userPlayerName))
            {
                return !validPlayer1(userPlayerName, player);
            }

            return false;
        }

        public static void sortPlayersInTermsOfName(List<Player> players, int index)
        {
            int size = players.Count;
            for (int i = index; i < size - 1; i++)
            {
                for (int j = i + 1; j < size; j++)
                {
                    if (players[i].first_name.CompareTo(players[j].first_name) > 0 ||
                        (players[i].first_name == players[j].first_name &&
                         players[i].second_name.CompareTo(players[j].second_name) > 0))
                    {
                        Player temp = players[i];
                        players[i] = players[j];
                        players[j] = temp;
                    }
                }
            }
        }

        public static List<Player> SearchPlayers(string searchTerm)
        {
            var db = new Database();
            var filteredPlayers1 =
                db.players.ToList().Where(p => validPlayer1(searchTerm, p)).ToList();
            List<Player> result = new List<Player>();
            foreach (var p in filteredPlayers1)
            {
                result.Add(p);
            }

            int size = result.Count;
            sortPlayersInTermsOfName(result, 0);
            var filteredPlayers2 =
                db.players.ToList().Where(p => validPlayer2(searchTerm, p)).ToList();
            foreach (var p in filteredPlayers2)
            {
                result.Add(p);
            }

            sortPlayersInTermsOfName(result, size);
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