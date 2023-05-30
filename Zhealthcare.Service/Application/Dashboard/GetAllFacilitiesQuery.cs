using MediatR;

namespace Zhealthcare.Service.Application.Dashboard
{
    public class GetAllFacilitiesQuery : IRequest<IEnumerable<FacilitiesResponse>>
    {
    }

    public class FacilitiesResponse
    {
        public string FacilityId { get; set; } = string.Empty;

        public IEnumerable<string> Physicians { get; set; } = Enumerable.Empty<string>();
    }
}
