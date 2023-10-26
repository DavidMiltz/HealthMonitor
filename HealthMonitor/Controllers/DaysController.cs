using Days;
using RadzenBlazorDemos;
using System.Text.RegularExpressions;

namespace Controllers
{
    public class DaysController
    {
        private readonly DataController dataController;
        private readonly DateTime thirtyDaysAgo;
        private readonly List<Day> allDays;
        private readonly List<Day> daysWithLowHealth;
        private readonly List<Day> daysBeforeLowHealth;

        public DaysController()
        {
            dataController = new DataController();
            thirtyDaysAgo = DateTime.Now.AddDays(-30);
            allDays = dataController.LoadAllDays("DataBase\\");
            daysWithLowHealth = GetDaysWithLowHealth();
            daysBeforeLowHealth = GetDaysBeforeLowHealth();
        }
        public List<Day> GetDaysWithLowHealth()
        {
            return allDays.Where(day => day.HealthStatus < 5).ToList();
        }
        public List<Day> GetDaysBeforeLowHealth()
        {
            Day? previousDay = null;
            List<Day> daysBeforeLowHealth = new();

            foreach (Day day in allDays)
            {
                if (day.HealthStatus < 5 && previousDay != null)
                {
                    daysBeforeLowHealth.Add(previousDay);
                }
                previousDay = day;
            }
            return daysBeforeLowHealth;
        }
        public List<Day> GetDaysWithAttribute(Func<Day, bool> condition)
        {
            List<Day> daysWithAttribute = new();
            daysWithAttribute = daysWithLowHealth.Where(condition).ToList();
            daysWithAttribute.AddRange(daysBeforeLowHealth.Where(condition));
            return daysWithAttribute;
        }

        public List<Day> GetDaysWithIn30Days()
        {
            Func<Day, bool> isWithin30DaysCondition = day => (thirtyDaysAgo - day.Date).TotalDays <= 0;
            return daysWithLowHealth.Where(isWithin30DaysCondition).ToList();
        }
        public IList<DailyHealthStatus> GetDaysWithLowHealthForScheduler()
        {
            IList<DailyHealthStatus> daysWithLowHealthList;
            daysWithLowHealthList = daysWithLowHealth
            .Select(day => new DailyHealthStatus { Start = day.Date, End = day.Date, Text = "Low health: " + day.Comment })
            .ToList();
            return daysWithLowHealthList;
        }
        public int DaysSinceLastPainkiller()
        {
            Func<Day, bool> hasDrugs = day => day.Drug != null && (thirtyDaysAgo - day.Date).TotalDays <= 0;
            var closestDayWithDrugUsage = GetDaysWithAttribute(hasDrugs)
                .OrderBy(day => Math.Abs((day.Date - DateTime.Today).TotalDays))
                .FirstOrDefault();

            return closestDayWithDrugUsage != null ? (int)(DateTime.Today - closestDayWithDrugUsage.Date).TotalDays : 0;
        }
        public List<string> printPotentialTriggers()
        {
            Func<Day, bool> hasAlcohol = day => day.Alcohol > 0;
            Func<Day, bool> hasBadSleep = day => day.QualityOfSleep < 5;
            Func<Day, bool> hasSex = day => day.Sex > 0;
            Func<Day, bool> hasSport = day => day.Sport > 0;
            List<string> items = new List<string>
            {
                "You had a bad sleep on " + GetDaysWithAttribute(hasBadSleep).Count + " days with low health or on the day before.",
                "You did sport on " + GetDaysWithAttribute(hasSport).Count + " days with low health or on the day before.",
                "You were sexual active on " + GetDaysWithAttribute(hasSex).Count + " days with low health or on the day before.",
                "You drank alcohol on " + GetDaysWithAttribute(hasAlcohol).Count + " days with low health or on the day before.",
            };

            var regex = new Regex(@"on (\d+) days");
            var sortedItems = items.OrderByDescending(item => int.Parse(regex.Match(item).Groups[1].Value)).ToList();
            return sortedItems;
        }
    }
}