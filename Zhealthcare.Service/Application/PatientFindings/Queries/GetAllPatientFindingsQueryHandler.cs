using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Queries
{
    public class GetAllPatientFindingsQueryHandler : IRequestHandler<GetAllPatientFindingsQuery, IEnumerable<PatientFinding>>
    {
        private readonly IRepository<PatientFinding> _repository;
        public GetAllPatientFindingsQueryHandler(IRepository<PatientFinding> repository)
        => _repository = repository;


        public async Task<IEnumerable<PatientFinding>> Handle(GetAllPatientFindingsQuery query, CancellationToken cancellationToken)
        => await _repository.GetAsync(x => x.Type == nameof(PatientFinding) && x.PatientId == query.PatientId && x.PartitionKey == query.Facility, cancellationToken);

    }

}
