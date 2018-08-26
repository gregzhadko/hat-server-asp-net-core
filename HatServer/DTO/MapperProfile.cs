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

            CreateMap<PostGameRequest, Game>()
                .ForMember(dest => dest.InGameId, s => s.MapFrom(g => g.Id))
                .ForMember(dest => dest.StartDate, s => s.MapFrom(g => new DateTime(g.Timestamp)))
                .ForMember(dest => dest.DeviceInfoGuid, s => s.MapFrom(g => new Guid(g.DeviceId)))
                .ForMember(dest => dest.Id, s => s.UseValue(0))
                .AfterMap((_, game) => game.Teams.ForEach(t => t.Game = game));
            
            CreateMap<TeamDTO, Team>()
                .ForMember(dest => dest.InGameId, s => s.MapFrom(t => t.Id))
                .ForMember(dest => dest.Id, s => s.UseValue(0))
                .AfterMap((_, team) => team.Players.ForEach(t => t.Team = team));
            
            CreateMap<PlayerDTO, Player>()
                .ForMember(dest => dest.InGameId, s => s.MapFrom(p => p.Id))
                .ForMember(dest => dest.Id, s => s.UseValue(0));

//            CreateMap<GamePhraseDTO, GamePhrase>()
//                .ForMember(dest => dest.Id, s => s.UseValue(0));
            
        }
    }
}