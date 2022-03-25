﻿using ageofqueenscom.Code;
using System.Collections.Generic;

namespace ageofqueenscom.Models
{
    public class ModsViewModel
    {
        public List<ModModel> ModList;

        public class ModModel
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Creator { get; set; }
            public string Date { get; set; }
            public string Id { get; set; }
            public string Category { get; set; }
            public string ImageUrl { get; set; }
            public string ModUrl { get; set; }
        }
    }

}
