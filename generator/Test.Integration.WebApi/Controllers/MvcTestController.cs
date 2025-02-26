using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Test.Integration.WebApi.Controllers;

[Route("[controller]")]
public class MvcTestController : Controller
{
    [HttpGet("file")]
    public FileContentResult File()
    {
        var content = Encoding.UTF8.GetBytes("Correct text");
        return File(content, "text/plain");
    }
}
