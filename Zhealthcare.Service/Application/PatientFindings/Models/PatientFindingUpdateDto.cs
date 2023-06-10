
namespace Zhealthcare.Service.Application.PatientFindings.Models
{
    public class PatientFindingUpdateDto
    {
        public string Id { get; set; } = string.Empty;
        public string QueryType { get; set; } = string.Empty;
        public string CdsName { get; set; } = string.Empty;
        public DateTime QueryDate { get; set; }
        public string QueryDiagnosis { get; set; } = string.Empty;
        public string PhysicianName { get; set; } = string.Empty;
        public string ClinicalIndicator { get; set; } = string.Empty;
        public string CurrentDrgNo { get; set; } = string.Empty;
        public string CurrentDrgDescription { get; set; } = string.Empty;
        public double InitialWeight { get; set; }
        public double Gmlos { get; set; }
        public string ExpectedDrgNo { get; set; } = string.Empty;
        public string ExpectedDrgDescription { get; set; } = string.Empty;
        public double ExpectedWeight { get; set; }
        public double ExpectedGmlos { get; set; } 
        public DateTime ResponseDate { get; set; } 
        public string ResponseType { get; set; } = string.Empty;
        public string ResponseComment { get; set; } = string.Empty;
        public string FollowupComment { get; set; } = string.Empty;
        public string RevisedDrgNo { get; set; } = string.Empty;
        public string RevisedDrgDescription { get; set; } = string.Empty;
        public double RevisedWeight { get; set; }
        public double RevisedGmlos { get; set; } 
        public double WeightDifference { get; set; }
        public string QueryStatus { get; set; } = string.Empty;
        public string ClinicalSummary { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
    }
}
