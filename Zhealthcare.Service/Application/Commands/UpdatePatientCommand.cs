using MediatR;

namespace Zhealthcare.Service.Application.Commands
{
    public record UpdatePatientCommand(PatientDto PatientDto) : IRequest<Guid>
    { }
}
