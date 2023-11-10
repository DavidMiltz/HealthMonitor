using Days;
using FileManagement;

namespace Controllers
{
    public class DataController
    {
        readonly DaysRepository repository;

        public DataController()
        {
            repository = new DaysRepository();
        }
        public List<Day> LoadAllDays(string DataBaseFolder)
        {
            return repository.LoadAllDays(DataBaseFolder);
        }

        public bool SaveDay(object _Object, DateTime _FileName, string DataBaseFolder)
        {
            return repository.SaveDay(_Object, _FileName, DataBaseFolder);
        }
        public Day LoadDay(string DataBaseFolder, string Date)
        {
            return repository.LoadDay(DataBaseFolder, Date);
        }        
    }
}