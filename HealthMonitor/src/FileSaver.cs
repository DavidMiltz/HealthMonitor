using System.Text.Json;

namespace Persistence
{
    public class FileSaver {
        public bool SaveDayToFileOnDisk(object _Object, DateTime _FileName)
        {
            var path = "DataBase\\"; 
            string fileName = _FileName.ToString("yyyy-dd-M") + ".json"; 
            string jsonString = JsonSerializer.Serialize(_Object);
            File.WriteAllText(path + fileName, jsonString);
            return true;
        }
    }
}
