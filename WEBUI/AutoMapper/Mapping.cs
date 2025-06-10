using AutoMapper;
using CORE.DTOs.ApplicationUser;
using CORE.Identity;

namespace WEBUI.AutoMapper
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<CreateApplicaitonUserDTO, ApplicationUser>().ReverseMap();
        }
    }
}
