using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class ModelBuilderExtensions
{
    public static void SeedRoles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityRole>().HasData(new List<IdentityRole>()
        {
            new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Name = "HR",
                NormalizedName = "HR",
            },
            new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Name = "ADMIN",
                NormalizedName = "ADMIN",
            }
        });
    }
}