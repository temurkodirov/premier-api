using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSSEstate.API.Models.UserModels
{
    public class UserLoginRequest
    {
        [Required]
        public string EmailOrPhoneNumber { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
