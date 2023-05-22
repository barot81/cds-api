using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Commands
{

    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Guid>
    {
        private readonly IRepository<Patient> _repository;

        public UpdatePatientCommandHandler(IRepository<Patient> repository)
        => _repository = repository;

        public async Task<Guid> Handle(UpdatePatientCommand command, CancellationToken cancellationToken)
        {
            var patient = await _repository.GetAsync(command.Id, command.FacilityId, cancellationToken);
            var updatedPatient = command.MapPatient(patient);
            var result = await _repository.UpdateAsync(updatedPatient, false, cancellationToken);
            return result == null ? Guid.Empty : Guid.Parse(result.Id);
        }
    }
}
