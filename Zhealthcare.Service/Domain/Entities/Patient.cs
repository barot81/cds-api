using Zhealthcare.Service.Models;

namespace Zhealthcare.Service.Domain.Entities
{
    public class Patient : BaseEntity
    {
        public string FacilityId { get; set; } = string.Empty;
        public long PatientNo { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string Room { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string CdsName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Sex { get; set; } = string.Empty;
        public string HealthPlan { get; set; } = string.Empty;
        public string QueryStatus { get; set; } = string.Empty;
        public DateTime? QueryDate { get; set; }
        public int Los { get; set; }
        public string FinancialClass { get; set; } = string.Empty;
        public string Mrn { get; set; } = string.Empty;
        public DateTime AdmitDate { get; set; }
        public string ReimbursementType { get; set; } = string.Empty;
        public DateTime? DischargeDate { get; set; }
        public string Concurrent_postDC { get; set; } = string.Empty;
        public string SecondaryInsurance { get; set; } = string.Empty;
        public bool Contracted { get; set; }
        public string PatientClass { get; set; } = string.Empty;
        public string StatusClass { get; set; } = string.Empty;
        public string ReviewStatus { get; set; } = string.Empty;

        public GeneralComment GeneralComment { get; set; } = default!;

        public string UmReviewer { get; set; } = string.Empty;
        public string Dcp { get; set; } = string.Empty;
        public string PatientType { get; set; } = string.Empty;
        public string SecondaryPhysician { get; set; } = string.Empty;
        public string DrgNo { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        public string ChiefComplaint { get; set; } = string.Empty;
        public string AttendingPhysician { get; set; } = string.Empty;
        public string AdmitOrigin { get; set; } = string.Empty;
        public string OriginDesc { get; set; } = string.Empty;
        public string Geo { get; set; } = string.Empty;
        public string Diff { get; set; } = string.Empty;
        public string RelWt { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; } = string.Empty;
        public DateTime LastUpdatedDate { get; set; }

        public bool ApplyFilter(PatientFilter patientFilter)
        {
            var result = true;
            if (patientFilter == null) return true;
            if (patientFilter.ReviewStatus != null)
                result = patientFilter.ReviewStatus.Contains(ReviewStatus);
            if (patientFilter.QueryStatus != null)
                result = result && patientFilter.QueryStatus.Contains(QueryStatus);
            DateTime? admStartDate = patientFilter?.AdmissionStartDate;
            DateTime? admEndDate = patientFilter?.AdmissionEndDate;
            if (admStartDate != null && admEndDate != null)
                 result = result && AdmitDate >= admStartDate && AdmitDate <= admEndDate;

            DateTime? disStartDate = patientFilter?.DischargeStartDate;
            DateTime? disEndDate = patientFilter?.DischargeEndDate;
            if (disStartDate != null && disEndDate != null)
                result = result && DischargeDate >= disStartDate && DischargeDate <= disEndDate;
            return result;
        }

    }
}
