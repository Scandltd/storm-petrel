namespace Test.Integration.WebApi
{
    public class TestMiddleware(RequestDelegate next)
    {
        public Task Invoke(HttpContext context)
        {
            context.Response.Headers["TestHeader"] = "Correct header";
            return next(context);
        }
    }
}
