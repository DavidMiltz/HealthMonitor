using Days;
using RadzenBlazorDemos;

namespace Controllers
{
    public class TimeLineController
    {
        private readonly DataController dataController;
        private readonly DateTime thirtyDaysAgo;
        public List<Day> allDays;


        public TimeLineController()
        {
            dataController = new DataController();
            allDays = dataController.LoadAllDays("DataBase\\");
        }

        public IList<DailyHealthForTimeLine> GetDaysWithLowHealthForTimeLine()
        {
            IList<DailyHealthForTimeLine> allDaysForTimeLine;
            allDaysForTimeLine = allDays
            .Where(day => day.Date.Month == DateTime.Today.Month) 
            .Select(day => new DailyHealthForTimeLine { Date = day.Date.ToString("yyyy,MM,dd"), Health = day.HealthStatus })
            .ToList();
            return allDaysForTimeLine;
        }
    }
}