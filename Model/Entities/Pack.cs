﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using FluentValidation;
using JetBrains.Annotations;

namespace Model.Entities
{
    [DebuggerDisplay("{Id}, {Name}, {Description}")]
    public sealed class Pack
    {
        public int Id { get; set; }

        public int Version { get; set; }

        public bool Free { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Name { get; set; }

        [CanBeNull]
        public string Description { get; set; }

        public IList<PhraseItem> Phrases { get; set; } = new List<PhraseItem>();

        [NotNull]
        public override string ToString() => $"{Id}. {Name}\t{Description}";
    }

    [UsedImplicitly]
    public sealed class PackValidator : AbstractValidator<Pack>
    {
        public PackValidator()
        {
            RuleFor(p => p.Name).NotEmpty().Length(0, 20);
            RuleFor(p => p.Language).NotEmpty();
        }
    }
}
