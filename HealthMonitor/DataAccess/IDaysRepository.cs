using System.Text.Json;
using Days;

namespace FileManagement
{
    public interface IDaysRepository {
        public List<Day> LoadAllDays(); 
        public bool SaveDay(object _Object, DateTime _FileName);  
    }
}