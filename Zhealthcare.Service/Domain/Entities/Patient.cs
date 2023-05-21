using Zhealthcare.Service.Models;

namespace Zhealthcare.Service.Domain.Entities
{
    public class Patient : BaseEntity
    {
        public string FacilityId { get; set; } = string.Empty;
        public string RoomId { get; set; } = string.Empty;
        public long AccountNo { get; set; }
        public bool IsActive { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Cds { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Sex { get; set; } = string.Empty;
        public string HealthPlanName { get; set; } = string.Empty;
        public string Status { get; set; } = String.Empty;

        public string QueryStatus { get; set; } = string.Empty;
        public DateTime QueryDate { get; set; }
        public double Los { get; set; }
        public string FinancialClass { get; set; } = string.Empty;
        public string Mrn { get; set; } = string.Empty;
        public DateTime AdmissionDate { get; set; }
        public string ReimbursementType { get; set; } = string.Empty;
        public DateTime DischargeDate { get; set; }
        public string Concurrent_postDC { get; set; } = string.Empty;
        public string PrimaryInsurance { get; set; } = string.Empty;
        public string SecondaryInsurance { get; set; } = string.Empty;
        public bool Contracted { get; set; }
        public string PatientClass { get; set; } = string.Empty;
        public string Pdx { get; set; } = string.Empty;
        public GeneralComment GeneralComment { get; set; } = new();
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; }
        public string LastUpdatedBy { get; set; } = string.Empty;
        public DateTime LastUpdatedTime { get; set; }

        public bool ApplyFilter(PatientFilter patientFilter)
        {
            var result = true;
            if (patientFilter == null) return true;
            if (patientFilter.Status != null)
                result = patientFilter.Status.Contains(Status);
            if (patientFilter.QueryStatus != null)
                result = result && patientFilter.QueryStatus.Contains(QueryStatus);
            DateTime? admStartDate = patientFilter?.AdmissionStartDate;
            DateTime? admEndDate = patientFilter?.AdmissionEndDate;
            if (admStartDate != null && admEndDate != null)
                 result = result && AdmissionDate >= admStartDate && AdmissionDate <= admEndDate;

            DateTime? disStartDate = patientFilter?.DischargeStartDate;
            DateTime? disEndDate = patientFilter?.DischargeEndDate;
            if (disStartDate != null && disEndDate != null)
                result = result && DischargeDate >= disStartDate && DischargeDate <= disEndDate;
            return result;
        }

    }
}
