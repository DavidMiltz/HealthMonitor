using Controllers;
using Days;

namespace Test;

    public class UnitTest1
    {
            private List<Day> daysFromDisk = new();
            Controllers.DaysController controller = new Controllers.DaysController();

        [Fact]
        public void PassingTest()
        {
            daysFromDisk = controller.LoadAllDays();
        
            Assert.Equal(4, daysFromDisk.Count);
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }