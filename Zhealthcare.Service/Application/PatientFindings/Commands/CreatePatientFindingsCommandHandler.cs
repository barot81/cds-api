using Mapster;
using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Commands
{
    public class CreatePatientFindingsCommandHandler : IRequestHandler<CreatePatientFindingCommand, PatientFinding>
    {
        private readonly IRepository<PatientFinding> _repository;

        public CreatePatientFindingsCommandHandler(IRepository<PatientFinding> repository)
        => _repository = repository;

        public async Task<PatientFinding> Handle(CreatePatientFindingCommand command, CancellationToken cancellationToken)
        {
            var patientFinding = command.PatientFindingsDto.Adapt<PatientFinding>();
            patientFinding.CreatedTime = DateTime.UtcNow;
            patientFinding.PartitionKey = command.FacilityId;
            patientFinding.Type = typeof(PatientFinding).Name;

            return await _repository.CreateAsync(patientFinding, cancellationToken);
        }
    }
}
