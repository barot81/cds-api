namespace Zhealthcare.Service.Domain.Entities
{
    public class PatientFinding : BaseEntity
    {
        public Guid PatientId { get; set; }
        public string FacilityId { get; set; } = string.Empty;
        public string QueryType { get; set; } = String.Empty;
        public string CdsName { get; set; } = String.Empty;
        public DateTime QueryDate { get; set; }
        public string QueryDiagnosis { get; set; } = String.Empty;
        public string PhysicianName { get; set; } = String.Empty;
        public string ClinicalIndicator { get; set; } = String.Empty;
        public string CurrentDrg { get; set; } = string.Empty; 
        public double InitialWeight { get; set; }
        public double Gmlos { get; set; }

        public string ExpectedDrg { get; set; } = string.Empty;
        public double ExpectedWeight { get; set; }
        public double ExpectedGmlos { get; set; }
        public DateTime ResponseDate { get; set; }
        public string ResponseType { get; set; } = String.Empty;
        public string ResponseComment { get; set; } = String.Empty;
        public string FollowupComment { get; set; } = String.Empty;
        public string RevisedDrg { get; set; } = string.Empty;
        public double RevisedWeight { get; set; }
        public double RevisedGmlos { get; set; }
        public double WeightDifference { get; set; }
        public string Status { get; set; } = String.Empty;
        public string ClinicalSummary { get; set; } = String.Empty;
        public string Comments { get; set; } = String.Empty;
        public DateTime CreatedTime { get; set; }
        
    }
}
