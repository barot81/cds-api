using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Queries
{
    public class GetAllPatientFindingsQueryHandler : IRequestHandler<GetAllPatientFindingsQuery, IEnumerable<Patient>>
    {
        private readonly IRepository<Patient> _repository;
        public GetAllPatientFindingsQueryHandler(IRepository<Patient> repository)
        => _repository = repository;


        public async Task<IEnumerable<Patient>> Handle(GetAllPatientFindingsQuery query, CancellationToken cancellationToken)
        => await _repository.GetAsync(x => x.PartitionKey == query.Facility, cancellationToken);

    }

}
