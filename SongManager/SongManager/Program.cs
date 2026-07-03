using System;
using System.Data;

namespace SongManager
    {
    internal class Program
    {
        static void Main(string[] args)
        {
            DataRepository repository = new DataRepository();
            ConsoleUI ui = new ConsoleUI(repository);
            ui.RunMenu();
        }
    }
}