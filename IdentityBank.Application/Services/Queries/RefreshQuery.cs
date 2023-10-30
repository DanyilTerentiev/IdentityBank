using IdentityBank.Application.Models.Response;
using IdentityBank.Domain.Models;
using MediatR;

namespace IdentityBank.Application.Services.Queries;

public record RefreshQuery(string Token) : IRequest<AppResponse<SignInResponse>>;