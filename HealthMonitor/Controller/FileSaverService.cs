using System.Text.Json;

namespace FileManagement
{
    public class FileSaverService {
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
