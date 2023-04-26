using Mapster;
using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Commands
{
    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Patient>
    {
        private readonly IRepository<Patient> _repository;

        public CreatePatientCommandHandler(IRepository<Patient> repository)
        => _repository = repository;

        public async Task<Patient> Handle(CreatePatientCommand command, CancellationToken cancellationToken)
        {
            var patient = PatientDto.Adapt<Patient>();
            patient.CreatedTime = DateTime.UtcNow;
            patient.PartitionKey = FacilityId;
            patient.Type = typeof(Patient).Name;

            return await _repository.CreateAsync(patient, cancellationToken);
        }
    }
}
