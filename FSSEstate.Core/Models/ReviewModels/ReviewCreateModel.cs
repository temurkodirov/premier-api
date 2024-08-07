﻿namespace FSSEstate.Core.Models.ReviewModels
{
    public class ReviewCreateModel
    {
        public decimal Mark { get; set; }
        public string Description { get; set; } = string.Empty;
        public long? AccountId { get; set; }
        public long? ProjectId { get; set; }
        public string UserAlias { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(5);
    }
}
