using AutoMapper;
using HatServer.DTO.Response;
using Model.Entities;

namespace HatServer.DTO
{
    public class DomainProfile : Profile 
    {
        public DomainProfile()
        {
            CreateMap<GamePhrase, GamePhraseResponse>();
            CreateMap<GamePack, GamePackResponse>();
            CreateMap<GamePack, GamePackEmptyResponse>().ForMember(dest => dest.Count, x => x.MapFrom(g => g.Phrases.Count));
        }
    }
}