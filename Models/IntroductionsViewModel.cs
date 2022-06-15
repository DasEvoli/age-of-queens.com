using System.Collections.Generic;

namespace Ageofqueenscom.Models
{
    public class IntroductionsViewModel
    {
        public List<Introduction> IntroductionList = new List<Introduction>();

        public class Introduction
        {
            public string Name { get; set; }
            public string Content { get; set; }
            public string ImageUrl { get; set; }
            public string TwitterUrl { get; set; }
            public string YoutubeUrl { get; set; }
            public string TwitchUrl { get; set; }
            public string InstagramUrl { get; set; }
        }
    }
}
