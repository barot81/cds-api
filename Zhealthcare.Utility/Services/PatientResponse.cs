using Newtonsoft.Json;

namespace Zhealthcare.Service.Domain.Entities
{
    internal class PatientResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("partitionKey")]
        public virtual string PartitionKey { get; set; } = string.Empty;

        [JsonProperty("facilityId")]
        public string FacilityId { get; set; } = string.Empty;

        [JsonProperty("room")]
        public string Room { get; set; } = string.Empty;

        [JsonProperty("accountNo")]
        public long AccountNo { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
        //public string FirstName { get; set; } = string.Empty;
        //public string LastName { get; set; } = string.Empty;
        //public string Cds { get; set; } = string.Empty;
        //public int Age { get; set; }
        //public string Sex { get; set; } = string.Empty;
        //public string HealthPlanName { get; set; } = string.Empty;
        //public string QueryStatus { get; set; } = String.Empty;
        //public string StatusClass { get; set; } = String.Empty;

        //public string QueryStatus { get; set; } = string.Empty;
        //public DateTime QueryDate { get; set; }
        //public double Los { get; set; }
        //public string FinancialClass { get; set; } = string.Empty;
        //public string Mrn { get; set; } = string.Empty;
        //public DateTime AdmissionDate { get; set; }
        //public string ReimbursementTypeLookup { get; set; } = string.Empty;
        //public DateTime DischargeDate { get; set; }
        //public string Concurrent_postDC { get; set; } = string.Empty;
        //public string PrimaryInsurance { get; set; } = string.Empty;
        //public string SecondaryInsurance { get; set; } = string.Empty;
        //public bool Contracted { get; set; }
        //public string PatientClass { get; set; } = string.Empty;
        //public string Pdx { get; set; } = string.Empty;
        //public GeneralComment GeneralComment { get; set; } = new();
        //public string CreatedBy { get; set; } = string.Empty;
        //public DateTime CreatedDate { get; set; }
        //public string LastUpdatedBy { get; set; } = string.Empty;
        //public DateTime LastUpdatedDate { get; set; }

    }
}