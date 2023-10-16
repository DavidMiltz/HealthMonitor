using Controllers;
using Days;
using Xunit;
using System.IO;
using Xunit.Abstractions;

namespace Test;

    public class ControllerTest : IDisposable
    {
            private List<Day> daysFromDisk = new();
            Controllers.DaysController controller = new Controllers.DaysController();
            public string TestFolderPath { get; }
            public string TestFolderPath2 { get; }
            private readonly ITestOutputHelper output;                     

            public ControllerTest(ITestOutputHelper output)
            {
                this.output = output;                
                List<Day> daysFromDisk = new List<Day>();
                TestFolderPath = Path.Combine(Path.GetTempPath(), "TestFolder\\");
                TestFolderPath2 = Path.Combine(Path.GetTempPath(), "TestFolder2\\");
                if (!Directory.Exists(TestFolderPath))
                {
                    Directory.CreateDirectory(TestFolderPath);
                }
                if (!Directory.Exists(TestFolderPath2))
                {
                    Directory.CreateDirectory(TestFolderPath2);
                }                           

                for (int i = 1; i <= 5; i++)
                {
                    Day day = new Day();
                    controller.SaveDay( day, DateTime.Now.AddDays(-i), TestFolderPath );
                }
            }

        [Fact]
        public void CanSaveOneDay()
        {
            Day day = new Day();
            Day day2 = new Day();
            controller.SaveDay( day, DateTime.Now, TestFolderPath2 );
            controller.SaveDay( day2, DateTime.Now.AddDays(+1), TestFolderPath2 );
            int fileCount = Directory.GetFiles(TestFolderPath2).Length;
        
            Assert.Equal(2, fileCount);
        }   
        [Fact]             
        public void CanLoadAllDays()
        {
            daysFromDisk = controller.LoadAllDays(TestFolderPath);
        
            Assert.Equal(5, daysFromDisk.Count);          
        }


        public void Dispose()
        {
            if (Directory.Exists(TestFolderPath))
            {
                Directory.Delete(TestFolderPath, true);
            }
            if (Directory.Exists(TestFolderPath2))
            {
                Directory.Delete(TestFolderPath2, true);
            }            
        }
    }