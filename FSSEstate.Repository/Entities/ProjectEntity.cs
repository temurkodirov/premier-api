using FSSEstate.Core.Enums;

namespace FSSEstate.Repository.Entities
{
    public class ProjectEntity : Auditable
    {
        public string PropertyTitle { get; set; } = string.Empty;
        public ProjectStatus Status { get; set; }
        public long? CategoryId { get; set; }
        public CategoryEntity? Category { get; set; }
        public long? AccountId { get; set; }
        public AccountEntity? Account { get; set; }
        public decimal Price { get; set; }
        public string Area { get; set; } = string.Empty;
        public int Bathrooms { get; set; }
        public int Bedrooms { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int BuildingAge { get; set; }
        public int Garage { get; set; }
        public int Rooms { get; set; }
        public string OtherFeatures { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public bool Agreement { get; set; } 
    }
}
