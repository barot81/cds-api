namespace Zhealthcare.Service.Domain.Entities
{
    public class Lookup : BaseEntity
    {
        public Lookup(string id, IEnumerable<ILookupItem> items)
        {
            Id = id;
            Items = items;
        }

        public Lookup()
        {
            Items = Enumerable.Empty<ILookupItem>();
        }
        public IEnumerable<ILookupItem> Items { get; set; }
    }
}
