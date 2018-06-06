﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;

namespace HatServer.DTO.Request
{
    public class LoginRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }

    [UsedImplicitly]
    public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(r => r.Name).NotEmpty().MaximumLength(50);
            RuleFor(r => r.Password).NotEmpty().MinimumLength(6).MaximumLength(100);
        }
    }
}
