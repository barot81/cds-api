using MediatR;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Queries
{
    public record GetPatientsByNosQuery(string FacilityId, List<long> PatientNos) : IRequest<IEnumerable<Patient>>;
}
