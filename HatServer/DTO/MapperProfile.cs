using AutoMapper;
using HatServer.DTO.Request;
using HatServer.DTO.Response;
using Model.Entities;

namespace HatServer.DTO
{
    public class MapperProfile : Profile 
    {
        public MapperProfile()
        {
            //Entity to DTO
            CreateMap<GamePhrase, GamePhraseResponse>();
            CreateMap<GamePack, GamePackResponse>();
            CreateMap<GamePack, GamePackEmptyResponse>().ForMember(dest => dest.Count, x => x.MapFrom(g => g.Phrases.Count));

            //DTO to Entity
            CreateMap<PostDeviceInfoRequest, DeviceInfo>();
        }
    }
}