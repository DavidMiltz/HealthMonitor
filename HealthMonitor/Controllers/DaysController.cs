using Days;
using RadzenBlazorDemos;
using System.Text.RegularExpressions;

namespace Controllers
{
    public class DaysController {
        private readonly DataController dataController;
        private readonly DateTime thirtyDaysAgo; 
        private readonly List<Day> allDays;
        private readonly List<Day> daysWithLowHealth;
        private readonly List<Day> daysBeforeLowHealth;
        private readonly List<Day> daysWithBadSleep;
        private readonly List<Day> daysWithSport;
        private readonly List<Day> daysWithSex;
        private readonly List<Day> daysWithAlcohol;
        private readonly Func<Day, bool> isAlcoholCondition = day => day.Alcohol > 0; 
        private readonly Func<Day, bool> isBadSleepCondition = day => day.QualityOfSleep < 5;
        private readonly Func<Day, bool> isDrugCondition; 
        private readonly Func<Day, bool> isSexCondition = day => day.Sex > 0;
        private readonly Func<Day, bool> isSportCondition = day => day.Sport > 0;

        public DaysController()
        {
            dataController = new DataController();
            thirtyDaysAgo = DateTime.Now.AddDays(-30);
            allDays = dataController.LoadAllDays("DataBase\\");
            daysWithLowHealth = GetDaysWithLowHealth();
            daysBeforeLowHealth = GetDaysBeforeLowHealth();
            daysWithAlcohol = GetDaysWithAttribute(isAlcoholCondition);
            daysWithBadSleep = GetDaysWithAttribute(isBadSleepCondition);
            isDrugCondition = day => day.Drug != null && (thirtyDaysAgo - day.Date).TotalDays <= 0;
            daysWithSex = GetDaysWithAttribute(isSexCondition);
            daysWithSport = GetDaysWithAttribute(isSportCondition);
        }
        private List<Day> FilterDaysWithProperty(List<Day> sourceDays, Func<Day, bool> filter)
        {
            return sourceDays.Where(filter).ToList();
        }                     
        public List<Day> GetDaysWithLowHealth()
        {
            return allDays.Where(day => day.HealthStatus < 5).ToList();
        }        
        public List<Day> GetDaysBeforeLowHealth()
        {
            Day? previousDay = null;
            List<Day> daysBeforeLowHealth = new();
            
            foreach(Day day in allDays){
                if(day.HealthStatus < 5 && previousDay != null) {
                    daysBeforeLowHealth.Add(previousDay);
                }
                previousDay = day;
            }            
            return daysBeforeLowHealth; 
        }
        public List<Day> GetDaysWithAttribute(Func<Day, bool> condition)
        {
            List<Day> daysWithAttribute = new();
            daysWithAttribute = FilterDaysWithProperty(daysWithLowHealth, condition);
            daysWithAttribute.AddRange(daysBeforeLowHealth.Where(condition));           
            return daysWithAttribute; 
        } 

        public List<Day> GetDaysWithIn30Days()
        {
            Func<Day, bool> isWithin30DaysCondition = day => (thirtyDaysAgo - day.Date).TotalDays <= 0;          
            return FilterDaysWithProperty(daysWithLowHealth, isWithin30DaysCondition);
        } 
        public IList<DailyHealthStatus> GetDaysWithLowHealthList()
        {
            IList<DailyHealthStatus> daysWithLowHealthList;
            daysWithLowHealthList = daysWithLowHealth
            .Select(day => new DailyHealthStatus { Start = day.Date, End = day.Date, Text = "Low health: " + day.Comment })
            .ToList();         
            return daysWithLowHealthList;
        }  
        public int DaysSinceLastPainkiller()
        {
            var today = DateTime.Today;
            var closestDay = GetDaysWithAttribute(isDrugCondition)
                .OrderBy(day => Math.Abs((day.Date - today).TotalDays))
                .FirstOrDefault();

            return closestDay != null ? (int)(today - closestDay.Date).TotalDays : 0;
        }                                                     
        public List<string> printResultList()
        {
            List<string> items = new List<string>
            {
                "You had a bad sleep on " + daysWithBadSleep.Count + " days with low health or on the day before.",
                "You did sport on " + daysWithSport.Count + " days with low health or on the day before.",
                "You were sexual active on " + daysWithSex.Count + " days with low health or on the day before.",
                "You drank alcohol on " + daysWithAlcohol.Count + " days with low health or on the day before.",
            };

            var regex = new Regex(@"on (\d+) days");
            var sortedItems = items.OrderByDescending(item => int.Parse(regex.Match(item).Groups[1].Value)).ToList();
            return sortedItems;
        }   

    }
}