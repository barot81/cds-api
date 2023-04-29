using MediatR;

namespace Zhealthcare.Service.Application.Patients.Commands
{
    public record DeletePatientFindingsByIdCommand(string FacilityId, Guid PatientId, Guid FindingId) : IRequest<bool>
    { }
}
