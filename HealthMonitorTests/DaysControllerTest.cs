using Controllers;
using Days;
using Xunit;
using Xunit.Abstractions;

namespace Test;

    public class DaysControllerTest
    {
            private readonly ITestOutputHelper output;
            Controllers.DaysController controller = new Controllers.DaysController();            

            private List<Day> allTestDays = new List<Day>(); 
            private List<Day> daysWithLowHealthTest = new List<Day>(); 

            public DaysControllerTest(ITestOutputHelper output)
            {
                this.output = output;                                  
                var day1 = new Day
                {
                    Date = DateTime.Now,
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
                    Date = DateTime.Now.AddDays(-1),
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
                    Date = DateTime.Now.AddDays(-2),
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
                    Date = DateTime.Now.AddDays(-3),
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
                    Date = DateTime.Now.AddDays(-4),
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
                    Date = DateTime.Now.AddDays(-31),
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
            Assert.Equal(2, controller.DaysSinceLastPainkiller());
        }  

        [Fact]        
        public void CanPrintPotentialTriggers()
        {
           var expectedItems = new List<string>
            {
                "You had a bad sleep on 9 days with low health or on the day before.",
                "You were sexual active on 7 days with low health or on the day before.",
                "You did sport on 1 days with low health or on the day before.",
                "You drank alcohol on 0 days with low health or on the day before."
            };

            var actualItems = controller.printPotentialTriggers();

            Assert.Equal(expectedItems, actualItems);
        }                     
        
        // [Fact]
        // public void CanGetDaysWithAttribute()
        // {
        //     Func<Day, bool> hasDrugs = day => day.Drug != null && (DateTime.Now.AddDays(-30) - day.Date).TotalDays <= 0;
        //     var expectedDays = new List<Day>
        //     {
        //         allTestDays.ElementAt(0), 
        //         allTestDays.ElementAt(1), 
        //         allTestDays.ElementAt(3), 
        //         allTestDays.ElementAt(4)  
        //     };

        //     var actualDays = controller.GetDaysWithAttribute(hasDrugs);

        //     Assert.Equal(expectedDays, actualDays);
        // }                   
    }