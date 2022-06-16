using System;
using System.Collections.Generic;

namespace Ageofqueenscom.Models
{
    public class HomeViewModel
    {
        public List<Blogpost> BlogpostList = new List<Blogpost>();
        
        public class Blogpost
        {
            public string Title { get; set;}
            public string Content { get; set; }
            public string Author { get; set; }
            public DateTime Created { get; set; }
            public string ImageName { get; set; }
        }
    }
}
