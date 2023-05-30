namespace Zhealthcare.Service.Domain.Entities.Lookup
{
    public class DiagnosisLookupItem : ILookupItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
