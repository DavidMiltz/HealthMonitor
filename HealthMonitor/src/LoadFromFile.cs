using System.Text.Json;
using Days;

namespace Persistence
{
    public class LoadFromFile {
        public List<Day> ObjectsFromFile()
        {
            var path = "DataBase\\"; 
            List<Day> daysFromDisk = new List<Day>();
            string[] files = Directory.GetFiles(path, "*.json");
            foreach (var file in files)
            {
                string jsonString = File.ReadAllText(file);
                Day day = JsonSerializer.Deserialize<Day>(jsonString)!;
                daysFromDisk.Add(day);
            }
            return daysFromDisk; 
        }
    }
}
