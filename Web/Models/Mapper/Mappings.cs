using AutoMapper;
using Web.Models.ViewModels;

namespace Web.Models.Mapper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Role, RoleEdit>().ReverseMap();
        }
    }
}
