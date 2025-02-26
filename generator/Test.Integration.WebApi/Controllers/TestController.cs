using Microsoft.AspNetCore.Mvc;

namespace Test.Integration.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("action")]
    public TestViewModel Action(string value)
    {
        var headers = HttpContext.Response.Headers;
        headers["NewHeader"] = value;
        return new TestViewModel(100, "Correct Text"); ;
    }

    [HttpGet]
    public TestViewModel Get()
    {
        return new TestViewModel(100, "Correct Text");
    }
}

public record TestViewModel(int Id, string Text);
