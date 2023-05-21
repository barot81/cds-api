using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Models
{
    public class PatientUpdateDto
    {
        public bool IsActive { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Cds { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Sex { get; set; } = string.Empty;
        public string HealthPlanName { get; set; } = string.Empty;
        public string QueryStatus { get; set; } = string.Empty;
        public int Los { get; set; }
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
        public DateTime QueryDate { get; set; }
        public string Pdx { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public GeneralComment GeneralComment { get; set; } = new GeneralComment();
    }
}
