using Mapster;
using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;
using Zhealthcare.Service.Models;

namespace Zhealthcare.Service.Application.Patients.Commands
{
    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Patient>
    {
        private readonly IRepository<Patient> _repository;
        private readonly UserContext _userContext;

        public CreatePatientCommandHandler(IRepository<Patient> repository, UserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }

        public async Task<Patient> Handle(CreatePatientCommand command, CancellationToken cancellationToken)
        {
            var patient = command.PatientDto.Adapt<Patient>();
            patient.Id = Guid.NewGuid().ToString();
            patient.CreatedDate = DateTime.UtcNow;
            patient.CreatedBy = _userContext.Name;
            patient.LastUpdatedDate = DateTime.UtcNow;
            patient.LastUpdatedBy = _userContext.Name;
            patient.PartitionKey = command.FacilityId;
            patient.Type = nameof(Patient);
            return await _repository.CreateAsync(patient, cancellationToken);
        }
    }
}
