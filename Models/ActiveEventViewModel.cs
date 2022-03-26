using System.Collections.Generic;

namespace ageofqueenscom.Models
{
    public class ActiveEventViewModel
    {
        public List<ActiveEventGameModel> ActiveGameEventGameList;
        public string Title;
        public string Information;
        public string RegistrationLink;
        public string Image;

        public class ActiveEventGameModel
        {
            public string Date {get;set;}
            public List<string> ActiveEventTeams;
            public string Maps {get;set;}
            public string Mode {get;set;}
            public string Information {get;set;}
        }
    }

}
