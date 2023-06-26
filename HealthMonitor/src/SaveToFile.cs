using System.Text.Json;

namespace Persistence
{
    public class SaveToFile {
        public bool ObjectToFile(object _Object, DateTime _FileName)
        {
            var path = "DataBase\\"; 
            string fileName = _FileName.ToString("yyyy-dd-M--HH-mm-ss") + ".json"; 
            string jsonString = JsonSerializer.Serialize(_Object);
            File.WriteAllText(path + fileName, jsonString);
            return true;
        }
    }
}
