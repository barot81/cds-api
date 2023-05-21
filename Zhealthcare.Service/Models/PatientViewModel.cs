namespace Zhealthcare.Service.Models
{
    public class PatientViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FacilityId { get; set; } = string.Empty;
        public string RoomId { get; set; } = string.Empty;
        public long AccountNo { get; set; }
        public string HealthPlanName { get; set; } = string.Empty;
        public string ReviewStatus { get; set; } = string.Empty;
        public string CdsName { get; set; } = string.Empty;
        public DateTime AdmissionDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public string ReimbursementType { get; set; } = string.Empty;
        public string DrgNo { get; set; } = string.Empty;
        public string DrgDescription { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
        public double Los { get; set; }
        public string QueryStatus { get; set; } = string.Empty;
        public DateTime QueryDate { get; set; }
    }
}