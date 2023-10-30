using IdentityBank.Domain.Models;
using MediatR;

namespace IdentityBank.Application.Services.Commands;

public record DeleteByIdCommand(Guid Id) : IRequest<AppResponse>;