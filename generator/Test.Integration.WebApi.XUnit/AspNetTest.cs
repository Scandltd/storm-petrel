using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Test.Integration.WebApi.Controllers;

namespace Test.Integration.WebApi.XUnit;

public class AspNetTest
{
    private readonly WebApplicationFactory<Program> _application = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddControllers();
            });
        });

    [Fact]
    public void WebApiControllerTest()
    {
        //Arrange
        var actualContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
        var controller = new TestController()
        {
            ControllerContext = actualContext
        };

        //Act
        var actualResult = controller.Action("CorrectHeaderValue");

        //Assert
        actualResult.Should().BeEquivalentTo(new TestViewModel(103, "Incorrect Text"));
        actualContext.HttpContext.Response.Headers["NewHeader"].ToString().Should().BeEquivalentTo("Incorrect header value");
        //Assert Response.Headers via BeEquivalentTo
        actualContext.HttpContext.Response.Headers.ToDictionary(x => x.Key, x => x.Value.ToList())
            .Should().BeEquivalentTo(new Dictionary<string, List<string>>());
    }

    [Fact]
    public void MvcControllerTest()
    {
        //Arrange
        using var controller = new MvcTestController();

        //Act
        var actualResult = controller.File();
        var actualContent = Encoding.UTF8.GetString(actualResult.FileContents);

        //Assert
        actualContent.Should().BeEquivalentTo("Incorrect content");
        actualResult.ContentType.Should().BeEquivalentTo("application/json");
    }

    [Fact]
    public async Task MiddlewareTest()
    {
        //Arrange
        var actual = false;
        var actualContext = new DefaultHttpContext();
        var testMiddleware = new TestMiddleware(_ =>
        {
            actual = true;
            return Task.CompletedTask;
        });

        //Act
        await testMiddleware.Invoke(actualContext);

        //Assert
        actual.Should().Be(false);
        actualContext.HttpContext.Response.Headers["TestHeader"].ToString().Should().BeEquivalentTo("Incorrect header");
    }

    [Fact]
    public async Task WebApiControllerIntegrationTest()
    {
        //Arrange
        var client = _application.CreateClient();

        //Act
        var response = await client.GetAsync(new Uri("/test", UriKind.Relative));
        var actual = await response.Content.ReadAsStringAsync();

        //Assert
        actual.Should().Be("{\"id\":103,\"text\":\"Incorrect Text\"}");
    }
}
