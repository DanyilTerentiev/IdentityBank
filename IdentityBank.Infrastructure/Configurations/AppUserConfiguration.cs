using IdentityBank.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityBank.Infrastructure.Configurations
{
    internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            PasswordHasher<AppUser> hasher = new PasswordHasher<AppUser>();

            builder.HasData(new AppUser()
            {
                Id = Guid.Parse("574db616-e33e-43ad-bc31-7d16e6e0ad48"),
                UserName = "Admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "Admin",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(new AppUser(), "User_1"),
            }, new AppUser()
            {
                Id = Guid.Parse("537826d9-90b5-4d70-9606-addbd078d509"),
                UserName = "Mike",
                Email = "mike.goloborodko@gmail.com",
                EmailConfirmed = true,
                FirstName = "Mike",
                LastName = "Tyson",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(new AppUser(), "User_1"),
            });

            builder.HasData(new AppUser
            {
                Id = Guid.Parse("bebdddb7-27ab-4513-8ba7-a3eefcc7772b"),
                UserName = "Dmytro",
                FirstName = "Oleksii",
                LastName = "Oleksii",
                Email = "assistant.dmytro@gmail.com",
                EmailConfirmed = true,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumberConfirmed = true,
                PasswordHash = hasher.HashPassword(new AppUser(), "Admin_1")
            });
        }
    }
}