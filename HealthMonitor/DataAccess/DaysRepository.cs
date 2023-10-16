using System.Text.Json;
using Days;

namespace FileManagement
{
    public class DaysRepository : IDaysRepository{
        public List<Day> LoadAllDays()
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

        public bool SaveDay(object _Object, DateTime _FileName)
        {
            var path = "DataBase\\"; 
            string fileName = _FileName.ToString("yyyy-dd-M") + ".json"; 
            string jsonString = JsonSerializer.Serialize(_Object);
            File.WriteAllText(path + fileName, jsonString);
            return true;
        }        
        
    }
}