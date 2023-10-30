using System.Net;
using System.Security.Claims;
using AutoMapper;
using ExceptionHandler;
using Identity.Domain.Exceptions;
using IdentityBank.Application.Interfaces;
using IdentityBank.Application.Models.Response;
using IdentityBank.Application.Services.Queries;
using IdentityBank.Application.Shared;
using IdentityBank.Domain.Entities;
using IdentityBank.Domain.Models;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityBank.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        private async Task<ClaimsIdentity> CreateClaimsIdentity(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var role = await _roleManager.FindByNameAsync(roles.First());
            
            if (role == null)
            {
                throw new NotFoundException("Login failed");
            }

            var scopes = await _roleManager.GetClaimsAsync(role);
            
            return new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(CustomClaims.UserId, user.Id.ToString()),
                new Claim(CustomClaims.Scopes, string.Join(',', scopes))
            });
        }
        
        public AuthService(JwtSettings jwtSettings, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole<Guid>> roleManager, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<AppResponse> SignUpAsync(SignUpRequest model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x =>
            x.Email == model.Email || x.PhoneNumber == model.PhoneNumber);

            if (user != null)
            {
                throw new ValidationException("The UserProfile already exists");
            }

            var entity = _mapper.Map<AppUser>(model);

            var userResult = await _userManager.CreateAsync(entity, model.Password);
            
            if (!userResult.Succeeded)
            {
                throw new ValidationException(userResult.Errors.Select(x => new AppError(null, x.Description)).ToList());
            }

            var roleResult = await _userManager.AddToRoleAsync(entity, Role.User);

            if (!roleResult.Succeeded)
            {
                throw new ValidationException(userResult.Errors.Select(x => new AppError(null, x.Description)).ToList());
            }
            
            return new AppResponse(HttpStatusCode.Created);
        }

        public async Task<AppResponse<SignInResponse>> SignInAsync(SignInRequest model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            
            if (user == null) 
            {
                throw new NotFoundException("The user doesn't exist");
            }

            var validPassword = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            
            if (!validPassword.Succeeded)
            {
                throw new ValidationException("Wrong Password");
            }
            
            var claimsIdentity = await CreateClaimsIdentity(user);

            var token = JwtService.GenerateJwtToken(_jwtSettings, claimsIdentity);

            var refreshToken = JwtService.GenerateRefreshToken(_jwtSettings, user.Id);
            await StoreTokenAsync(user, "refresh_token", refreshToken);

            return new AppResponse<SignInResponse>(HttpStatusCode.OK, new SignInResponse(accessToken: token, refreshToken: refreshToken));

        }

        private async Task StoreTokenAsync(AppUser user, string name, string value)
        {
            var existingToken = await _userManager.GetAuthenticationTokenAsync(user, "TokenProvider", name);
            
            if (existingToken != null)
            {
                await _userManager.RemoveAuthenticationTokenAsync(user, "TokenProvider", name);
            }

            var result = await _userManager.SetAuthenticationTokenAsync(user, "TokenProvider", name, value);
            
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors.Select(x => new AppError(null,x.Description)).ToList());
            }
        }

        public async Task<AppResponse<SignInResponse>> ExchangeRefreshTokenAsync(string refreshToken)
        {
            var principal = JwtService.GetPrincipalFromExpiredToken(refreshToken, _jwtSettings);
            var userId = principal.Claims.Single(claim => claim.Type == CustomClaims.UserId).Value;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.ToString() == userId);
            
            if (user is null)
            {
                throw new NotFoundException("Invalid refresh token");
            }
            
            var existingToken = await _userManager.GetAuthenticationTokenAsync(user, "TokenProvider", "refresh_token");
            
            if (existingToken is null || !existingToken.Equals(refreshToken))
            {
                throw new ValidationException("Token is not valid, Sorry");
            }
            
            var removeResult = await _userManager.RemoveAuthenticationTokenAsync(user, "TokenProvider", "refresh_token");
            
            if (!removeResult.Succeeded)
            {
                throw new ValidationException(error: removeResult.Errors.Select(e => new AppError(e.Description, e.Code)));
            }
            
            var newRefresh = JwtService.GenerateRefreshToken(_jwtSettings, user.Id);
            var result = await _userManager.SetAuthenticationTokenAsync(user, "TokenProvider", "refresh_token", newRefresh);
            
            if (!result.Succeeded)
            {
                throw new ValidationException(error: result.Errors.Select(e => new AppError(e.Description, e.Code)));
            }

            var claimsIdentity = await CreateClaimsIdentity(user);

            var accessToken = JwtService.GenerateJwtToken(_jwtSettings, claimsIdentity);
            
            return new AppResponse<SignInResponse>(HttpStatusCode.OK, new SignInResponse(accessToken, newRefresh));
        }

        public async Task<AppResponse> GetByIdAsync(Guid requestId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Id == requestId);

            if (user is not null)
            {
                return new AppResponse(HttpStatusCode.OK);
            }

            return new AppResponse(HttpStatusCode.NotFound);
        }

        public async Task<AppResponse> DeleteByIdAsync(Guid requestId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Id == requestId);
 
            if (user is not null)
            {
                await _publishEndpoint.Publish(new UserDeletedEvent{UserId = user.Id, DeletedAt = DateTime.Now});
                // await _userManager.DeleteAsync(user);
                return new AppResponse<AppUser>(HttpStatusCode.OK, user);
            }

            throw new NotFoundException("User was not found");
        }
    }
}
