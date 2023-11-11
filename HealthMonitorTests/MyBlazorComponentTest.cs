using Bunit;
using HealthMonitor.Components;
using Days;

public class AirpressureSectionTest : TestContext
{
    [Fact]
    public void ShouldRenderComponent()
    {
        // Arrange
        var daysWithLowHealth = new List<Day>
        {
            new Day { AirPressure = 10 },
            new Day { AirPressure = 20 },
            // Add more sample days as needed
        };

        var cut = RenderComponent<HealthMonitor.Components.AirpressureSection>(
            parameters => parameters.Add(p => p.daysWithLowHealth, daysWithLowHealth)
        );

        // Act - No action required for this example

        // Assert

        var averageAirpressure = cut.Find("div"); // Adjust this selector based on the actual markup
        Assert.Contains("15", averageAirpressure.TextContent); // Update with the expected average airpressure
    }
}
