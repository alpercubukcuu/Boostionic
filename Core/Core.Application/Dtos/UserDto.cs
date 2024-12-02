using Core.Application.Dtos.CommonDtos;
using Core.Domain.Entities;
using Newtonsoft.Json;


namespace Core.Application.Dtos
{
    public class UserDto : BaseDto
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
        public string PasswordHash { get; set; }

        [JsonProperty("FailedLoginAttempts")]
        public int? FailedLoginAttempts { get; set; }

        [JsonProperty("LastFailedLoginAttempt")]
        public DateTime? LastFailedLoginAttempt { get; set; }

        [JsonProperty("Birthday")]
        public DateTime Birthday { get; set; }

        [JsonProperty("LastLogin")]
        public DateTime LastLogin { get; set; }

        [JsonProperty("UserRoleId")]
        public Guid UserRoleId { get; set; }
        public UserRoleDto UserRole { get; set; }


        [JsonProperty("ClientId")]
        public Guid? ClientId { get; set; }
        public ClientDto Client { get; set; }


        [JsonProperty("OwnerId")]
        public Guid? OwnerId { get; set; }
        public OwnerEntityDto Owner { get; set; }


        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("RememberMe")]
        public bool RememberMe { get; set; }

        [JsonProperty("Token")]
        public string Token { get; set; }
        public bool IsInvated { get; set; }

        public bool IsOwner { get; set; }
        public Guid? ParentId { get; set; }
        public byte UserType { get; set; } = 0;
        public bool IsSetup { get; set; } = false;
    }
}
