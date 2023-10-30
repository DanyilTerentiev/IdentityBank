using IdentityBank.Application.Interfaces;
using IdentityBank.Application.Services.Commands;
using IdentityBank.Domain.Models;
using MediatR;

namespace IdentityBank.Application.Services.Handlers;

public class DeleteByIdHandler : IRequestHandler<DeleteByIdCommand, AppResponse>
{
    private readonly IAuthService _authService;
    
    public DeleteByIdHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AppResponse> Handle(DeleteByIdCommand request, CancellationToken cancellationToken)
    {
        return await _authService.DeleteByIdAsync(request.Id);
    }
}