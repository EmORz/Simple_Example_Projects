namespace Forum.Web.Middlewares
{
    using Forum.Web.Utilities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public class SeedRolesMiddleware
    {
        private readonly RequestDelegate next;

        public SeedRolesMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext httpContext, RoleManager<IdentityRole> roleManager)
        {
            Seeder.SeedRoles(roleManager).Wait();

            return this.next(httpContext);
        }
    }
}