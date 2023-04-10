using AutoMapper;
using CVAsp.NetProject.Entities;
using CVAsp.NetProject.Models;

namespace CVAsp.NetProject
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, CreateUserModel>().ReverseMap();
            CreateMap<User, EditUserModel>().ReverseMap();
            CreateMap<User, RegisterViewModel>().ReverseMap();
            CreateMap<User, UserInfoViewModel>().ReverseMap();
        }
    }
}
