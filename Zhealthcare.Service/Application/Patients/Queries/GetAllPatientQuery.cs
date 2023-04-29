using MediatR;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Queries
{
    public record GetAllPatientsQuery(string Facility) : IRequest<IEnumerable<Patient>>
    { }
}
