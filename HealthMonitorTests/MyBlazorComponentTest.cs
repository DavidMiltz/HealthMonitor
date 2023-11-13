using Bunit;
using HealthMonitor.Components;
using RadzenBlazorDemos;
using Days;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

public class AirpressureSectionTest : TestContext
{
    [Fact]
    public void RendersTimelinePageCorrectly()
    {
        // Arrange
        // using var ctx = new TestContext();

        // // Register the TimeLineController service
        // ctx.Services.AddSingleton<Controllers.TimeLineController>();

        // // Act
        // var cut = ctx.RenderComponent<TimeLine>();

        // // Assert
        // cut.MarkupMatches(@"
        //     <h1 style=""color: #4340D2;"">Health Status Timeline</h1>
        //     <!-- Add more assertions based on your expected HTML structure -->
        // ");

        // // You can also interact with the rendered component and make assertions based on its state
        // // For example, you can trigger an event and assert that the component updates as expected
        // var inputSelect = cut.Find("InputSelect");
        // inputSelect.Change("3"); // Change the selected month to 3 (or any other value)
        // cut.WaitForState(() => cut.Find("h1").InnerHtml.Contains("March"));

        // // Add more assertions based on your specific component logic
    }
}
