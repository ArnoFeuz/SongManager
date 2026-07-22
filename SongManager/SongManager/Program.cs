using System;
using System.Data;

namespace SongManager
    {
    internal class Program
    {
        static void Main(string[] args)
        {
            DataRepository repository = JsonStorage.LoadDataFromJsonFile("songs.json");
            ConsoleUI ui = new ConsoleUI(repository);
            ui.RunMenu();
        }
    }
}