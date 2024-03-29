﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Utilities;

namespace Model.Entities
{
    public sealed class ReviewState : ICloneable<ReviewState>
    {
        public ReviewState()
        {
            DateTime = DateTime.UtcNow;
        }
        
        public int Id { get; set; }

        [ForeignKey(nameof(PhraseItemId))]
        public PhraseItem PhraseItem { get; set; }

        public int PhraseItemId { get; set; }

        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public ServerUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public State State { get; set; }

        public string Comment { get; set; }
        
        public DateTime DateTime { get; private set; }

        [NotNull]
        public ReviewState Clone()
        {
            return new ReviewState
            {
                PhraseItemId = PhraseItemId,
                UserId = UserId,
                State = State,
                Comment = Comment
            };
        }

        [NotNull]
        public override string ToString() => $"{User.UserName}: {State}";
    }
}
