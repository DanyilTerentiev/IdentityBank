using System.Net;
using IdentityBank.Application.Interfaces;
using IdentityBank.Application.Services.Commands;
using IdentityBank.Application.Services.Queries;
using IdentityBank.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityBankIdentityBank.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPermissionService _permissionService;
        
        public AccountController(IMediator mediator, IPermissionService permissionService)
        {
            _mediator = mediator;
            _permissionService = permissionService;
        }
        
        [HttpPost("sign-up")]
        public async Task<AppResponse> Register(SignUpRequest signUpModel)
        {
            return await _mediator.Send(signUpModel);
        }

        [HttpPost("sign-in")]
        public async Task<AppResponse> SignIn(SignInRequest signIn)
        {
            return await _mediator.Send(signIn);
        }

        [HttpPost("refresh")]
        public async Task<AppResponse> Refresh([FromBody]string refreshToken)
        {
            return await _mediator.Send(new RefreshQuery(refreshToken));
        }

        [HttpGet("{id}")]
        public async Task<AppResponse> GetById(Guid id)
        {
            if (_permissionService.CanGetUsers)
                return await _mediator.Send(new GetByIdQuery(id));
            
            return new AppResponse(HttpStatusCode.Forbidden);
        }

        [HttpDelete("{id}")]
        public async Task<AppResponse> DeleteById(Guid id)
        {
            // if (_permissionService.CanDeleteUsers)
                return await _mediator.Send(new DeleteByIdCommand(id));

            return new AppResponse(HttpStatusCode.Forbidden);
        }
    }
}
