﻿using FSSEstate.Core.Enums;

namespace FSSEstate.Repository.Entities
{
    public class AffairEntity : Auditable
    {
        public string Name { get; set; } = string.Empty;
        public string NameUz { get; set; } = string.Empty;
        public string NameRu { get; set; } = string.Empty; 
        public string NameEn { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long? ParentId { get; set; }
    }
}