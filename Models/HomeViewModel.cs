using System.Collections.Generic;

namespace ageofqueenscom.Models
{
    public class HomeViewModel
    {
        public List<Blogpost> BlogpostList;
        
        public class Blogpost
        {
            public string Title { get; set;}
            public string Content { get; set; }
            public string Author { get; set; }
            public string Created { get; set; }
            public string ImageUrl { get; set; }
        }
    }
}
