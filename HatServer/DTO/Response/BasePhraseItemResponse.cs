using System.Linq;
using System.Collections.Generic;
using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DTO.Response
{
    public sealed class BasePhraseItemResponse
    {
        internal BasePhraseItemResponse([NotNull] PhraseItem phrase, IEnumerable<ServerUser> users = null)
        {
            Id = phrase.Id;
            Phrase = phrase.Phrase;
            Complexity = phrase.Complexity;
            Description = phrase.Description;
            PackId = phrase.PackId;
            TrackId = phrase.TrackId;
            Version = phrase.Version;
            Reviews = phrase.ReviewStates.Select(s => new BaseReviewStateResponse(s, users)).ToList();
        }

        [UsedImplicitly]
        public int Version { get; set; }

        [UsedImplicitly]
        public int Id { get; set; }

        [UsedImplicitly]
        public string Phrase { get; set; }

        [UsedImplicitly]
        public double? Complexity { get; set; }

        [UsedImplicitly]
        public string Description { get; set; }

        [UsedImplicitly]
        public int PackId { get; set; }

        [UsedImplicitly]
        public int TrackId { get; set; }

        [UsedImplicitly]
        public List<BaseReviewStateResponse> Reviews { get; set; }
    }
}
