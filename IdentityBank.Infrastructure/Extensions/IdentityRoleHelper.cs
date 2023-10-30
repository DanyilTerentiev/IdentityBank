using Microsoft.AspNetCore.Identity;

namespace IdentityBank.Infrastructure.Extensions
{
    public static class IdentityRoleHelper
    {
        private static readonly Dictionary<string, string[]> AdminScopes = new()
        {
            {"user", new[] { "get", "create", "delete", "update" } }
        };

        private static readonly Dictionary<string, string[]> UserScopes = new()
        {
            { "user", new[] { "get" } }
        };

        private static int _id = 1;
        
        internal static IEnumerable<IdentityRoleClaim<Guid>> GetAdminPermissionsScope()
        {
            var roleClaims = new List<IdentityRoleClaim<Guid>>();

            foreach (var entityPermission in AdminScopes)
            {
                var entity = entityPermission.Key;
                var permissions = entityPermission.Value;

                foreach (var permission in permissions)
                {
                    var claim = new IdentityRoleClaim<Guid>
                    {
                        Id = _id,
                        RoleId = Guid.Parse("d522e6ae-a0d5-4753-8bf1-feb30e3b575e"),
                        ClaimType = entity,
                        ClaimValue = permission
                    };

                    roleClaims.Add(claim);
                    _id++;
                }
            }

            return roleClaims;
        }

        internal static IEnumerable<IdentityRoleClaim<Guid>> GetUserPermissionsScope()
        {
            var claims = new List<IdentityRoleClaim<Guid>>();
            foreach (var scope in UserScopes)
            {
                var entity = scope.Key;
                var permissions = scope.Value;

                foreach (var permission in permissions)
                {
                    claims.Add(new IdentityRoleClaim<Guid>()
                    {
                        Id = _id,
                        RoleId = Guid.Parse("7a7231fb-fe42-40df-bf8b-1adcb564a135"),
                        ClaimType = entity,
                        ClaimValue = permission
                    });
                    _id++;
                }
            }

            return claims;
        }

    }
}
