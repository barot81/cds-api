using MediatR;
using Zhealthcare.Service.Application.Models;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Commands
{
    public record CreatePatientCommand(string FacilityId, PatientDto PatientDto) : IRequest<Patient>
    {
    }
}
