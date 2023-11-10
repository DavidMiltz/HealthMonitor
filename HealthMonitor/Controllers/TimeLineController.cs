using Days;
using RadzenBlazorDemos;

namespace Controllers
{
    public class TimeLineController
    {
        public List<Day> AllDays { get; set; }

        public TimeLineController()
        {
            DataController dataController = new DataController();
            AllDays = dataController.LoadAllDays("DataBase\\");
        }

        public IList<DailyHealthForTimeLine> GetAllDaysForTimeLine(int month)
        {
            IList<DailyHealthForTimeLine> allDaysForTimeLine;
            allDaysForTimeLine = AllDays
            .Where(day => day.Date.Month == month) 
            .Select(day => new DailyHealthForTimeLine { Date = day.Date.ToString("yyyy,MM,dd"), Health = day.HealthStatus, Mental = day.MentalStatus })
            .ToList();
            return allDaysForTimeLine;
        }
    }
}