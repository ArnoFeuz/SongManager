namespace SongManager
{
    public class DataRepository
    {
        public List<Song> Songs { get; } = new List<Song>();
        public List<Artist> Artists { get; } = new List<Artist>();
        public List<Genre> Genres { get; } = new List<Genre>();

        public void AddSongWithArtistAndGenre(string songTitle, string artistName, string genreName, string instrument, string notes, string sheet, Difficulty difficulty)
        {
            Artist existingArtist = Artists.FirstOrDefault(a => a.Name.Equals(artistName, StringComparison.OrdinalIgnoreCase));
            int assignedArtistId;

            if (existingArtist == null)
            {
                int newArtistId = Artists.Count > 0 ? Artists.Max(a => a.Id) + 1 : 1;
                Artist newArtist = new Artist { Id = newArtistId, Name = artistName };
                Artists.Add(newArtist);
                assignedArtistId = newArtistId;
            }
            else
            {
                assignedArtistId = existingArtist.Id;
            }
            Genre existingGenre = Genres.FirstOrDefault(g => g.Name.Equals(genreName, StringComparison.OrdinalIgnoreCase));
            int assignedGenreId;

            if (existingGenre == null)
            {
                int newGenreId = Genres.Count > 0 ? Genres.Max(g => g.Id) + 1 : 1;
                Genre newGenre = new Genre { Id = newGenreId, Name = genreName };
                Genres.Add(newGenre);
                assignedGenreId = newGenreId;
            }
            else
            {
                assignedGenreId = existingGenre.Id;
            }
            int newSongId = Songs.Count > 0 ? Songs.Max(s => s.Id) + 1 : 1;

            Song newSong = new Song
            {
                Id = newSongId,
                Title = songTitle,
                ArtistId = assignedArtistId,
                GenreId = assignedGenreId,
                Instrument = instrument,
                Notes = notes,
                Sheet = sheet,
                Difficulty = difficulty
            };

            Songs.Add(newSong);
        }
        public bool UpdateSong(int songId, string newTitle, string newNotes, string newSheet, Difficulty newDifficulty)
        {
            Song songToUpdate = Songs.FirstOrDefault(s => s.Id == songId);
            if (songToUpdate == null)
            {
                return false;
            }
            else
            {
                songToUpdate.Title = newTitle;
                songToUpdate.Notes = newNotes;
                songToUpdate.Sheet = newSheet;
                songToUpdate.Difficulty = newDifficulty;
                return true;
            }
        }
        public bool DeleteSong(int songId)
        {
            Song songToDelete = Songs.FirstOrDefault(s => s.Id == songId);
            if (songToDelete == null)
            {
                return false;
            }

            Songs.Remove(songToDelete);
            {
                return true;
            }
        }
        public List<Song> SearchSongsByTitle(string title)
        {
            return Songs.Where(s => s.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public Song SearchSongsById(int id)
        {
            return Songs.FirstOrDefault(s => s.Id == id);
        }
        public List<Song> SearchSongsByArtist(string artistName)
        {
            var matchingArtists = Artists.Where(a => a.Name.Contains(artistName, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matchingArtists.Count == 0) return new List<Song>();

            return Songs.Where(s => matchingArtists.Any(a => a.Id == s.ArtistId)).ToList();
        }

        public List<Song> SearchSongsByGenre(string genreName)
        {
            var matchingGenres = Genres.Where(g => g.Name.Contains(genreName, StringComparison.OrdinalIgnoreCase)).ToList();
            if (matchingGenres.Count == 0) return new List<Song>();

            return Songs.Where(s => matchingGenres.Any(g => g.Id == s.GenreId)).ToList();
        }
        public List<Song> SearchSongsByDifficulty(Difficulty difficulty)
        {
            return Songs.Where(s => s.Difficulty == difficulty).ToList();
        }   
        public List<Song> SearchSongsByInstrument(string instrument)
        {
            return Songs.Where(s => s.Instrument.Contains(instrument, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public Artist GetArtistByID(int artistId)
        {
            return Artists.FirstOrDefault(a => a.Id == artistId);
        }
    }
}