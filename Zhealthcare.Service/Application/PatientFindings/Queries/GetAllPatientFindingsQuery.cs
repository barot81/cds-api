using MediatR;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Queries
{
    public record GetAllPatientFindingsQuery(string Facility, Guid PatientId) : IRequest<IEnumerable<PatientFinding>>
    { }
}
