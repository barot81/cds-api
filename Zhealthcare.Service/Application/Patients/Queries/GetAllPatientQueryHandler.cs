﻿using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Queries
{
    public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, IEnumerable<Patient>>
    {
        private readonly IRepository<Patient> _repository;
        public GetAllPatientsQueryHandler(IRepository<Patient> repository)
        => _repository = repository;

        public async Task<IEnumerable<Patient>> Handle(GetAllPatientsQuery query, CancellationToken cancellationToken)
        => await _repository.GetAsync(x => x.Type == nameof(Patient) && x.PartitionKey == query.Facility, cancellationToken);

    }

}
