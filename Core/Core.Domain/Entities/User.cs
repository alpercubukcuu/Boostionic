using Core.Domain.Common;
using Newtonsoft.Json;


namespace Core.Domain.Entities
{
    public class User : BaseEntity
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("SurName")]
        public string SurName { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("PhoneNumber")]
        public string? PhoneNumber { get; set; }

        [JsonProperty("PasswordHash")]
        public string? PasswordHash { get; set; }

        [JsonProperty("FailedLoginAttempts")]
        public int? FailedLoginAttempts { get; set; }

        [JsonProperty("LastFailedLoginAttempt")]
        public DateTime? LastFailedLoginAttempt { get; set; }

        [JsonProperty("Birthday")]
        public DateTime? Birthday { get; set; }

        [JsonProperty("LastLogin")]
        public DateTime? LastLogin { get; set; }
        
        [JsonProperty("EmailVerified")]
        public bool EmailVerified  { get; set; }
        
        [JsonProperty("SocialMediaId")]
        public string? SocialMediaId { get; set; }
        [JsonProperty("SocialMediaType")]
        public int? SocialMediType { get; set; }
        
        [JsonProperty("UserRoleId")]
        public Guid UserRoleId { get; set; }
        public UserRole UserRole { get; set; }


        [JsonProperty("ClientId")]
        public Guid? ClientId { get; set; }
        public Client Client { get; set; }

        public Guid? OwnerId { get; set; }
        public OwnerEntity Owner { get; set; }       

        public bool IsSetup { get; set; } = false;
    }
}
