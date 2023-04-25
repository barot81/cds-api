using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Queries
{
    public class GetAllPatientQueryHandler : IRequestHandler<GetAllPatientQuery, IEnumerable<Patient>>
    {
        private readonly IRepository<Patient> _repository;
        public GetAllPatientQueryHandler(IRepository<Patient> repository)
        => _repository = repository;
        

        public async Task<IEnumerable<Patient>> Handle(GetAllPatientQuery query, CancellationToken cancellationToken)
        => await _repository.GetAsync(x => x.PartitionKey == query.Facility, cancellationToken);
          
    }

}
