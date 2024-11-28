using Core.Domain.Common;

namespace Core.Domain.Entities
{
    public class Client : BaseEntity
    {
        public string ClientName { get; set; }
        public string LegalName { get; set; }
        public string RegistrationNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public DateTime? EstablishDate { get; set; }

        public string ServiceNeed { get; set; }
        public string Notes { get; set; }

        public ICollection<User> Users { get; set; }
        public Language Language { get; set; }
        public ICollection<Industry> Industries { get; set; }

    }
}
