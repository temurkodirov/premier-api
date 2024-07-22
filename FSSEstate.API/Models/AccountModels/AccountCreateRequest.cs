using FSSEstate.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSSEstate.API.Models.AccountModels
{
    public class AccountCreateRequest
    {
        public string? EmailOrPhoneNumber { get; set; }
        public string Password { get; set; } = string.Empty;
        public string? Fullname { get; set; }
        public AccountType AccountType { get; set; }
    }
}
