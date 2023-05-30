namespace Zhealthcare.Service.Domain.Entities.Lookup
{

    public class ReimbursementTypeLookupItem : ILookupItem
    {
        public int Id { get; set; }
        public string PayorCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ReimburseType { get; set; } = string.Empty;
    }
}
