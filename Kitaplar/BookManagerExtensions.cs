
using Microsoft.EntityFrameworkCore;
using X.PagedList.Web.Common;
using Microsoft.AspNetCore.Identity;

namespace Kitaplar
{
    public static class BookManagerExtensions
    {
        public static PagedListRenderOptions PagedListOptions => new PagedListRenderOptions
        {
            ActiveLiElementClass = "active",
            LiElementClasses = new[] { "page-item" },
            PageClasses = new[] { "page-link" }
        };

        public static IApplicationBuilder UseKitaplar(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            using var userManager=scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            using var RoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            var configuration=scope.ServiceProvider.GetRequiredService<IConfiguration>();
            context.Database.Migrate();

            new[]
            {
                new Role{Name="Administrators"},
                new Role{Name="BookAdministrators"},
                new Role{Name="Members"},
            }
            .ToList()
            .ForEach(role =>
            {
                if(!RoleManager.RoleExistsAsync(role.Name).Result)

                {
                  var result=RoleManager.CreateAsync(role).Result;
                }
            });
            var user = new User
            {
                FirstName = configuration.GetValue<string>("DefaultUser:FirstName"),
                LastName = configuration.GetValue<string>("DefaultUser:LastName"),
                UserName = configuration.GetValue<string>("DefaultUser:Email"),
                Email = configuration.GetValue<string>("DefaultUser:Email"),
                EmailConfirmed = true
            };
             
                userManager.CreateAsync(user,configuration.GetValue<string>("DefaultUser:Password")).Wait();
            
                userManager.AddToRoleAsync(user, "Administrators").Wait();

           return app ;
               
           

           
        }
    }
}
