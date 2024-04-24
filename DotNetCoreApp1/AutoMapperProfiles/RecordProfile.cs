using AutoMapper;
using DotNetCoreApp1.Controllers.Types;
using DotNetCoreApp1.Models.Types;

namespace DotNetCoreApp1.AutoMapperProfiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<Document, DocumentDto>()
                .ForMember(dest => dest.DocumentId, opt => opt.MapFrom( src => Guid.NewGuid()));
        }
    }
}
