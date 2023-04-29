using MediatR;

namespace Zhealthcare.Service.Application.Patients.Commands
{
    public record DeletePatientByIdCommand(string FacilityId, Guid Id) : IRequest<bool>
    { }
}
