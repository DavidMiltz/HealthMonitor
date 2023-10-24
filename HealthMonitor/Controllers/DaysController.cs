using Days;
using FileManagement;
using RadzenBlazorDemos;
using System.Text.RegularExpressions;

namespace Controllers
{
    public class DaysController {

        DaysRepository repository;
        DateTime thirtyDaysAgo; 
        List<Day> allDays;
        List<Day> daysWithLowHealth;
        List<Day> daysBeforeLowHealth;
        List<Day> daysWithBadSleep;
        List<Day> daysWithSport;
        List<Day> daysWithSex;
        List<Day> daysWithAlcohol;
        List<Day> daysWithDrugUsage;

        public DaysController()
        {
            repository = new DaysRepository();
            thirtyDaysAgo = DateTime.Now.AddDays(-30);
            allDays = repository.LoadAllDays("DataBase\\");
            daysWithLowHealth = GetDaysWithLowHealth();
            daysBeforeLowHealth = GetDaysBeforeLowHealth();
            daysWithBadSleep = GetDaysWithBadSleep();
            daysWithSport = GetDaysWithSport();
            daysWithSex = GetDaysWithSex();
            daysWithAlcohol = GetDaysWithAlcohol();
            daysWithDrugUsage = GetDaysWithDrugUsage();
        }
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
        public List<Day> GetDaysWithLowHealth()
        {
            List<Day> daysWithLowHealth = new();

            foreach(Day day in allDays){
                if( day.HealthStatus < 5 ) {
                    daysWithLowHealth.Add(day);
                }
            }            
            return daysWithLowHealth; 
        }

        public List<Day> GetDaysBeforeLowHealth()
        {
            Day previousDay = new() {};
            List<Day> daysBeforeLowHealth = new();
            
            foreach(Day day in allDays){
                if( day.HealthStatus < 5 ) {
                    if(previousDay != null){
                        daysBeforeLowHealth.Add(previousDay);
                    }
                }
                previousDay = day;
            }            
            return daysBeforeLowHealth; 
        }    
        public List<Day> GetDaysWithBadSleep()
        {
            List<Day> daysWithBadSleep = new();
            Func<Day, bool> isBadSleepCondition = day => day.QualityOfSleep < 5;  

            daysWithBadSleep = FilterDaysWithProperty(daysWithLowHealth, isBadSleepCondition);
            daysWithBadSleep.AddRange(daysBeforeLowHealth.Where(isBadSleepCondition));           
            return daysWithBadSleep; 
        }
        public List<Day> GetDaysWithSport()
        {
            List<Day> daysWithSport = new();
            Func<Day, bool> isSportCondition = day => day.Sport > 0;  

            daysWithSport = FilterDaysWithProperty(daysWithLowHealth, isSportCondition);
            daysWithSport.AddRange(daysBeforeLowHealth.Where(isSportCondition));           
            return daysWithSport; 
        }
        public List<Day> GetDaysWithSex()
        {
            List<Day> daysWithSex = new();
            Func<Day, bool> isSexCondition = day => day.Sex > 0;   

            daysWithSex = FilterDaysWithProperty(daysWithLowHealth, isSexCondition);
            daysWithSex.AddRange(daysBeforeLowHealth.Where(isSexCondition));           
            return daysWithSex; 
        }
        public List<Day> GetDaysWithAlcohol()
        {
            List<Day> daysWithAlcohol = new();
            Func<Day, bool> isAlcoholCondition = day => day.Alcohol > 0;  

            daysWithAlcohol = FilterDaysWithProperty(daysWithLowHealth, isAlcoholCondition);
            daysWithAlcohol.AddRange(daysBeforeLowHealth.Where(isAlcoholCondition));           
            return daysWithAlcohol; 
        }
        public List<Day> GetDaysWithDrugUsage()
        {
            List<Day> daysWithDrugUsage = new();
            Func<Day, bool> isDrugCondition = day => day.Drug != null && (thirtyDaysAgo - day.Date).TotalDays <= 0; 

            daysWithDrugUsage = FilterDaysWithProperty(daysWithLowHealth, isDrugCondition);
            daysWithDrugUsage.AddRange(daysBeforeLowHealth.Where(isDrugCondition));           
            return daysWithDrugUsage; 
        }
        public List<Day> GetDaysWithFood()
        {
            List<Day> daysWithFood = new();
            Func<Day, bool> isFoodCondition = day => day.Food != null;  

            daysWithFood = FilterDaysWithProperty(daysWithLowHealth, isFoodCondition);
            daysWithFood.AddRange(daysBeforeLowHealth.Where(isFoodCondition));           
            return daysWithFood; 
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
        public int DaysSinceLastPainkiller()
        {
            var today = DateTime.Today;
            var sortedDays = daysWithDrugUsage.OrderBy(d => Math.Abs((d.Date - today).TotalDays)).ToList();
            var closestDay = sortedDays.FirstOrDefault();
            if(closestDay != null)
            {
                TimeSpan timePassed = today - closestDay.Date;
                return timePassed.Days;
            }
            return 0;
        }                                                     
        private List<Day> FilterDaysWithProperty(List<Day> sourceDays, Func<Day, bool> filter)
        {
            return sourceDays.Where(filter).ToList();
        }                     

    }
}