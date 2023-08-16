namespace Zhealthcare.Utility.Models
{
    internal class ColumnMap
    {
        public string FacilityId { get; set; } = string.Empty;
        public Dictionary<string, string> Mapping { get; set; } = new Dictionary<string, string>();
    }
}
