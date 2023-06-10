using MediatR;
using Zhealthcare.Service.Application.Patients.Models;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Commands
{
    public record UpdatePatientCommand(string Id, string FacilityId, PatientUpdateDto PatientDto) : IRequest<Guid>
    {
        public Patient MapPatient(Patient patient)
        {
            patient.IsActive = PatientDto.IsActive;
            patient.PatientName = PatientDto.PatientName;
            patient.Cds = PatientDto.Cds;
            patient.Age = PatientDto.Age;
            patient.Sex = PatientDto.Sex;
            patient.HealthPlan = PatientDto.HealthPlan;
            patient.QueryStatus = PatientDto.QueryStatus;
            patient.QueryDate = PatientDto.QueryDate;
            patient.Los = PatientDto.Los;
            patient.FinancialClass = PatientDto.FinancialClass;
            patient.Mrn = PatientDto.Mrn;
            patient.AdmitDate = PatientDto.AdmitDate;
            patient.DischargeDate = PatientDto.DischargeDate;
            patient.ReimbursementType = PatientDto.ReimbursementType;
            patient.Concurrent_postDC = PatientDto.Concurrent_postDC;
            patient.SecondaryInsurance = PatientDto.SecondaryInsurance;
            patient.Contracted = PatientDto.Contracted;
            patient.PatientClass = PatientDto.PatientClass;
            patient.ReviewStatus = PatientDto.ReviewStatus;
            patient.LastUpdatedDate = DateTime.UtcNow;
            patient.Type = typeof(Patient).Name;
            patient.GeneralComment = PatientDto.GeneralComment;
            return patient;
        }
    }
}
