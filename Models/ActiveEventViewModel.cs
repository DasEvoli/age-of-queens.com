using System.Collections.Generic;

namespace Ageofqueenscom.Models
{
    public class ActiveEventViewModel
    {
        public List<ActiveEventGame> ActiveGameEventGameList;
        public string Title;
        public string Information;
        public string RegistrationLink;
        public string Image;

        public class ActiveEventGame
        {
            public string Date {get;set;}
            public List<string> ActiveEventTeams;
            public string Maps {get;set;}
            public string Mode {get;set;}
            public string Information {get;set;}
        }
    }

}
