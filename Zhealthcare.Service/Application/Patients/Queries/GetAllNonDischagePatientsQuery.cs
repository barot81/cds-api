using MediatR;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Queries
{
    public record GetAllNonDischagePatientsQuery(string FacilityId) : IRequest<IEnumerable<Patient>>;
}
