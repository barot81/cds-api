using MediatR;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Queries
{
    public record GetPatientsByIdQuery(string Facility, Guid Id) : IRequest<Patient>
    { }
}
