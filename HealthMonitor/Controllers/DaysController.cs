using Days;
using FileManagement;

namespace Controllers
{
    public class DaysController {

        DaysRepository repository = new DaysRepository();
        public List<Day> LoadAllDays(string DataBaseFolder)
        {
            return repository.LoadAllDays(DataBaseFolder); 
        }

        public bool SaveDay(object _Object, DateTime _FileName, string DataBaseFolder)
        {
            if(repository.SaveDay(_Object, _FileName, DataBaseFolder))
            {
                return true;
            }
            return false;
        }        
    }
}