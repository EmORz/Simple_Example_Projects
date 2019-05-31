namespace Forum.Web.Utilities
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Threading.Tasks;

    public static class Seeder
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            bool OwnerRoleExists = await roleManager.RoleExistsAsync("Owner");
            if (!OwnerRoleExists)
            {
                var ownerRole = new IdentityRole() { Name = "Owner", NormalizedName = "OWNER", ConcurrencyStamp = "0" };
                var result = await roleManager.CreateAsync(ownerRole);
            }


            bool AdminRoleExists = await roleManager.RoleExistsAsync("Administrator");
            if (!AdminRoleExists)
            {
                var adminRole = new IdentityRole() { Name = "Administrator", NormalizedName = "ADMINISTRATOR", ConcurrencyStamp = "0" };
                var result = await roleManager.CreateAsync(adminRole);
            }

            bool UserRoleExists = await roleManager.RoleExistsAsync("User");
            if (!UserRoleExists)
            {
                var userRole = new IdentityRole() { Name = "User", NormalizedName = "USER", ConcurrencyStamp = "1" };
                var result = await roleManager.CreateAsync(userRole);
            }
        }
    }
}