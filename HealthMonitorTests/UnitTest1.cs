using Persistence;
using Days;

namespace Test;

    public class UnitTest1
    {
            private List<Day> todosFromDisk = new();
            Persistence.LoadFromFile getFiles = new Persistence.LoadFromFile();

        [Fact]
        public void PassingTest()
        {
            todosFromDisk = getFiles.ObjectsFromFile();
        
            Assert.Equal(4, todosFromDisk.Count);
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }