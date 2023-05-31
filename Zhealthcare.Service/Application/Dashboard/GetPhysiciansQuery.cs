using MediatR;

namespace Zhealthcare.Service.Application.Dashboard
{
    public record GetPhysiciansQuery(string FacilityId) : IRequest<IEnumerable<string>>;
}
