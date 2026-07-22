using System.IO;
using System.Text.Json;

namespace SongManager
{
    public static class JsonStorage
    {
        public static void SaveDataToJsonFile(DataRepository dataRepository, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(dataRepository, options);
            File.WriteAllText(filePath, jsonString);
        }
        public static DataRepository LoadDataFromJsonFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new DataRepository();
            }

            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<DataRepository>(jsonString) ?? new DataRepository();
        }
    }
}