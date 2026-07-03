using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("              Song Manager              ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Add Song");
                Console.WriteLine("2. Search");
                Console.WriteLine("3. Update Song");
                Console.WriteLine("4. Delete Song");
                Console.WriteLine("5. Exit");
                Console.WriteLine("========================================");
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
                        //UpdateSong();
                        break;
                    case "4":
                        return;
                    case "5":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }

        }

        private void AddSong()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("              Song Manager              ");
            Console.WriteLine("                Add Song                ");
            Console.WriteLine("========================================");
            Console.WriteLine("Enter Song Title:");
            string title = Console.ReadLine();
            Console.Write("Enter artist name: ");
            string artistName = Console.ReadLine();
            Console.Write("Enter genre name: ");
            string genreName = Console.ReadLine();
            Console.Write("Enter instrument: ");
            string instrument = Console.ReadLine();
            Console.Write("Enter notes (optional): ");
            string notes = Console.ReadLine();
            Console.Write("Enter sheet music (optional): ");
            string sheet = Console.ReadLine();
            Console.Write("Enter difficulty (1. Easy, 2. Medium, 3. Hard): ");
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
                    Console.WriteLine("Invalid difficulty. Defaulting to Easy.");
                    difficulty = Difficulty.Easy;
                    break;
            }
            _dataRepository.AddSongWithArtistAndGenre(title, artistName, genreName, instrument, notes, sheet, difficulty);
            Console.WriteLine("Song added successfully!");
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        private void SearchOptions()
        {
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
                    //case "2":
                    //SearchById();
                    break;
                    //case "3":
                    //SearchByArtist();
                    break;

                    //case "4":
                    //SearchByGenre();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
        private void SearchByTitle()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("              Song Manager              ");
            Console.WriteLine("             Search by Title            ");
            Console.WriteLine("========================================");
            Console.Write("Enter song title to search: ");
            string searchTitle = (Console.ReadLine());
            var results = _dataRepository.SearchSongsByTitle(searchTitle);
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("              Song Manager              ");
            Console.WriteLine("                Results                 ");
            Console.WriteLine("========================================");
            if (results.Count == 0)
            {
                Console.WriteLine("No songs found with the given title.");
                Console.WriteLine("Press any key to return to the search menu...");
                Console.ReadKey();
                return;
            }
            else
            {
                foreach (var song in results)
                {
                    int artistId = song.ArtistId;
                    int genreId = song.GenreId;
                    var artist = _dataRepository.GetArtistByID(artistId);
                    //var genre = _dataRepository.GetGenreById(genreId);
                    Console.WriteLine($"Title: {song.Title}");
                    Console.WriteLine($"Artist: {artist?.Name}");
                    //Console.WriteLine($"Genre: {genre?.Name}");
                    Console.WriteLine($"Instrument: {song.Instrument}");
                    Console.WriteLine($"Difficulty: {song.Difficulty}");
                    Console.WriteLine("\nPress any key to return to the search menu...");
                    Console.ReadKey();

                }
            }

        }
    }
}
