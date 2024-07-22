using FSSEstate.Core.Models.UserModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSSEstate.Repository.Entities
{
    public class ConfirmationEmailEntity : Auditable
    {
        public string Guid { get; set; } = string.Empty;
        public bool IsUsed { get; set; } = false;
        public DateTime? ExpiredAt { get; set; } = DateTime.UtcNow.AddHours(5);
        public AccountEntity? Account { get; set; }
        public long AccountId { get; set; }
    }
}
