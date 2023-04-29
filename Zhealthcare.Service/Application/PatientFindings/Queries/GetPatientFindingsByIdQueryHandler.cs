using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Queries
{

    public class GetPatientFindingsByIdQueryHandler : IRequestHandler<GetPatientFindingsByIdQuery, Patient>
    {
        private readonly IRepository<Patient> _repository;
        public GetPatientFindingsByIdQueryHandler(IRepository<Patient> repository)
        => _repository = repository;

        public async Task<Patient> Handle(GetPatientFindingsByIdQuery query, CancellationToken cancellationToken)
        => await _repository.GetAsync(query.Id.ToString(), query.Facility, cancellationToken);

    }
}
