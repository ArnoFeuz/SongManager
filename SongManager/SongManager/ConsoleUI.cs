using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;


namespace SongManager
{
    internal class ConsoleUI
    {
        private readonly DataRepository _dataRepository;

        public ConsoleUI(DataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public void RunMenu()
        {
            while (true)
            {
                MenuColor();
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("              Song Manager              ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Add Song");
                Console.WriteLine("2. Search");
                Console.WriteLine("3. Update Song");
                Console.WriteLine("4. Delete Song");
                Console.WriteLine("5. List All Songs");
                Console.WriteLine("6. Save");
                Console.WriteLine("7. Exit");
                Console.WriteLine("========================================");
                RandomSongs();
                Console.ForegroundColor = ConsoleColor.DarkGray ;
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AddSong();
                        break;
                    case "2":
                        SearchOptions();
                        break;
                    case "3":
                        UpdateSong();
                        break;
                    case "4":
                        DeleteSong();
                        break;
                    case "5":
                        ListAllSongs();
                        break;
                    case "6":
                        Console.WriteLine("Saving data to JSON file...");
                        JsonStorage.SaveDataToJsonFile(_dataRepository, "songs.json");
                        Console.WriteLine("Data saved successfully.");  
                        break;
                    case "7":
                        Console.WriteLine("Saving data to JSON file...");
                        JsonStorage.SaveDataToJsonFile(_dataRepository, "songs.json");
                        Console.WriteLine("Data saved successfully. Exiting the application.");
                        Environment.Exit(0);
                        break;
                    default:
                        WriteError("Invalid option. Please try again.");
                        break;
                }
            }

        }

        private void AddSong()
        {
            MenuColor();
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("              Song Manager              ");
            Console.WriteLine("                Add Song                ");
            Console.WriteLine("========================================");
            WritePrompt("Enter Song Title:");
            string title = Console.ReadLine();
            WritePrompt("Enter artist name: ");
            string artistName = Console.ReadLine();
            WritePrompt("Enter genre name: ");
            string genreName = Console.ReadLine();
            WritePrompt("Enter instrument: ");
            string instrument = Console.ReadLine();
            WritePrompt("Enter notes (optional): ");
            string notes = Console.ReadLine();
            WritePrompt("Enter sheet music (optional): ");
            string sheet = Console.ReadLine();
            WritePrompt("Enter difficulty (1. Easy, 2. Medium, 3. Hard): ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string difficultyInput = Console.ReadLine();
            Difficulty difficulty;
            switch (difficultyInput)
            {
                case "1":
                    difficulty = Difficulty.Easy;
                    break;
                case "2":
                    difficulty = Difficulty.Medium;
                    break;
                case "3":
                    difficulty = Difficulty.Hard;
                    break;
                default:
                    WriteWarning("Invalid difficulty. Defaulting to Easy.");
                    difficulty = Difficulty.Easy;
                    break;
            }
            _dataRepository.AddSongWithArtistAndGenre(title, artistName, genreName, instrument, notes, sheet, difficulty);
            WriteSuccess("Song added successfully!");
            WritePrompt("Press any key to return to the main menu...");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.ReadKey();
        }

        private void UpdateSong()
        {
            MenuColor();
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("              Song Manager              ");
            Console.WriteLine("              Update Song               ");
            Console.WriteLine("========================================");
            Console.WriteLine("Enter the Id of the song you want to update: ");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int songId))
            {
                WriteError("Invalid Id. Please enter a number.");
                return;
            }
            var existingSong = _dataRepository.SearchSongsById(songId);
            if (existingSong == null)
            {
                WriteError("Song not found!");
                WritePrompt("Press any key to return to the main menu...");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.ReadKey();
                return;
            }
            else
            {
                WritePrompt("Enter Song Title:");
                string newTitle = Console.ReadLine();
                WritePrompt("Enter artist name: ");
                string newArtistName = Console.ReadLine();
                WritePrompt("Enter genre name: ");
                string newGenreName = Console.ReadLine();
                WritePrompt("Enter instrument: ");
                string newInstrument = Console.ReadLine();
                WritePrompt("Enter notes (optional): ");
                string newNotes = Console.ReadLine();
                WritePrompt("Enter sheet music (optional): ");
                string newSheet = Console.ReadLine();
                WritePrompt("Enter difficulty (1. Easy, 2. Medium, 3. Hard): ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                string difficultyInput = Console.ReadLine();
                Difficulty newDifficulty;
                switch (difficultyInput)
                {
                    case "1":
                        newDifficulty = Difficulty.Easy;
                        break;
                    case "2":
                        newDifficulty = Difficulty.Medium;
                        break;
                    case "3":
                        newDifficulty = Difficulty.Hard;
                        break;
                    default:
                        WriteWarning("Invalid difficulty. Defaulting to Easy.");
                        newDifficulty = Difficulty.Easy;
                        break;
                }
                _dataRepository.UpdateSong(songId, newTitle, newArtistName, newGenreName, newNotes, newSheet, newDifficulty);
                WriteSuccess("Song updated successfully!");
                WritePrompt("Press any key to return to the main menu...");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.ReadKey();
            }

        }

