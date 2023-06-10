using MediatR;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Models
{
    public class PatientUpdateDto
    {
        public bool IsActive { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string Cds { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Sex { get; set; } = string.Empty;
        public string HealthPlan { get; set; } = string.Empty;
        public int Los { get; set; }
        public string FinancialClass { get; set; } = string.Empty;
        public string Mrn { get; set; } = string.Empty;
        public DateTime AdmitDate { get; set; } 
        public string ReimbursementType { get; set; } = string.Empty;
        public DateTime DischargeDate { get; set; } 
        public string Concurrent_postDC { get; set; } = string.Empty;
        public string PrimaryInsurance { get; set; } = string.Empty;
        public string SecondaryInsurance { get; set; } = string.Empty;
        public bool Contracted { get; set; }
        public string ReviewStatus { get; set; } = string.Empty;
        public GeneralComment GeneralComment { get; set; } = new GeneralComment();
        public string PatientClass { get; set; } = string.Empty;
        public DateTime QueryDate { get; set; }
        public string QueryStatus { get; set; } = string.Empty;

    }

    public class PatientCommentUpdateDto
    {
        public string ReviewStatus { get; set; } = string.Empty;

        public GeneralComment GeneralComment { get; set; } = default!;
    }

    public class PatientReviewStatusUpdateDto
    {
        public string ReviewStatus { get; set; } = string.Empty;
    }
}

