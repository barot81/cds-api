namespace Zhealthcare.Service.Helper
{
    public class ColumnMapper
    {
        public string FacilityId { get; set; } = string.Empty;
        public Dictionary<string, string> Mapper { get; set; } = new();
    }

    public class Contract
    {
        public string Fc { get; set; } = string.Empty;
        public string Insurance { get; set; } = string.Empty;
        public bool IsContracted { get; set; }
        public string ReimbursementType { get; set; } = string.Empty;

    }
}
