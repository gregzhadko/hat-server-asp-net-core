using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DTO.Response
{
    public sealed class BasePackResponse
    {
        public BasePackResponse([NotNull] Pack pack, IEnumerable<ServerUser> users)
        {
            Id = pack.Id;
            Language = pack.Language;
            Name = pack.Name;
            Version = 1;
            Description = pack.Description;
            Phrases = pack.Phrases.Select(p => new BasePhraseItemResponse(p, users)).ToList();
        }

        [UsedImplicitly]
        public int Id { get; set; }

        [UsedImplicitly]
        public string Language { get; set; }

        [UsedImplicitly]
        public string Name { get; set; }

        [UsedImplicitly]
        public string Description { get; set; }

        [UsedImplicitly]
        public int Version { get; set; }

        [UsedImplicitly]
        public IList<BasePhraseItemResponse> Phrases { get; set; }
    }
}
