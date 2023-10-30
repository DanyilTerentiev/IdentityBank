using IdentityBank.Application.Models.Response;
using IdentityBank.Application.Services.Queries;
using IdentityBank.Domain.Entities;
using IdentityBank.Domain.Models;

namespace IdentityBank.Application.Interfaces;

public interface IAuthService
{
    Task<AppResponse> SignUpAsync(SignUpRequest model);
        
    Task<AppResponse<SignInResponse>> SignInAsync(SignInRequest model);
        
    Task<AppResponse<SignInResponse>> ExchangeRefreshTokenAsync(string refreshToken);
    
    Task<AppResponse> GetByIdAsync(Guid requestId);
    
    Task<AppResponse> DeleteByIdAsync(Guid requestId);
}