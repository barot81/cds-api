using MediatR;
using Zhealthcare.Service.Application.PatientFindings.Models;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Commands
{
    public record CreatePatientFindingCommand(string FacilityId,Guid PatientId, PatientFindingDto PatientFindingsDto) : IRequest<PatientFinding>
    {
    }
}
