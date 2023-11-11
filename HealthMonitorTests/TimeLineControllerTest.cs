using Controllers;
using Days;
using RadzenBlazorDemos;
using Xunit;
using Xunit.Abstractions;
using System.Globalization;

namespace Test;

    public class TimeLineControllerTest
    {
            private readonly ITestOutputHelper output;
            Controllers.TimeLineController controller = new Controllers.TimeLineController();            

            private List<Day> allTestDays = new List<Day>(); 

            public TimeLineControllerTest(ITestOutputHelper output)
            {
                this.output = output;                                  
                var day1 = new Day
                {
                    Date = new DateTime(2023, 10, 29),
                    Drug = null,
                    City = "Munich",
                    QualityOfSleep = 1,
                    HealthStatus = 1,
                    Sport = 0,
                    Merkmal = 1,
                    Alcohol = 0,
                    AirPressure = 1013,
                    Comment = "This is a sample comment",
                    Food = "Pasta"
                };
                var day2 = new Day
                {
                    Date = new DateTime(2023, 10, 28),
                    Drug = null,
                    City = "Munich",
                    QualityOfSleep = 2,
                    HealthStatus = 1,
                    Sport = 0,
                    Merkmal = 1,
                    Alcohol = 0,
                    AirPressure = 1013,
                    Comment = "This is a sample comment",
                    Food = "Pasta"
                };
                var day3 = new Day
                {
                    Date = new DateTime(2023, 10, 27),
                    Drug = null,
                    City = "Munich",
                    QualityOfSleep = 8,
                    HealthStatus = 7,
                    Sport = 6,
                    Merkmal = 1,
                    Alcohol = 0,
                    AirPressure = 1013,
                    Comment = "This is a sample comment",
                    Food = "Pasta"
                };
                var day4 = new Day
                {
                    Date = new DateTime(2023, 10, 26),
                    Drug = "Paracetamol",
                    City = "Munich",
                    QualityOfSleep = 3,
                    HealthStatus = 2,
                    Sport = 0,
                    Merkmal = 1,
                    Alcohol = 0,
                    AirPressure = 1013,
                    Comment = "This is a sample comment",
                    Food = "Pasta"
                };
                var day5 = new Day
                {
                    Date = new DateTime(2023, 10, 25),
                    Drug = "Paracetamol",
                    City = "Munich",
                    QualityOfSleep = 2,
                    HealthStatus = 2,
                    Sport = 0,
                    Merkmal = 1,
                    Alcohol = 0,
                    AirPressure = 1013,
                    Comment = "This is a sample comment",
                    Food = "Pasta"
                };
                var day6 = new Day
                {
                    Date = new DateTime(2023, 09, 14),
                    Drug = "Paracetamol",
                    City = "Munich",
                    QualityOfSleep = 1,
                    HealthStatus = 1,
                    Sport = 0,
                    Merkmal = 1,
                    Alcohol = 0,
                    AirPressure = 1013,
                    Comment = "This is a sample comment",
                    Food = "Pasta"
                };                
                allTestDays.Add(day1);
                allTestDays.Add(day2);                                                                                                 
                allTestDays.Add(day3);
                allTestDays.Add(day4);
                allTestDays.Add(day5);
                allTestDays.Add(day6);
                controller.AllDays = allTestDays;                                        
            }

        [Fact]
        public void CanGetAllDaysForTimeLineOctober()
        {
            IList<DailyHealthForTimeLine> expectedDays = new List<DailyHealthForTimeLine>(); 
            IList<DailyHealthForTimeLine> daysForOctober = new List<DailyHealthForTimeLine>(); 

            foreach(var day in allTestDays)
            {
                var testDay = new DailyHealthForTimeLine();
                testDay.Date = day.Date.ToString("yyyy,MM,dd");
                testDay.Health = day.HealthStatus;
                if(day.Date.Month != 9)
                {
                    expectedDays.Add(testDay);
                }
            }
            expectedDays = expectedDays
                .OrderByDescending(day => day.Date != null 
                    ? DateTime.ParseExact(day.Date, "yyyy,MM,dd", CultureInfo.InvariantCulture) 
                    : DateTime.MinValue)
                .ToList();

            daysForOctober = controller.GetAllDaysForTimeLine(10);       

            for (int i = 0; i < expectedDays.Count; i++)
            {
                Assert.Equal(expectedDays[i].Date, daysForOctober[i].Date);
                Assert.Equal(expectedDays[i].Health, daysForOctober[i].Health);
            }
        }
        [Fact]
        public void CanGetAllDaysForTimeLineSeptember()
        {
            IList<DailyHealthForTimeLine> expectedDays = new List<DailyHealthForTimeLine>(); 
            IList<DailyHealthForTimeLine> daysForSeptember = new List<DailyHealthForTimeLine>(); 

            foreach(var day in allTestDays)
            {
                var testDay = new DailyHealthForTimeLine();
                testDay.Date = day.Date.ToString("yyyy,MM,dd");
                testDay.Health = day.HealthStatus;
                if(day.Date.Month != 10)
                {
                    expectedDays.Add(testDay);
                }
            }
            expectedDays = expectedDays
                .OrderByDescending(day => day.Date != null 
                    ? DateTime.ParseExact(day.Date, "yyyy,MM,dd", CultureInfo.InvariantCulture) 
                    : DateTime.MinValue)
                .ToList();

            daysForSeptember = controller.GetAllDaysForTimeLine(9);       

            for (int i = 0; i < expectedDays.Count; i++)
            {
                Assert.Equal(expectedDays[i].Date, daysForSeptember[i].Date);
                Assert.Equal(expectedDays[i].Health, daysForSeptember[i].Health);
            }
        }                     
    }