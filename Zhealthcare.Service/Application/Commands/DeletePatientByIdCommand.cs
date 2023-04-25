using MediatR;

namespace Zhealthcare.Service.Application.Commands
{
    public record DeletePatientByIdCommand(Guid Id, string FacilityId) : IRequest<bool>
    { }
}
