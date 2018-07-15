using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using Utilities;

namespace Model.Entities
{
    public sealed class ReviewState : ICloneable<ReviewState>, IEquatable<ReviewState>
    {
        public int Id { get; set; }

        [ForeignKey(nameof(PhraseItemId))]
        public PhraseItem PhraseItem { get; set; }

        public int PhraseItemId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ServerUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public State State { get; set; }

        public string Comment { get; set; }

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

        public bool Equals([CanBeNull] ReviewState other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id == other.Id && PhraseItemId == other.PhraseItemId && string.Equals(UserId, other.UserId) &&
                   State == other.State && string.Equals(Comment, other.Comment);
        }

        public override bool Equals([CanBeNull] object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is ReviewState state && Equals(state);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ PhraseItemId;
                hashCode = (hashCode * 397) ^ (UserId != null ? UserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)State;
                hashCode = (hashCode * 397) ^ (Comment != null ? Comment.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
