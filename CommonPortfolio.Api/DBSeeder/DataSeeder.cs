using CommonPortfolio.Domain.Entity;
using CommonPortfolio.Infrastructure.Context;

namespace CommonPortfolio.Api.DBSeeder
{
    public class DataSeeder
    {
        public static async Task SeedDefaultData(IApplicationBuilder applicationBuilder)
        {

            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDBContext>();
                if (context == null)
                {
                    throw new Exception("Please run migration");
                }

                var RoleAdmin = "admin";
                //Roles
                using (var tx = await context.Database.BeginTransactionAsync())
                {
                    string adminUserEmail = "admin@gmail.com";

                    var adminUser = context.AppUsers.Any(a => a.Email == adminUserEmail);

                    if (!adminUser)
                    {
                        var newAdminUser = new AppUser
                        {
                            UserName = "admin",
                            Name = "Super Admin",
                            Email = adminUserEmail,
                            Contact = "0000000000",
                            Password = BCrypt.Net.BCrypt.HashPassword("admin@123"),
                            Role = RoleAdmin
                        };

                        await context.AppUsers.AddAsync(newAdminUser);
                    }
                    await context.SaveChangesAsync().ConfigureAwait(false);
                    await tx.CommitAsync();
                }
            }
        }
    }
}
