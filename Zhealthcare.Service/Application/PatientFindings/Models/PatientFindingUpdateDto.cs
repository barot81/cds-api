
namespace Zhealthcare.Service.Application.PatientFindings.Models
{
    public class PatientFindingUpdateDto
    {   
        public string QueryType { get; set; } = string.Empty;
        public string CdsName { get; set; } = string.Empty;
        public DateTime QueryDate { get; set; }
        public string QueryDiagnosis { get; set; } = string.Empty;
        public string PhysicianName { get; set; } = string.Empty;
        public string ClinicalIndicator { get; set; } = string.Empty;
        public double CurrentDRG { get; set; }
        public double InitialWeight { get; set; }
        public double Gmlos { get; set; }
        public double ExpectedDRG { get; set; }
        public double ExpectedWeight { get; set; }
        public double ExpectedGMLOS { get; set; } 
        public DateTime ResponseDate { get; set; } 
        public string ResponseType { get; set; } = string.Empty;
        public string ResponseComment { get; set; } = string.Empty;
        public string FollowupComment { get; set; } = string.Empty;
        public double RevisedDRG { get; set; } 
        public double RevisedWeight { get; set; }
        public double RevisedGMLOS { get; set; } 
        public double WeightDifference { get; set; }
        public string Status { get; set; } = string.Empty;
        public string ClinicalSummary { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
    }
}
