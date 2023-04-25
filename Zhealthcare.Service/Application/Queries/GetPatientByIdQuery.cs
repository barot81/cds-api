using MediatR;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Queries
{
    public record GetPatientByIdQuery(string Facility, Guid Id) : IRequest<Patient> 
    { }
}
