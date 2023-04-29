using Mapster;
using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Commands
{
    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Patient>
    {
        private readonly IRepository<Patient> _repository;

        public CreatePatientCommandHandler(IRepository<Patient> repository)
        => _repository = repository;

        public async Task<Patient> Handle(CreatePatientCommand command, CancellationToken cancellationToken)
        {
            var patient = command.PatientDto.Adapt<Patient>();
            patient.CreatedTime = DateTime.UtcNow;
            patient.PartitionKey = command.FacilityId;
            patient.Type = nameof(Patient);

            return await _repository.CreateAsync(patient, cancellationToken);
        }
    }
}
