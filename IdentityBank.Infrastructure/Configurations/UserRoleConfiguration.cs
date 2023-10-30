using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Configurations
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            builder.HasData(new List<IdentityUserRole<Guid>>()
            {
                new()
                {
                    RoleId = Guid.Parse("d522e6ae-a0d5-4753-8bf1-feb30e3b575e"),
                    UserId = Guid.Parse("574db616-e33e-43ad-bc31-7d16e6e0ad48"),
                },
                new()
                {
                    RoleId = Guid.Parse("7a7231fb-fe42-40df-bf8b-1adcb564a135"),
                    UserId = Guid.Parse("537826d9-90b5-4d70-9606-addbd078d509")
                }
            });
        }
    }
}
