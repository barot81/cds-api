namespace Zhealthcare.Service.Models
{
    public class PatientCensisReport
    {
        public string RoomId { get; set; } = string.Empty;
        public string VisitId { get; set; } = string.Empty;
        public string MedicalRecord { get; set; } = string.Empty;
        public string ReimbursementType { get; set; } = string.Empty;

        public string PatientName { get; set; } = string.Empty;

        public DateTime AdmitDate { get; set; }

        public string Sex { get; set; } = string.Empty;

        public int Age { get; set; }

        public string AttendingMD { get; set; } = string.Empty;

        public string HealthPlan { get; set; } = string.Empty;

        public double DRGAmount { get; set; }

        public string Outlier { get; set; } = string.Empty;

        public double TotalCharges { get; set; }

        public string Threshold { get; set; } = string.Empty;
        public string PrincipalDiagnosis { get; set; } = string.Empty;
        public string SecondaryDiagnosis { get; set; } = string.Empty;

        public string PrincipalProcedure { get; set; } = string.Empty;
        public string DRGDescription { get; set; } = string.Empty;
        public double LOS { get; set; }
        public double GLOS { get; set; }
        public string Status { get; set; } = string.Empty;
        public string SCI { get; set; } = string.Empty;

    }
}
