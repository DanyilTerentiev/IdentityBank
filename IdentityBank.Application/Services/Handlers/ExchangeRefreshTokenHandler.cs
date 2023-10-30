using IdentityBank.Application.Interfaces;
using IdentityBank.Application.Models.Response;
using IdentityBank.Application.Services.Queries;
using IdentityBank.Domain.Models;
using MediatR;

namespace IdentityBank.Application.Services.Requests;

public class ExchangeRefreshTokenHandler : IRequestHandler<RefreshQuery, AppResponse<SignInResponse>>
{
    private readonly IAuthService _authService;

    public ExchangeRefreshTokenHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AppResponse<SignInResponse>> Handle(RefreshQuery request, CancellationToken cancellationToken)
    {
        return await _authService.ExchangeRefreshTokenAsync(request.Token);
    }
}