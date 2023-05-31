using MediatR;

namespace Zhealthcare.Service.Application.Dashboard
{
    public record GetAllStatusCountsQuery(string FacilityId) : IRequest<IEnumerable<StatusCount>>
    {
    }

    public class StatusCount
    {
        public StatusCount(string name, int count)
        {
            Name= name;
            Count= count;
        }

        public string  Name { get; set; }
        public int Count { get; set; }
    }
}
