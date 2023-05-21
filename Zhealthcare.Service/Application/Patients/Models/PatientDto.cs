﻿namespace Zhealthcare.Service.Application.Patients.Models
{
    public class PatientDto
    {
        public Guid Id { get; set; }
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
        public string QueryStatus { get; set; } = string.Empty;
        public int Los { get; set; }
        public string FinancialClass { get; set; } = string.Empty;
        public string Mrn { get; set; } = string.Empty;
        public string AdmissioinDate { get; set; } = string.Empty;
        public string ReimbursementType { get; set; } = string.Empty;
        public string DischargeDate { get; set; } = string.Empty;
        public string Concurrent_postDC { get; set; } = string.Empty;
        public string PrimaryInsurance { get; set; } = string.Empty;
        public string SecondaryInsurance { get; set; } = string.Empty;
        public bool Contracted { get; set; }
        public string PatientClass { get; set; } = string.Empty;
        public string Pdx { get; set; } = string.Empty;
        public string StatusClass { get; set; } = string.Empty;
    }
}