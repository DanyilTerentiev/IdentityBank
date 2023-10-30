using IdentityBank.Application.Interfaces;
using IdentityBank.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace IdentityBank.Application.Services;

public class PermissionService : IPermissionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PermissionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private bool HasPermission(string permission)
    {
        var scope = _httpContextAccessor.HttpContext.User.Claims
            .Where(x => x.Type == CustomClaims.Scopes)
            .SelectMany(c => c.Value.Split(','));

        return scope.Contains(permission);
    }

    public bool CanGetUsers => HasPermission("user: get");

    public bool CanDeleteUsers => HasPermission("user: delete");
}