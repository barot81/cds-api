using Mapster;
using MediatR;
using Zhealthcare.Service.Application.PatientFindings.Models;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Commands
{
    public record UpdatePatientFindingsCommand(string FacilityId, Guid PatientId, Guid FindingId, PatientFindingUpdateDto PatientFindingDto) : IRequest<Guid>
    {
        public PatientFinding MapPatientFinding()
        {

            var patientFindingData = PatientFindingDto.Adapt<PatientFinding>();
            patientFindingData.FacilityId = FacilityId;
            patientFindingData.PatientId = PatientId;
            patientFindingData.PartitionKey = FacilityId; 
            return patientFindingData;
        }
    }
}
