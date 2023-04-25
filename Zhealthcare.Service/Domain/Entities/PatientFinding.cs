namespace Zhealthcare.Service.Domain.Entities
{
    public class PatientFinding
    {
        public string Id { get; set; } = String.Empty;
        public string QueryType { get; set; } = String.Empty;
        public string CdsName { get; set; } = String.Empty;
        public DateTime QueryDate { get; set; }
        public string QueryDiagnosis { get; set; } = String.Empty;
        public string PhysicianName { get; set; } = String.Empty;
        public string ClinicalIndicator { get; set; } = String.Empty;
        public string CurrentDRG { get; set; } = String.Empty;
        public double InitialWeight { get; set; }
        public double Gmlos { get; set; }

        public double ExpectedDRG { get; set; }
        public double ExpectedWeight { get; set; }
        public string ExpectedGMLOS { get; set; } = String.Empty;
        public string ResponseDate { get; set; } = String.Empty;
        public string ResponseType { get; set; } = String.Empty;
        public string ResponseComment { get; set; } = String.Empty;
        public string FollowupComment { get; set; } = String.Empty;
        public int RevisedDRGNo { get; set; }
        public double RevisedWeight { get; set; }
        public double RevisedGMLOS { get; set; }
        public double WeightDifference { get; set; }
        public string Status { get; set; } = String.Empty;
        public string ClinicalSummary { get; set; } = String.Empty;
        public string Comments { get; set; } = String.Empty;
    }
}
