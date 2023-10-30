using IdentityBank.Application.Models.Response;
using IdentityBank.Domain.Models;
using MediatR;

namespace IdentityBank.Application.Services.Queries
{
    public class SignInRequest : IRequest<AppResponse<SignInResponse>>
    {
        public string Email { get; set; } = null!;
        
        public string Password { get; set; } = null!;
    }
}
