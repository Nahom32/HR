namespace Human_Resources.Middlewares
{
    public class RoleBasedMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _accessor;
        public RoleBasedMiddleWare(RequestDelegate next, IHttpContextAccessor accessor)
        {
            _next = next;
            _accessor = accessor;

        }
        public async Task Invoke(HttpContext context)
        {
            // Check if the user is authenticated and the requested URL is not the login URL
            if (context.User.Identity.IsAuthenticated)
            {
                // Redirect to the login page
                if (!_accessor.HttpContext.User.IsInRole("Admin"))
                {
                    context.Response.Redirect("/Attendance/Index");
                    return;
                }
            }

            // Call the next middleware
            await _next(context);
        }

    }
}
