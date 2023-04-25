using Mapster;
using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Commands
{

    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Guid>
    {
        private readonly IRepository<Patient> _repository;

        public UpdatePatientCommandHandler(IRepository<Patient> repository)
        => _repository = repository;

        public async Task<Guid> Handle(UpdatePatientCommand command, CancellationToken cancellationToken)
        {
            var patient = command.PatientDto.Adapt<Patient>();
            patient.LastUpdatedTime = DateTime.UtcNow;
            patient.Type = typeof(Patient).Name;

            var result = await _repository.UpdateAsync(patient, cancellationToken: cancellationToken);

            return result == null ? default : Guid.Parse(result.Id);
            
        }

    }
}
