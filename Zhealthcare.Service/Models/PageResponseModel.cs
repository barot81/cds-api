using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Models
{
    public class PageResponseModel
    {
        public int? CurrentPage { get; set; }
        public int? Count { get; set; }

        public IEnumerable<Patient> Result { get; set; } = Enumerable.Empty<Patient>();
    }
}
