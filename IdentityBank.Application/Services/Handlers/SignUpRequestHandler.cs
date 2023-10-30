using IdentityBank.Application.Interfaces;
using IdentityBank.Application.Services.Queries;
using IdentityBank.Domain.Models;
using MediatR;

namespace IdentityBank.Application.Services.Requests;

public class SignUpRequestHandler : IRequestHandler<SignUpRequest, AppResponse>
{
    private readonly IAuthService _authService;
    
    public SignUpRequestHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AppResponse> Handle(SignUpRequest request, CancellationToken cancellationToken)
    {
        return await _authService.SignUpAsync(request);
    }
}