using System.Collections.Generic;

namespace ageofqueenscom.Models
{
    public class IntroductionsViewModel
    {
        public List<IntroductionModel> Introductions;

        public class IntroductionModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string ImageUrl { get; set; }
            public string TwitterUrl { get; set; }
            public string YoutubeUrl { get; set; }
            public string TwitchUrl { get; set; }
            public string InstagramUrl { get; set; }
        }
    }
}
