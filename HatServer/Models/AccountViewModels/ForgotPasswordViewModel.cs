using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HatServer.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
