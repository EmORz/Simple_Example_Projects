using Microsoft.AspNetCore.Authorization;

namespace Forum.Web.Attributes.CustomAuthorizeAttributes
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(", ", roles);
        }
    }
}