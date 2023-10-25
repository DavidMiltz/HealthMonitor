using System.Text.Json;
using Days;

namespace FileManagement
{
    public class DaysRepository : IDaysRepository
    {
        public List<Day> LoadAllDays(string DataBaseFolder)
        {
            List<Day> daysFromDisk = new List<Day>();
            string[] files = Directory.GetFiles(DataBaseFolder, "*.json");
            foreach (var file in files)
            {
                string jsonString = File.ReadAllText(file);
                Day day = JsonSerializer.Deserialize<Day>(jsonString)!;
                daysFromDisk.Add(day);
            }
            return daysFromDisk;
        }

        public bool SaveDay(object _Object, DateTime _FileName, string DataBaseFolder)
        {
            string fileName = _FileName.ToString("yyyy-dd-M") + ".json";
            string jsonString = JsonSerializer.Serialize(_Object);
            File.WriteAllText(DataBaseFolder + fileName, jsonString);
            return true;
        }

    }
}