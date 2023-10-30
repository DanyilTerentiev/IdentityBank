using IdentityBank.Domain.Models;
using MediatR;

namespace IdentityBank.Application.Services.Queries;

public class SignUpRequest : IRequest<AppResponse>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Password { get; set; } = null!;
}
