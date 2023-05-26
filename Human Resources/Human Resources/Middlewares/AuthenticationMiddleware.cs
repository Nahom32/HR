
using Human_Resources.Models;
using Microsoft.AspNetCore.Identity;

namespace Human_Resources.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
            

        }

        public async Task Invoke(HttpContext context)
        {
            // Check if the user is authenticated and the requested URL is not the login URL
            if (!context.User.Identity.IsAuthenticated && context.Request.Path != "/Account/Login")
            {
                // Redirect to the login page
                context.Response.Redirect("/Account/Login");
                return;
            }
            
            // Call the next middleware
            await _next(context);
        }
    }

}
