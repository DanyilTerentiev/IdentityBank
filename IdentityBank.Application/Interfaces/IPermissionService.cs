namespace IdentityBank.Application.Interfaces;

public interface IPermissionService
{
    public bool CanGetUsers { get; }

    public bool CanDeleteUsers { get; }
}