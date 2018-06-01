using System.Linq;
using System.Collections.Generic;
using JetBrains.Annotations;
using Model.Entities;

namespace HatServer.DTO.Response
{
    public class BasePhraseItemResponse
    {
        public BasePhraseItemResponse([NotNull] PhraseItem phrase)
        {
            Phrase = phrase.Phrase;
            Complexity = phrase.Complexity;
            Description = phrase.Description;
            PackId = phrase.PackId;
            TrackId = phrase.TrackId;
            ReviewStates = phrase.ReviewStates.Select(s => new BaseReviewStateResponse(s)).ToList();
        }

        public string Phrase { get; set; }

        public double? Complexity { get; set; }

        public string Description { get; set; }

        public int PackId { get; set; }

        public int TrackId { get; set; }

        public List<BaseReviewStateResponse> ReviewStates { get; set; }
    }
}
