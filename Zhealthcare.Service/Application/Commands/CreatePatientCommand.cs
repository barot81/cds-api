using MediatR;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Commands
{
    public record CreatePatientCommand(PatientDto PatientDto, string FacilityId) : IRequest<Patient>
    {
    }

    public class PatientDto
    {
        public Guid Id { get; set; }
        public string FacilityId { get; set; } = string.Empty;
        public string RoomId { get; set; } = string.Empty;
        public long AccountNo { get; set; }
        public bool IsActive { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Cds { get; set; } = String.Empty;
        public int Age { get; set; }
        public string Sex { get; set; } = String.Empty;
        public string HealthPlanName { get; set; } = String.Empty;
        public string QueryStatus { get; set; } = String.Empty;
        public int Los { get; set; }
        public string FinancialClass { get; set; } = String.Empty;
        public string Mrn { get; set; } = String.Empty;
        public string AdmissioinDate { get; set; } = String.Empty;
        public string ReimbursementType { get; set; } = String.Empty;
        public string DischargeDate { get; set; } = String.Empty;
        public string Concurrent_postDC { get; set; } = String.Empty;
        public string SecondaryInsurance { get; set; } = String.Empty;
        public bool Contracted { get; set; }
        public string PatientClass { get; set; } = String.Empty;
        public string Pdx { get; set; } = String.Empty;
        public string StatusClass { get; set; } = String.Empty;
    }
}
