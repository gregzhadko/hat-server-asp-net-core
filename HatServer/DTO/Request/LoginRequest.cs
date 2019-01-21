using System.ComponentModel.DataAnnotations;
using FluentValidation;
using JetBrains.Annotations;

namespace HatServer.DTO.Request
{
    /// <summary>
    /// Data transfer object for passing credentials
    /// The credentials should be provided to each user individually.
    /// </summary>
    public sealed class LoginRequest
    {
        /// <summary>
        /// The name of the user.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// User's password.
        /// </summary>
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
