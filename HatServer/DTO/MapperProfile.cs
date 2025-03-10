﻿using System;
using AutoMapper;
using HatServer.DTO.Request;
using HatServer.DTO.Response;
using JetBrains.Annotations;
using Model.Entities;
using Utilities;

namespace HatServer.DTO
{
    [UsedImplicitly]
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region Entity to DTO

            CreateMap<GamePhrase, GamePhraseResponse>();
            CreateMap<GamePack, GamePackResponse>();
            CreateMap<GamePack, GamePackEmptyResponse>()
                .ForMember(dest => dest.Count, x => x.MapFrom(g => g.Phrases.Count));

            #endregion

            #region DTO to Entity

            CreateMap<PostDeviceInfoRequest, DeviceInfo>()
                .ForMember(dest => dest.DeviceGuid, s => s.MapFrom(d => d.DeviceId));

            CreateMap<PostGameRequest, Game>()
                .ForMember(dest => dest.InGameId, s => s.MapFrom(g => g.Id))
                .ForMember(dest => dest.StartDate, s => s.MapFrom(g => g.Timestamp.ToDateTime()))
                .ForMember(dest => dest.DeviceId, s => s.MapFrom(g => new Guid(g.DeviceId)))
                .ForMember(dest => dest.Id, s => s.MapFrom(src => 0))
                .AfterMap((_, game) =>
                {
                    game.Teams.ForEach(t => t.Game = game);
                    game.Words.ForEach(t => t.Game = game);
                });

            CreateMap<GamePhraseDTO, InGamePhrase>()
                .ForMember(dest => dest.Id, s => s.MapFrom(src => 0))
                .ForMember(dest => dest.InGameId, s => s.MapFrom(g => g.Id));

            CreateMap<TeamDTO, Team>()
                .ForMember(dest => dest.InGameId, s => s.MapFrom(t => t.Id))
                .ForMember(dest => dest.Id, s => s.MapFrom(src => 0))
                .AfterMap((_, team) => team.Players.ForEach(t => t.Team = team));

            CreateMap<PlayerDTO, Player>()
                .ForMember(dest => dest.InGameId, s => s.MapFrom(p => p.Id))
                .ForMember(dest => dest.Id, s => s.MapFrom(src => 0));

            CreateMap<PostRoundRequest, Round>()
                .ForMember(dest => dest.Id, s => s.MapFrom(src => 0))
                .ForMember(dest => dest.GameId, s => s.MapFrom(src => 0))
                .ForMember(dest => dest.DeviceId, s => s.MapFrom(r => new Guid(r.DeviceId)))
                .ForMember(dest => dest.StartTime, s => s.MapFrom(r => r.Timestamp.ToDateTime()))
                .ForMember(dest => dest.GameGUID, s => s.MapFrom(r => r.GameId))
                .AfterMap((_, round) => round.Words.ForEach(w => w.Round = round));

            CreateMap<RoundPhraseDTO, RoundPhrase>()
                .ForMember(dest => dest.Id, s => s.MapFrom(src => 0))
                .ForMember(dest => dest.State,
                    s => s.MapFrom(r => Enum.Parse(typeof(RoundPhraseStateEnum), r.State.ReplaceFirstCharToUpper())));
            //.ForMember(dest => dest.State, s => s.MapFrom(p => p.State))
            //.ForMember(dest => dest.RoundId, s => s.MapFrom(p => p.WordId))

            CreateMap<SettingsDTO, Settings>()
                .ForMember(dest => dest.Id, s => s.MapFrom(src => 0));

            #endregion

        }
    }
}