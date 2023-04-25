using MediatR;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Queries
{
    public record GetAllPatientQuery(string Facility) : IRequest<IEnumerable<Patient>>
    { }
}
