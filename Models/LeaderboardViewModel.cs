using System.Collections.Generic;

namespace Ageofqueenscom.Models
{
    public class LeaderboardViewModel
    {
        public List<LeaderboardPlayer> LeaderboardPlayerListRM;

        public class LeaderboardPlayer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
            public int Rating { get; set; }
            public int HighestRating { get; set; }
        }
    }
}
