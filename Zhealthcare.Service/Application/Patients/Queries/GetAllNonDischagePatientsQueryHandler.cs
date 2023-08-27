using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Queries
{
    public class GetAllNonDischagePatientsQueryHandler : IRequestHandler<GetAllNonDischagePatientsQuery, IEnumerable<Patient>>
    {
        private readonly IRepository<Patient> _patientRepository;
        public GetAllNonDischagePatientsQueryHandler(IRepository<Patient> repository)
        => _patientRepository = repository;

        public async Task<IEnumerable<Patient>> Handle(GetAllNonDischagePatientsQuery request, CancellationToken cancellationToken)
        => await _patientRepository.GetAsync(x => x.DischargeDate == null && x.FacilityId == request.FacilityId && x.Type == nameof(Patient), cancellationToken);
        
    }
}
