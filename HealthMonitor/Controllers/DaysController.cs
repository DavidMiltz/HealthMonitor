using Days;
using FileManagement;

namespace Controllers
{
    public class DaysController {

        DaysRepository repository = new DaysRepository();
        public List<Day> LoadAllDays()
        {
            return repository.LoadAllDays(); 
        }

        public bool SaveDay(object _Object, DateTime _FileName)
        {
            if(repository.SaveDay(_Object, _FileName))
            {
                return true;
            }
            return false;
        }        
    }
}