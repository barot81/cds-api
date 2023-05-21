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
            patient.FirstName = PatientDto.FirstName;
            patient.LastName = PatientDto.LastName;
            patient.Cds = PatientDto.Cds;
            patient.Age = PatientDto.Age;
            patient.Sex = PatientDto.Sex;
            patient.HealthPlanName = PatientDto.HealthPlanName;
            patient.QueryStatus = PatientDto.QueryStatus;
            patient.QueryDate = PatientDto.QueryDate;
            patient.Los = PatientDto.Los;
            patient.FinancialClass = PatientDto.FinancialClass;
            patient.Mrn = PatientDto.Mrn;
            patient.AdmissionDate = PatientDto.AdmissionDate;
            patient.DischargeDate = PatientDto.DischargeDate;
            patient.ReimbursementType = PatientDto.ReimbursementType;
            patient.Concurrent_postDC = PatientDto.Concurrent_postDC;
            patient.SecondaryInsurance = PatientDto.SecondaryInsurance;
            patient.Contracted = PatientDto.Contracted;
            patient.PatientClass = PatientDto.PatientClass;
            patient.Pdx = PatientDto.Pdx;
            patient.Status = PatientDto.Status;
            patient.LastUpdatedTime = DateTime.UtcNow;
            patient.Type = typeof(Patient).Name;
            patient.GeneralComment = PatientDto.GeneralComment;
            return patient;
        }
    }
}
