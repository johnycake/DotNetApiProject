using AutoMapper;
using DotNetCoreApp1.Controllers.Types;
using DotNetCoreApp1.Models.Types;

namespace DotNetCoreApp1.AutoMapperProfiles
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<Data, DataDto>()
                .ForMember(dest => dest.DataId, opt => opt.MapFrom( src => Guid.NewGuid()));
        }
    }
}
