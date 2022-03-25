using ageofqueenscom.Code;
using System.Collections.Generic;

namespace ageofqueenscom.Models
{
    public class LeaderboardViewModel
    {
        public List<LeaderboardPlayerModel> LeaderboardPlayerListRM;

        public class LeaderboardPlayerModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
            public int Rating { get; set; }
            public int HighestRating { get; set; }
        }
    }
}
