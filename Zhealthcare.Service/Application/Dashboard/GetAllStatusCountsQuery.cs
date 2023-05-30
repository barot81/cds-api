using MediatR;

namespace Zhealthcare.Service.Application.Dashboard
{
    public record GetAllStatusCountsQuery(string FacilityId) : IRequest<StatusCountResponse>
    {
    }

    public class StatusCountResponse
    {
        public StatusCountResponse(Dictionary<string, int> statusCounts)
        {
            StatusCounts = statusCounts;
        }

        public Dictionary<string, int> StatusCounts { get; set; }
    }
}
