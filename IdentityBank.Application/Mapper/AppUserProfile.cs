using AutoMapper;
using IdentityBank.Application.Models.Request;
using IdentityBank.Domain.Entities;

namespace IdentityBank.Application.Mapper;

public class AppUserProfile : Profile
{
    public AppUserProfile()
    {
        CreateMap<SignUp, AppUser>()
            .AfterMap((source, dest) =>
            {
                dest.UserName = source.FirstName + source.LastName;
                dest.EmailConfirmed = true;
                dest.PhoneNumberConfirmed = true;
            });
    }    
}