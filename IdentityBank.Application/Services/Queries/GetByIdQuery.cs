using IdentityBank.Domain.Models;
using MediatR;

namespace IdentityBank.Application.Services.Queries;

public record GetByIdQuery(Guid Id) : IRequest<AppResponse>;