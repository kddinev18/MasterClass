using HRManagement.Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class ModelBuilderExtensions
{
    public static void SeedRoles(this ModelBuilder modelBuilder)
    {
        var rules = typeof(Roles)
            .GetProperties()
            .Where(f => f.PropertyType == typeof(string))
            .Select(f => (string)f.GetValue(null));

        List<IdentityRole> identityRoles = new List<IdentityRole>();
        foreach (string rule in rules)
        {
            identityRoles.Add(new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Name = rule,
                NormalizedName = rule,
            });
        }

        modelBuilder.Entity<IdentityRole>().HasData(identityRoles);
    }
}