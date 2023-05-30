namespace Zhealthcare.Service.Domain.Entities
{

    public class ReimbursementTypeLookupItem : ILookupItem
    {
        public int Id { get; set; }
        public string PayorCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ReimburseType { get; set; } = string.Empty;
    }

    public class DiagnosisLookupItem : ILookupItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
