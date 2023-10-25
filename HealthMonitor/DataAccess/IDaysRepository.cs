using Days;

namespace FileManagement
{
    public interface IDaysRepository
    {
        public List<Day> LoadAllDays(string DataBaseFolder);
        public bool SaveDay(object _Object, DateTime _FileName, string DataBaseFolder);
    }
}