using IdentityBank.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityBank.Infrastructure.Configurations
{
    internal class IdentityRoleClaimsConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> builder)
        {
            builder.HasData(IdentityRoleHelper.GetAdminPermissionsScope());
            builder.HasData(IdentityRoleHelper.GetUserPermissionsScope());
        }
    }
}
