using System.Text.Json;
using Items;

namespace Persistence
{
    public class LoadFromFile {
        public List<TodoItem> ObjectsFromFile()
        {
            var path = "DataBase\\"; 
            List<TodoItem> todosFromDisk = new List<TodoItem>();
            string[] files = Directory.GetFiles(path, "*.json");
            foreach (var file in files)
            {
                string jsonString = File.ReadAllText(file);
                Console.WriteLine(jsonString);
                TodoItem todoItem = JsonSerializer.Deserialize<TodoItem>(jsonString)!;
                todosFromDisk.Add(todoItem);
            }
            return todosFromDisk; 
        }
    }
}
