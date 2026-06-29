using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongManager
{
    public class DataRepository
    {
        public List<Song> Songs { get; } = new List<Song>();
        public List<Artist> Artists { get; } = new List<Artist>();
        public List<Genre> Genres { get; } = new List<Genre>();
        public List<difficulty> Difficulties { get; } = new List<difficulty>();
    }
}
