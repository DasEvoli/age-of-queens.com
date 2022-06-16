using System;
using System.Collections.Generic;

namespace Ageofqueenscom.Models
{
    public class ModsViewModel
    {
        public List<Mod> ModList = new List<Mod>();

        public class Mod
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Creator { get; set; }
            public DateTime UploadDate { get; set; }
            public int ModId { get; set; }
            public string Category { get; set; }
            public string ImageUrl { get; set; }
            public string ModUrl { get; set; }
        }
    }
}
