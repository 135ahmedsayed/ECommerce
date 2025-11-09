using AutoMapper;
using Ecommerce.Domain.Entities.Auth;
using Ecommerce.Shared.DTOs.Users;

namespace Ecommerce.Services.MappingProfile;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Address , AddressDTO>()
             .ReverseMap();
    }
}
