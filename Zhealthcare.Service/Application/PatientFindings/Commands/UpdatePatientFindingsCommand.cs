using MediatR;
using Zhealthcare.Service.Application.PatientFindings.Models;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Commands
{
    public record UpdatePatientFindingsCommand(string FacilityId, Guid PatientId, Guid FindingId, PatientFindingUpdateDto PatientFindingDto) : IRequest<Guid>
    {
        public PatientFinding MapPatientFinding(PatientFinding patientFinding)
        {
            patientFinding.QueryType = PatientFindingDto.QueryType;
            patientFinding.CdsName = PatientFindingDto.CdsName;
            patientFinding.QueryDate = PatientFindingDto.QueryDate;
            patientFinding.QueryDiagnosis = PatientFindingDto.QueryDiagnosis;
            patientFinding.PhysicianName = PatientFindingDto.PhysicianName;
            patientFinding.ClinicalIndicator = PatientFindingDto.ClinicalIndicator;
            patientFinding.CurrentDRG = PatientFindingDto.CurrentDRG;
            patientFinding.InitialWeight = PatientFindingDto.InitialWeight;
            patientFinding.Gmlos = PatientFindingDto.Gmlos;
            patientFinding.ExpectedDRG = PatientFindingDto.Gmlos;
            patientFinding.ExpectedWeight = PatientFindingDto.Gmlos;
            patientFinding.ExpectedGMLOS = PatientFindingDto.ExpectedGMLOS;
            patientFinding.ResponseDate = PatientFindingDto.ResponseDate;
            patientFinding.ResponseType = PatientFindingDto.ResponseType;
            patientFinding.ResponseComment = PatientFindingDto.ResponseComment;
            patientFinding.FollowupComment = PatientFindingDto.FollowupComment;
            patientFinding.RevisedDRG = PatientFindingDto.RevisedDRG;
            patientFinding.RevisedWeight = PatientFindingDto.RevisedWeight;
            patientFinding.RevisedGMLOS = PatientFindingDto.RevisedGMLOS;
            patientFinding.WeightDifference = PatientFindingDto.WeightDifference;
            patientFinding.Status = PatientFindingDto.Status;
            patientFinding.ClinicalSummary = PatientFindingDto.ClinicalSummary;
            patientFinding.Comments = PatientFindingDto.Comments;
            return patientFinding;
        }
    }
}
