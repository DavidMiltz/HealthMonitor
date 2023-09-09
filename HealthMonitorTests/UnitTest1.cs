using FileManagement;
using Days;

namespace Test;

    public class UnitTest1
    {
            private List<Day> daysFromDisk = new();
            FileManagement.LoadFromFile getFiles = new FileManagement.LoadFromFile();

        [Fact]
        public void PassingTest()
        {
            daysFromDisk = getFiles.LoadAllDays();
        
            Assert.Equal(4, todosFromDisk.Count);
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }