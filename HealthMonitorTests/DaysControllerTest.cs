using Controllers;
using Days;
using RadzenBlazorDemos;
using Xunit;
using Xunit.Abstractions;

namespace Test;

    public class DaysControllerTest
    {
            private readonly ITestOutputHelper output;
            Controllers.DaysController controller = new Controllers.DaysController();            

            private List<Day> allTestDays = new List<Day>(); 
            private List<Day> daysWithLowHealthTest = new List<Day>(); 
            private DateTime thirtyDaysAgoTest = new DateTime(2023, 09, 29);
            private DateTime currentDate = new DateTime(2023, 10, 29);

            public DaysControllerTest(ITestOutputHelper output)
            {
                this.output = output;                                  
                var day1 = new Day
                {
                    Date = currentDate,
                    Drug = null,
                    City = "Munich",
                    QualityOfSleep = 1,
                    HealthStatus = 1,
                    Sport = 0,
                    Sex = 1,
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
                    Sex = 1,
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
                    Sex = 1,
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
                    Sex = 1,
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
                    Sex = 1,
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
                    Sex = 1,
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
                controller.allDays = allTestDays;
                daysWithLowHealthTest.Add(day1);
                daysWithLowHealthTest.Add(day2);                                                                                                 
                daysWithLowHealthTest.Add(day4);
                daysWithLowHealthTest.Add(day5);
                daysWithLowHealthTest.Add(day6);
                controller.daysWithLowHealth = daysWithLowHealthTest;
                controller.thirtyDaysAgo = thirtyDaysAgoTest;                                              
            }

        [Fact]
        public void CanGetDaysWithLowHealth()
        {
            var expectedDays = allTestDays.Where(day => day.HealthStatus < 5).ToList();

            var actualDays = controller.GetDaysWithLowHealth();

            Assert.Equal(expectedDays, actualDays);
        } 
  
        [Fact]
        public void CanGetDaysBeforeLowHealth()
        {
            var expectedDays = new List<Day>
            {
                allTestDays.ElementAt(0), 
                allTestDays.ElementAt(2), 
                allTestDays.ElementAt(3),  
                allTestDays.ElementAt(4)  
            };

            var actualDays = controller.GetDaysBeforeLowHealth();

            Assert.Equal(expectedDays, actualDays);
        }

        [Fact]
        public void CanGetDaysWithin30Days()
        {
            var expectedDays = new List<Day>
            {
                allTestDays.ElementAt(0), 
                allTestDays.ElementAt(1), 
                allTestDays.ElementAt(3), 
                allTestDays.ElementAt(4)  
            };

            var actualDays = controller.GetDaysWithIn30Days();

            Assert.Equal(expectedDays, actualDays);
        }  

        [Fact]
        public void CanGetDaysWithAttribute()
        {
            Func<Day, bool> condition = day => day.Alcohol > 0;

            var expectedDays = allTestDays.Where(condition).ToList();

            var actualDays = controller.GetDaysWithAttribute(condition);

            Assert.Equal(expectedDays, actualDays);
        }        

        [Fact]
        public void CanGetDaysSinceLastPainkiller()
        { 
            Assert.Equal(3, controller.DaysSinceLastPainkiller(currentDate));
        }  

        [Fact]        
        public void CanPrintPotentialTriggers()
        {
           var expectedItems = new List<string>
            {
                "You had a bad sleep on 8 days with low health or on the day before.",
                "You were sexual active on 6 days with low health or on the day before.",
                "You did sport on 0 days with low health or on the day before.",
                "You drank alcohol on 0 days with low health or on the day before."
            };

            var actualItems = controller.printPotentialTriggers();

            Assert.Equal(expectedItems, actualItems);
        }

        [Fact]
        public void CanGetDaysWithLowHealthForScheduler()
        {
            var expectedList = new List<DailyHealthStatus>();
            foreach (var day in allTestDays)
            {
                if (day.HealthStatus < 5)
                {
                    expectedList.Add(new DailyHealthStatus
                    {
                        Start = day.Date,
                        End = day.Date,
                        Text = "Low health: " + day.Comment
                    });
                }
            }

            var actualList = controller.GetDaysWithLowHealthForScheduler();

            Assert.Equal(expectedList.Count, actualList.Count);

            for (int i = 0; i < expectedList.Count; i++)
            {
                Assert.Equal(expectedList[i].Start, actualList[i].Start);
                Assert.Equal(expectedList[i].End, actualList[i].End);
                Assert.Equal(expectedList[i].Text, actualList[i].Text);
            }
        }                                                  
    }