        private void SearchOptions()
        {
            MenuColor();
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("              Song Manager              ");
            Console.WriteLine("                 Search                 ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Search by Title");
            Console.WriteLine("2. Search by Id");
            Console.WriteLine("3. Search by Artist");
            Console.WriteLine("4. Search by Genre");
            Console.WriteLine("5. Back to Main Menu");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    SearchByTitle();
                    break;
                case "2":
                    SearchById();
                    break;
                case "3":
                    SearchByArtist();
                    break;

                case "4":
                    SearchByGenre();
                    break;
                case "5":
                    return;
                default:
                    WriteError("Invalid option. Please try again.");
                    break;
            }
        }
        private void SearchByTitle()
        {
            Console.Clear();
            MenuColor();
            Console.WriteLine("========================================");
            Console.WriteLine("              Song Manager              ");
            Console.WriteLine("             Search by Title            ");
            Console.WriteLine("========================================");
            WritePrompt("Enter song title to search: ");
            string searchTitle = Console.ReadLine();
            var results = _dataRepository.SearchSongsByTitle(searchTitle);
            MenuColor();
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("              Song Manager              ");
            Console.WriteLine("                Results                 ");
            Console.WriteLine("========================================");
            if (results.Count == 0)
            {
                WriteError("No songs found with the given title.");
                WritePrompt("Press any key to return to the search menu...");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.ReadKey();
                return;
            }
            else
            {
                OutputColor();
                foreach (var song in results)
                {
                    int artistId = song.ArtistId;
                    int genreId = song.GenreId;
                    var artist = _dataRepository.GetArtistByID(artistId);
                    var genre = _dataRepository.GetGenreByID(genreId);
                    Console.WriteLine($"Title: {song.Title}");
                    Console.WriteLine($"Artist: {artist?.Name}");
                    Console.WriteLine($"Genre: {genre?.Name}");
                    Console.WriteLine($"Instrument: {song.Instrument}");
                    Console.WriteLine($"Difficulty: {song.Difficulty}");
                    Console.WriteLine("\nPress any key to return to the search menu...");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.ReadKey();

                }
            }

        }
        private void SearchById()
        {
            MenuColor();
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("              Song Manager              ");
            Console.WriteLine("               Search by Id             ");
            Console.WriteLine("========================================");
            WritePrompt("Enter song Id to search: ");
            string searchId = Console.ReadLine();
            if (!int.TryParse(searchId, out int songId))
            {
                WriteError("Invalid Id. Please enter a number.");
                return;
            }
            else
            {
                var song = _dataRepository.SearchSongsById(songId);
                MenuColor();
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("              Song Manager              ");
                Console.WriteLine("                Results                 ");
                Console.WriteLine("========================================");
                if (song == null)
                {
                    WriteError("No song found with the given Id.");
                    WritePrompt("Press any key to return to the search menu...");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.ReadKey();
                    return;
                }
                else
                {
                    OutputColor();
                    int artistId = song.ArtistId;
                    int genreId = song.GenreId;
                    var artist = _dataRepository.GetArtistByID(artistId);
                    var genre = _dataRepository.GetGenreByID(genreId);
                    Console.WriteLine($"Title: {song.Title}");
                    Console.WriteLine($"Artist: {artist?.Name}");
                    Console.WriteLine($"Genre: {genre?.Name}");
                    Console.WriteLine($"Instrument: {song.Instrument}");
                    Console.WriteLine($"Difficulty: {song.Difficulty}");
                    WritePrompt("\nPress any key to return to the search menu...");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.ReadKey();
                }
            }
        }
        private void SearchByArtist()
        {
            MenuColor();
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("              Song Manager              ");
            Console.WriteLine("             Search by Artist           ");
            Console.WriteLine("========================================");
            WritePrompt("Enter artist name to search: ");
            string searchArtist = Console.ReadLine();
            var results = _dataRepository.SearchSongsByArtist(searchArtist);
            MenuColor();
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("              Song Manager              ");
            Console.WriteLine("                Results                 ");
            Console.WriteLine("========================================");
            if (results.Count == 0)
            {
                WriteError("No songs found with the given artist.");
                WritePrompt("Press any key to return to the search menu...");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.ReadKey();
                return;
            }
            else
            {
                OutputColor();
                foreach (var song in results)
                {
                    int artistId = song.ArtistId;
                    int genreId = song.GenreId;
                    var artist = _dataRepository.GetArtistByID(artistId);
                    var genre = _dataRepository.GetGenreByID(genreId);
                    Console.WriteLine($"Title: {song.Title}");
                    Console.WriteLine($"Artist: {artist?.Name}");
                    Console.WriteLine($"Genre: {genre?.Name}");
                    Console.WriteLine($"Instrument: {song.Instrument}");
                    Console.WriteLine($"Difficulty: {song.Difficulty}");
                    Console.WriteLine("----------------------------------------");
                }
                WritePrompt("\nPress any key to return to the search menu...");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.ReadKey();
            }
        }
        private void SearchByGenre()
        {
            Console.Clear();
            MenuColor();
            Console.WriteLine("========================================");
            Console.WriteLine("              Song Manager              ");
            Console.WriteLine("             Search by Genre            ");
            Console.WriteLine("========================================");
            WritePrompt("Enter genre name to search: ");
            string searchGenre = Console.ReadLine();
            var results = _dataRepository.SearchSongsByGenre(searchGenre);
            Console.Clear();
            MenuColor();
            Console.WriteLine("========================================");
            Console.WriteLine("              Song Manager              ");
            Console.WriteLine("                Results                 ");
            Console.WriteLine("========================================");
            if (results.Count == 0)
            {
                WriteError("No songs found with the given genre.");
                WritePrompt("Press any key to return to the search menu...");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.ReadKey();
                return;
            }
            else
            {
                OutputColor();
                foreach (var song in results)
                {
                    int artistId = song.ArtistId;
                    int genreId = song.GenreId;
                    var artist = _dataRepository.GetArtistByID(artistId);
                    var genre = _dataRepository.GetGenreByID(genreId);
                    Console.WriteLine($"Title: {song.Title}");
                    Console.WriteLine($"Artist: {artist?.Name}");
                    Console.WriteLine($"Genre: {genre?.Name}");
                    Console.WriteLine($"Instrument: {song.Instrument}");
                    Console.WriteLine($"Difficulty: {song.Difficulty}");
                    Console.WriteLine("----------------------------------------");

                }
                WritePrompt("\nPress any key to return to the search menu...");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.ReadKey();
            }
        }
        private void DeleteSong()
        {
            MenuColor();
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("              Song Manager              ");
            Console.WriteLine("               Delete Song              ");
            Console.WriteLine("========================================");
            WritePrompt("Enter the Id of the song you want to delete: ");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int songId))
            {
                WriteError("Invalid Id. Please enter a number.");
                return;
            }
            var existingSong = _dataRepository.SearchSongsById(songId);
            if (existingSong == null)
            {
                WriteError("Song not found!");
                WritePrompt("Press any key to return to the main menu...");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.ReadKey();
                return;
            }
            else
            {
                _dataRepository.DeleteSong(songId);
                WriteSuccess("Song deleted successfully!");
                WritePrompt("Press any key to return to the main menu...");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.ReadKey();
            }
        }
        private void ListAllSongs()
        {
            MenuColor();
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("              Song Manager              ");
            Console.WriteLine("             List All Songs             ");
            Console.WriteLine("========================================");
            var allSongs = _dataRepository.Songs;
            if (allSongs.Count == 0)
            {
                WriteError("No songs available.");
                WritePrompt("Press any key to return to the main menu...");
                Console.ForegroundColor = ConsoleColor.DarkGray ;
                Console.ReadKey();
                return;
            }
            else
            {
                foreach (var song in allSongs)
                {
                    OutputColor();
                    int artistId = song.ArtistId;
                    int genreId = song.GenreId;
                    var artist = _dataRepository.GetArtistByID(artistId);
                    var genre = _dataRepository.GetGenreByID(genreId);
                    Console.WriteLine($"Title: {song.Title}");
                    Console.WriteLine($"Artist: {artist?.Name}");
                    Console.WriteLine($"Genre: {genre?.Name}");
                    Console.WriteLine($"Instrument: {song.Instrument}");
                    Console.WriteLine($"Difficulty: {song.Difficulty}");
                    Console.WriteLine("----------------------------------------");
                }
                WritePrompt("\nPress any key to return to the main menu...");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.ReadKey();
            }
        }
        private void RandomSongs()
        {
            var random = new Random();
            var randomSongs = _dataRepository.Songs.OrderBy(x => random.Next()).Take(3).ToList();
            
            if (randomSongs.Count == 0)
            {
                return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("");
                Console.WriteLine("Random Songs:");
                Console.WriteLine("----------------------------------------");
                OutputColor();
                foreach (var song in randomSongs)
                {
                    int artistId = song.ArtistId;
                    int genreId = song.GenreId;
                    var artist = _dataRepository.GetArtistByID(artistId);
                    var genre = _dataRepository.GetGenreByID(genreId); 
                    Console.WriteLine("");
                    Console.WriteLine($"Title: {song.Title}");
                    Console.WriteLine($"Artist: {artist?.Name}");
                    Console.WriteLine($"Genre: {genre?.Name}");
                    Console.WriteLine($"Instrument: {song.Instrument}");
                    Console.WriteLine($"Difficulty: {song.Difficulty}");
                    Console.WriteLine("----------------------------------------");
                }

            }
        }
        private void WritePrompt(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(text);
            Console.ResetColor();
        }

        private void WriteError(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        private void WriteSuccess(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        private void MenuColor()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
        }
        private void OutputColor()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }   
        private void WriteWarning(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
