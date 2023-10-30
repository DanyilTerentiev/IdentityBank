using IdentityBank.Application.Interfaces;
using IdentityBank.Application.Models.Response;
using IdentityBank.Application.Services.Queries;
using IdentityBank.Domain.Models;
using MediatR;

namespace IdentityBank.Application.Services.Requests;

public class SignInRequestHandler : IRequestHandler<SignInRequest, AppResponse<SignInResponse>>
{
    private readonly IAuthService _authService;

    public SignInRequestHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AppResponse<SignInResponse>> Handle(SignInRequest request, CancellationToken cancellationToken)
    {
        return await _authService.SignInAsync(request);
    }
}