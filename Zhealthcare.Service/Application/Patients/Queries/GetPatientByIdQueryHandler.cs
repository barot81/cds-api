using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Queries
{

    public class GetPatientsByIdQueryHandler : IRequestHandler<GetPatientsByIdQuery, Patient>
    {
        private readonly IRepository<Patient> _repository;
        public GetPatientsByIdQueryHandler(IRepository<Patient> repository)
        => _repository = repository;

        public async Task<Patient> Handle(GetPatientsByIdQuery query, CancellationToken cancellationToken)
        => await _repository.GetAsync(query.Id.ToString(), query.Facility, cancellationToken);

    }
}
