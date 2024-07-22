using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSSEstate.Core.Models.ConfirmEmailModels
{
    public class ConfirmEmailCreateModel
    {
        public string Guid { get; set; } = string.Empty;
        public bool IsUsed { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public long AccountId { get; set; }
    }
}
