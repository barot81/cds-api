using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Queries
{
    public class GetPatientsByNosQueryHandler : IRequestHandler<GetPatientsByNosQuery, IEnumerable<Patient>>
    {
        private readonly IRepository<Patient> _patientRepository;
        public GetPatientsByNosQueryHandler(IRepository<Patient> repository)
        => _patientRepository = repository;

        public async Task<IEnumerable<Patient>> Handle(GetPatientsByNosQuery request, CancellationToken cancellationToken)
        => await _patientRepository.GetAsync(x => request.PatientNos.Contains(x.PatientNo) && x.FacilityId == request.FacilityId, cancellationToken);
        
    }
}
