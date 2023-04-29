using MediatR;
using Zhealthcare.Service.Application.Patients.Models;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Commands
{
    public record CreatePatientCommand(string FacilityId, PatientDto PatientDto) : IRequest<Patient>
    {
    }
}
