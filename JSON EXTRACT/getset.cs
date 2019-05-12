using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_EXTRACT
{
    class getset
    {
        public class Movie
        {
            public string Name { get; set; }
            public DateTime Airdate { get; set; }
            public int RottenTomatoRating { get; set; }
            public string[] MainCharacters { get; set; }
            public string Genres { get; set; }
        }
    }
}
