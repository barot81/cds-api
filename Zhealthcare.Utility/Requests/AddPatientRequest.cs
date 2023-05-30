using MediatR;
using Zhealthcare.Service.Application.Patients.Models;

namespace Zhealthcare.Utility.Requests
{
    internal record AddPatientRequest(PatientDto PatientDto, CancellationToken CancellationToken) : IRequest<bool>
    {
    }
}
