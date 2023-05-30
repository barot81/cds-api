namespace Zhealthcare.Service.Domain.Entities.Lookup
{
    public class Lookup<T> : BaseEntity where T : ILookupItem
    {
        public Lookup(string id, IEnumerable<T> items)
        {
            Id = id;
            Items = items;
        }

        public IEnumerable<T> Items { get; set; }
    }
}
