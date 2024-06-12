using AutoMapper;
using Blindness_API.Models.DTO;
using Blindness_API.Models;

namespace Blindness_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
           

            CreateMap<ApplicationUser, UserDTO>().ReverseMap();

        }
    }
}
