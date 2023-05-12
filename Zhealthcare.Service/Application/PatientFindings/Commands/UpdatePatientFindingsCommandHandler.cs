using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Commands
{

    public class UpdatePatientFindingsCommandHandler : IRequestHandler<UpdatePatientFindingsCommand, Guid>
    {
        private readonly IRepository<PatientFinding> _repository;

        public UpdatePatientFindingsCommandHandler(IRepository<PatientFinding> repository)
        => _repository = repository;

        public async Task<Guid> Handle(UpdatePatientFindingsCommand command, CancellationToken cancellationToken)
        {
            var updatedPatientFinding = command.MapPatientFinding();
            var result = await _repository.UpdateAsync(updatedPatientFinding, false, cancellationToken);
            return result == null ? Guid.Empty : Guid.Parse(result.Id);
        }
    }
}
