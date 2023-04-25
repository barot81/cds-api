using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Queries
{

    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, Patient>
    {
        private readonly IRepository<Patient> _repository;
        public GetPatientByIdQueryHandler(IRepository<Patient> repository)
        => _repository = repository;

        public async Task<Patient> Handle(GetPatientByIdQuery query, CancellationToken cancellationToken)
        => await _repository.GetAsync(query.Id.ToString(), query.Facility, cancellationToken);

    }
}
