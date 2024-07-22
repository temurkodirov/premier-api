using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSSEstate.Core.Models.ConfirmEmailModels
{
    public class ConfirmEmailModel
    {
        public long Id { get; set; }
        public string Guid { get; set; } = string.Empty;
        public bool IsUsed { get; set; }
        public DateTime? ExpiredAt { get; set; } 
        public DateTime? CreatedAt { get; set; }
        public long AccountId { get; set; }
    }
}
