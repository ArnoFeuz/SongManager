using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongManager
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }
        public int GenreId { get; set; }
        public Difficulty Difficulty { get; set; }
        public string Instrument { get; set; }
        public string Notes { get; set; }
        public string Sheet { get; set; }
}

    public class Artist
    {
     public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
}
