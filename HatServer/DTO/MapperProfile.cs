using System;
using System.Linq;
using AutoMapper;
using HatServer.DTO.Request;
using HatServer.DTO.Response;
using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DTO
{
    [UsedImplicitly]
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Entity to DTO
            CreateMap<GamePhrase, GamePhraseResponse>();
            CreateMap<GamePack, GamePackResponse>();
            CreateMap<GamePack, GamePackEmptyResponse>()
                .ForMember(dest => dest.Count, x => x.MapFrom(g => g.Phrases.Count));

            //DTO to Entity
            CreateMap<PostDeviceInfoRequest, DeviceInfo>()
                .ForMember(dest => dest.DeviceGuid, s => s.MapFrom(d => d.DeviceId));

            CreateMap<PlayerDTO, Player>().ForMember(dest => dest.InGameId, s => s.MapFrom(p => p.Id));
            CreateMap<TeamDTO, Team>().ForMember(dest => dest.InGameId, s => s.MapFrom(t => t.Id));
            CreateMap<PostGameRequest, Game>().ForMember(dest => dest.InGameId, s => s.MapFrom(g => new Guid(g.Id)))
                .ForMember(dest => dest.StartDate, s => s.MapFrom(g => new DateTime(g.Timestamp)));
        }
    }
}