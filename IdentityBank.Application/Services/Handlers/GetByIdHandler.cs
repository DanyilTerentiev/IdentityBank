using IdentityBank.Application.Interfaces;
using IdentityBank.Application.Services.Queries;
using IdentityBank.Domain.Models;
using MediatR;

namespace IdentityBank.Application.Services.Handlers;

public class GetByIdHandler : IRequestHandler<GetByIdQuery, AppResponse>
{
    private readonly IAuthService _authService;

    public GetByIdHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public Task<AppResponse> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        return _authService.GetByIdAsync(request.Id);
    }
}