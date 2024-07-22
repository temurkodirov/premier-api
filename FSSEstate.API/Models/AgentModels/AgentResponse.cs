namespace FSSEstate.API.Models.AgentModels
{
    public class AgentResponse
    {
        public long Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Landline { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
        public string FacebookUrl { get; set; } = string.Empty;
        public string TwitterUrl { get; set; } = string.Empty;
        public string LinkedInUrl { get; set; } = string.Empty;
        public string GooglePlusUrl { get; set; } = string.Empty;
        public string InstagramUrl { get; set; } = string.Empty;
        public string Tumbler { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
