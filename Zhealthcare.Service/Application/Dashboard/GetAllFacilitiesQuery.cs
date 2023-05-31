using MediatR;

namespace Zhealthcare.Service.Application.Dashboard
{
    public record GetAllFacilitiesQuery : IRequest<IEnumerable<string>>;
}
