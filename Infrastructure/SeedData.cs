using Application.Auth.Models;
using Core.Articles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class SeedData
    {
        public static async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            if (await dbContext.Articles.AnyAsync())
            {
                return;
            }
            var articles = new[]
            {
            new Article {Id = 1, Title = "First Custom Article", Content = "Custom content for the first article.", PublishedDate = DateTime.Now.AddDays(-1) },
            new Article {Id = 2, Title = "Second Custom Article", Content = "Custom content for the second article.", PublishedDate = DateTime.Now.AddHours(-6) },
            new Article { Id = 3,Title = "Third Custom Article", Content = "Custom content for the third article.", PublishedDate = DateTime.Now.AddMinutes(-30) }
            };
            dbContext.Articles.AddRange(articles);

            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // List of users to seed
            var users = new[]
            {
            new User { UserName = "testuser1", Email = "testuser1@example.com" },
            new User { UserName = "testuser2", Email = "testuser2@example.com" }
        };

            foreach (var user in users)
            {
                // Check if user already exists
                if (await userManager.FindByNameAsync(user.UserName) == null)
                {
                    var result = await userManager.CreateAsync(user, "Password123!");

                    if (!result.Succeeded)
                    {
                        // Log validation errors
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"Error seeding user {user.UserName}: {error.Description}");
                        }
                    }
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
